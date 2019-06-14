using MediatR;
using eGoatDDD.Application.Goats.Models;

namespace eGoatDDD.Application.Goats.Queries
{
    public class GetGoatSiblingsQuery : IRequest<GoatsListViewModel>
    {
        public GetGoatSiblingsQuery()
        {

        }

        public GetGoatSiblingsQuery(long maternalId, long sireId, long goatId)
        {
            MaternalId = maternalId;
            SireId = sireId;
            GoatId = goatId;
        }

        public long MaternalId { get; set; }

        public long SireId { get; set; }

        public long GoatId { get; set; }
    }
}
