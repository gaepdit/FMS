using FluentAssertions;
using FMS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHelpers;
using Xunit;
using static FMS.Pages.Users.IndexModel;

namespace FMS.Tests.Users
{
    public class UserSearchTests
    {
        private readonly List<UserSearchResult> _searchResults = DataHelpers
            .GetApplicationUsers()
            .Select(e => new UserSearchResult()
            {
                Email = e.Email,
                Name = e.SortableFullName,
                Id = e.Id
            }).ToList();

        [Fact]
        public async Task OnPost_IfValidModel_ReturnPage()
        {
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(l => l.GetUsersAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(DataHelpers.GetApplicationUsers())
                .Verifiable();
            var pageModel = new Pages.Users.IndexModel(mockUserService.Object);

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.Should().BeTrue();
            pageModel.SearchResults.Should().BeEquivalentTo(_searchResults);
        }

        [Fact]
        public async Task OnPost_IfInvalidModel_ReturnBadRequestAsync()
        {
            var mockUserService = new Mock<IUserService>();
            var pageModel = new Pages.Users.IndexModel(mockUserService.Object);
            pageModel.ModelState.AddModelError("Error", "Sample error description");

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            //assert
            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.Should().BeFalse();
        }
    }
}
