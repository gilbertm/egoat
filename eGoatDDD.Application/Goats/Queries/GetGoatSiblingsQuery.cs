using MediatR;
using eGoatDDD.Application.Goats.Models;

namespace eGoatDDD.Application.Goats.Queries
{
    public class GetGoatSiblingsQuery : IRequest<GoatsListViewModel>
    {
        public GetGoatSiblingsQuery()
        {

        }

        public GetGoatSiblingsQuery(long maternalId, long sireId)
        {
            MaternalId = maternalId;
            SireId = sireId;
        }

        public long MaternalId { get; set; }

        public long SireId { get; set; }
    }
}
