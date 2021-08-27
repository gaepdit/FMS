using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Services;
using FMS.Pages.Users;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
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

            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(l => l.GetUsersAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(searchResults)
                .Verifiable();
            var pageModel = new IndexModel();

            var result = await pageModel.OnGetSearchAsync(mockUserService.Object, name, email, role)
                .ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeTrue();
            pageModel.SearchResults.Should().BeEquivalentTo(searchResults);
        }

        [Fact]
        public async Task OnSearch_IfInvalidModel_ReturnPageWithInvalidModelState()
        {
            var mockUserService = new Mock<IUserService>();
            var pageModel = new IndexModel();
            pageModel.ModelState.AddModelError("Error", "Sample error description");

            var result = await pageModel.OnGetSearchAsync(mockUserService.Object, null, null, null)
                .ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeFalse();
        }
    }
}