using System.Threading;
using MediatR;
using eGoatDDD.Persistence.Repository;
using AutoMapper;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace eGoatDDD.Application.Users.Roles.Queries
{
    public class GetRolesCurrentQueryHandler : IRequestHandler<GetRolesCurrentQuery, IList<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public GetRolesCurrentQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IList<string>> Handle(GetRolesCurrentQuery request, CancellationToken cancellationToken)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;

            var userClaims = new List<string>();

            foreach (var claim in claims)
            {
                if (claim.Type == ClaimTypes.Role)
                {
                    userClaims.Add(claim.Value);
                }
            }

            await Task.Delay(10);

            return userClaims.ToList();
        }
    }
}
