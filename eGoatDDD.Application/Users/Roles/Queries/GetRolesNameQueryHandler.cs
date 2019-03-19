using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using eGoatDDD.Persistence.Repository;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using System.Collections.Generic;

namespace eGoatDDD.Application.Users.Roles.Queries
{
    public class GetRolesNameQueryHandler : IRequestHandler<GetRolesNameQuery, IEnumerable<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public GetRolesNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<string>> Handle(GetRolesNameQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<IdentityRole>().Query().Select(r => r.Name).ToListAsync();
        }
    }
}
