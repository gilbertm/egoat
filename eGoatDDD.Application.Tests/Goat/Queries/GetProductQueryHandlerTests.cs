using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Application.Goats.Queries;
using eGoatDDD.Application.Tests.Infrastructure;
using eGoatDDD.Persistence;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace eGoatDDD.Application.Goat.Queries.Tests
{

    [Collection("QueryCollection")]
    public class GetGoatQueryHandlerTests
    {
        private readonly eGoatDDDDbContext _context;

        public GetGoatQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        /* [Fact]
        public async Task GetGoattDetailTest()
        {
            var sut = new GetGoatQueryHandler(_context);

            var result = await sut.Handle(new GetGoatQuery(1), CancellationToken.None);

            result.ShouldBeOfType<GoatViewModel>();
            result.Goat.Id.ShouldBe(1);
        } */
    }
}
