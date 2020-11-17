using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Services;
using FMS.Pages.Users;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using Xunit;
using Xunit.Extensions.AssertExtensions;
using static FMS.Pages.Users.IndexModel;

namespace FMS.App.Tests.Users
{
    public class UserSearchTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData("a", "b")]
        public async Task OnSearch_IfValidModel_ReturnPage(string name, string email)
        {
            var users = UserTestData.ApplicationUsers;
            var searchResults = users
                .Select(e => new UserSearchResult() {Email = e.Email, Name = e.SortableFullName, Id = e.Id})
                .ToList();

            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(l => l.GetUsersAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(users)
                .Verifiable();
            var pageModel = new IndexModel(mockUserService.Object);

            var result = await pageModel.OnGetSearchAsync(name, email).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeTrue();
            pageModel.SearchResults.Should().BeEquivalentTo(searchResults);
        }

        [Fact]
        public async Task OnSearch_IfInvalidModel_ReturnPageWithInvalidModelState()
        {
            var mockUserService = new Mock<IUserService>();
            var pageModel = new IndexModel(mockUserService.Object);
            pageModel.ModelState.AddModelError("Error", "Sample error description");

            var result = await pageModel.OnGetSearchAsync(null, null).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeFalse();
        }
    }
}