using MediatR;
using eGoatDDD.Application.AppUsers.Models;

namespace eGoatDDD.Application.AppUsers.Queries
{
    public class GetAppUserQuery : IRequest<AppUserViewModel>
    {
        public GetAppUserQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
