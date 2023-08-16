using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Services;
using FMS.Pages.Users;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NSubstitute;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace FMS.App.Tests.Users
{
    public class UserSearchTests
    {
        [Theory]
        [InlineData(null, null, null)]
        [InlineData("a", "b", "c")]
        public async Task OnSearch_IfValidModel_ReturnPage(string name, string email, string role)
        {
            var searchResults = UserTestData.ApplicationUsers
                .Select(e => new UserView(e))
                .ToList();

            var mockUserService = Substitute.For<IUserService>();
            mockUserService.GetUsersAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns(searchResults);
            var pageModel = new IndexModel();

            var result = await pageModel.OnGetSearchAsync(mockUserService, name, email, role)
                .ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeTrue();
            pageModel.SearchResults.Should().BeEquivalentTo(searchResults);
        }

        [Fact]
        public async Task OnSearch_IfInvalidModel_ReturnPageWithInvalidModelState()
        {
            var mockUserService = Substitute.For<IUserService>();
            var pageModel = new IndexModel();
            pageModel.ModelState.AddModelError("Error", "Sample error description");

            var result = await pageModel.OnGetSearchAsync(mockUserService, null, null, null)
                .ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeFalse();
        }
    }
}
