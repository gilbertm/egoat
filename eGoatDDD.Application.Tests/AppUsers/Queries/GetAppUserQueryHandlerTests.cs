using eGoatDDD.Application.AppUsers.Models;
using eGoatDDD.Application.Tests.Infrastructure;
using eGoatDDD.Persistence;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace eGoatDDD.Application.AppUsers.Queries.Tests
{

    [Collection("QueryCollection")]
    public class UpdateAppUserCommandHandlerTests
    {
        private readonly eGoatDDDDbContext _context;

        public UpdateAppUserCommandHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetAppUserBeforeUpdateTest()
        {
            var sut = new GetAppUserQueryHandler(_context);

            var result = await sut.Handle(new GetAppUserQuery("1"), CancellationToken.None);

            result.ShouldBeOfType<AppUserViewModel>();
            result.AppUser.FirstName.ShouldNotBe("Jose");
        }
    }
}
