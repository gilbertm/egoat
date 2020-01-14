using MediatR;
using eGoatDDD.Application.Histories.Models;

namespace eGoatDDD.Application.Histories.Queries
{
    public class GetHistoriesQuery : IRequest<HistoriesListViewModel>
    {
        public GetHistoriesQuery()
        {

        }

        public GetHistoriesQuery(long goatId)
        {
            GoatId = goatId;
        }

        public long GoatId { get; set; }
    }
}
