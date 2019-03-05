using MediatR;
using eGoatDDD.Application.Parents.Models;

namespace eGoatDDD.Application.Parents.Queries
{
    public class GetParentsQuery : IRequest<ParentsListViewModel>
    {
        public GetParentsQuery()
        {

        }

        public GetParentsQuery(long goatChildId)
        {
            GoatChildId = goatChildId;
        }

        public long GoatChildId { get; set; }
    }
}
