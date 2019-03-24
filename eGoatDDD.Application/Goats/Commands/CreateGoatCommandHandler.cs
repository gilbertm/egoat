using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using eGoatDDD.Persistence.Repository;
using ImageWriter.Interface;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace eGoatDDD.Application.Goats.Commands
{
    public class CreateGoatCommandHandler : IRequestHandler<CreateGoatCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;
        private readonly IImageWriter _imageWriter;
        private IHostingEnvironment _hostingEnvironment;


        public CreateGoatCommandHandler(IUnitOfWork unitOfWork,
            eGoatDDDDbContext context,
            IMediator mediator,
            IImageWriter imageWriter, IHostingEnvironment environment)
        {
            _context = context;
            _mediator = mediator;
            _imageWriter = imageWriter;
            _unitOfWork = unitOfWork;
            _hostingEnvironment = environment;
        }

        public async Task<bool> Handle(CreateGoatCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var goat = new Goat
                    {
                        Id = 0,
                        ColorId = request.ColorId,
                        DisposalId = request.DisposalId,
                        Code = request.Code,
                        Gender = request.Gender,
                        BirthDate = request.BirthDate,
                        Description = request.Description,
                    };

                    _context.Goats.Add(goat);

                    await _context.SaveChangesAsync(cancellationToken);

                    if (request.MaternalId.HasValue)
                        if (request.MaternalId.Value > 0)
                        {
                            _context.Parents.Add(new Parent
                            {
                                ParentId = request.MaternalId.Value,
                                GoatId = goat.Id
                            });
                        }

                    if (request.SireId.HasValue)
                        if (request.SireId.Value > 0)
                        {
                            _context.Parents.Add(new Parent
                            {
                                ParentId = request.SireId.Value,
                                GoatId = goat.Id
                            });
                        }

                    for (int i = 0; i < request.BreedId.Count(); i++)
                    {
                        _context.GoatBreeds.Add(new GoatBreed
                        {
                            GoatId = goat.Id,
                            BreedId = (int)request.BreedId.ElementAt(i),
                            Percentage = (float)request.BreedPercent.ElementAt(i)
                        });
                    }

                    await _context.SaveChangesAsync(cancellationToken);


                    if (request.Files != null)
                    {
                        foreach (IFormFile file in request.Files)
                        {
                            if (file == null || file.Length == 0)
                            {
                                continue;
                            }

                            var result = await _imageWriter.UploadImage(file, "tmp");

                            string webRootPath = _hostingEnvironment.WebRootPath;
                            string contentRootPath = _hostingEnvironment.ContentRootPath;


                            if (result.ToString() != "")
                            {
                                var oldFilePathAndName = Path.Combine(contentRootPath, "tmp", result.ToString());

                                var targetPath = Path.Combine("images", "uploads");

                                var newFilePathAndName = Path.Combine(webRootPath, targetPath, result.ToString());

                                File.Move(oldFilePathAndName, newFilePathAndName);

                                Resource resource = new Resource
                                {
                                    Filename = result.ToString(),
                                    Location = targetPath
                                };

                                _context.Resources.Add(resource);

                                _context.GoatResources.Add(new GoatResource
                                {
                                    GoatId = goat.Id,
                                    ResourceId = resource.ResourceId,
                                });

                            }



                        }
                    }

                    await _context.SaveChangesAsync(cancellationToken);

                    transaction.Complete();
                }
                catch (Exception e)
                {
                    _unitOfWork.Rollback();

                    return false;
                }
            }

            return true;
        }
    }
}