using MediatR;

namespace eGoatDDD.Application.GoatResources.Queries
{
    public class GetGoatResourceQuery : IRequest<bool>
    {
        public GetGoatResourceQuery()
        {
        }

        public GetGoatResourceQuery(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; set; }
    }
}
