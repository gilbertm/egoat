using eGoatDDD.Application.Loans.Models;
using MediatR;

namespace eGoatDDD.Application.Loans.Queries
{
    public class GetLoansQuery : IRequest<LoansListViewModel>
    {
        public GetLoansQuery()
        {

        }

        public GetLoansQuery(string userId, string role)
        {
            UserId = userId;
            Role = role;
        }

        public string UserId { get; set; }

        public string Role { get; set; }
    }
}