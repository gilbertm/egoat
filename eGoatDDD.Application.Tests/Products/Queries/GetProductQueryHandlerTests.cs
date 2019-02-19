using eGoatDDD.Application.Products.Models;
using eGoatDDD.Application.Tests.Infrastructure;
using eGoatDDD.Persistence;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace eGoatDDD.Application.Products.Queries.Tests
{

    [Collection("QueryCollection")]
    public class GetAppUserQueryHandlerTests
    {
        private readonly eGoatDDDDbContext _context;

        public GetAppUserQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetProductDetailTest()
        {
            var sut = new GetProductQueryHandler(_context);

            var result = await sut.Handle(new GetProductQuery(1), CancellationToken.None);

            result.ShouldBeOfType<ProductViewModel>();
            result.Product.ProductName.ShouldBe("Cash");
        }
    }
}
