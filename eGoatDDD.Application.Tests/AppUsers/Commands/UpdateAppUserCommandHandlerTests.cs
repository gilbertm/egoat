using eGoatDDD.Application.AppUsers.Models;
using eGoatDDD.Application.Tests.Infrastructure;
using eGoatDDD.Persistence;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace eGoatDDD.Application.AppUsers.Commands.Tests
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
            var sut = new UpdateAppUserCommandHandler(_context);

            var appUserDto = new AppUserDto
            {
                AppUserId = "1",
                FirstName = "GilbertTwo",
                PackageId = 1,
                Role = "Lender",
                LastName = "Maloloy-off",
            };

            var result = await sut.Handle(new UpdateAppUserCommand(appUserDto), CancellationToken.None);

            result.ShouldBeOfType<AppUserDto>();
            result.FirstName.ShouldNotBe("Gilbert");
            result.FirstName.ShouldBe("GilbertTwo");
        }
    }
}
