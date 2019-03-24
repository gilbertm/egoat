using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using eGoatDDD.Persistence.Repository;
using ImageWriter.Interface;
using Microsoft.AspNetCore.Hosting;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using System.IO;
using eGoatDDD.Application.GoatResources.Models;
using Microsoft.EntityFrameworkCore;

namespace eGoatDDD.Application.Goats.Commands
{
    public class EditGoatCommandHandler : IRequestHandler<EditGoatCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;
        private readonly IImageWriter _imageWriter;
        private IHostingEnvironment _hostingEnvironment;

        public EditGoatCommandHandler(IUnitOfWork unitOfWork,
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

        public async Task<bool> Handle(EditGoatCommand request, CancellationToken cancellationToken)
        {

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    Goat goat = _context.Goats.
                    Where(g => g.Id == request.Id).SingleOrDefault();

                    if (goat != null)
                    {
                        goat.ColorId = request.ColorId;
                        goat.Code = request.Code;
                        goat.Gender = request.Gender;
                        goat.BirthDate = request.BirthDate;
                        goat.Description = request.Description;
                    };

                    if (request.MaternalId.HasValue)
                        if (request.MaternalId.Value > 0)
                        {
                            Parent parent = _context.Parents.Where(p => (p.GoatId == request.Id) && (p.ParentId == request.MaternalId.Value)).SingleOrDefault();

                            _context.Parents.Remove(parent);

                            _context.Parents.Add(new Parent
                            {
                                ParentId = request.MaternalId.Value,
                                GoatId = goat.Id
                            });
                        }

                    if (request.SireId.HasValue)
                        if (request.SireId.Value > 0)
                        {
                            Parent parent = _context.Parents.Where(p => (p.GoatId == request.Id) && (p.ParentId == request.SireId.Value)).SingleOrDefault();

                            _context.Parents.Remove(parent);


                            _context.Parents.Add(new Parent
                            {
                                ParentId = request.SireId.Value,
                                GoatId = goat.Id
                            });
                        }

                    List<GoatBreed> goatBreeds = _context.GoatBreeds.Where(breed => (breed.GoatId == request.Id)).ToList();

                    _context.GoatBreeds.RemoveRange(goatBreeds);

                    for (int i = 0; i < request.BreedId.Count(); i++)
                    {

                        _context.GoatBreeds.Add(new GoatBreed
                        {
                            GoatId = goat.Id,
                            BreedId = (int)request.BreedId.ElementAt(i),
                            Percentage = (float)request.BreedPercent.ElementAt(i)
                        });
                    }

                    List<GoatResource> goatResources = _context.GoatResources.Include(r => r.Resource).Where(gr => gr.GoatId == request.Id).ToList();

                    _context.GoatResources.RemoveRange(goatResources);

                    foreach (var item in goatResources)
                    {
                        _context.Resources.Remove(item.Resource);
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