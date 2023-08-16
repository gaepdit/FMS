using System;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Services;
using FMS.Pages.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NSubstitute;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace FMS.App.Tests.Users
{
    public class UserDetailsTests
    {
        [Fact]
        public async Task OnGet_PopulatesThePageModel()
        {
            var user = new UserView(UserTestData.ApplicationUsers[0]);

            var mockUserService = Substitute.For<IUserService>();
            mockUserService.GetUserByIdAsync(Arg.Any<Guid>()).Returns(user);

            var pageModel = new DetailsModel(mockUserService);

            var result = await pageModel.OnGetAsync(user.Id).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.Id.Should().Be(user.Id.ToString());
            pageModel.Email.Should().Be(user.Email);
            pageModel.DisplayName.Should().Be(user.DisplayName);
        }

        [Fact]
        public async Task OnGet_NonexistentIdReturnsNotFound()
        {
            var mockUserService = Substitute.For<IUserService>();
            var pageModel = new DetailsModel(mockUserService);

            var result = await pageModel.OnGetAsync(Guid.Empty).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.Email.ShouldBeNull();
            pageModel.DisplayName.ShouldBeNull();
        }

        [Fact]
        public async Task OnGet_MissingIdReturnsNotFound()
        {
            var mockUserService = Substitute.For<IUserService>();
            var pageModel = new DetailsModel(mockUserService);

            var result = await pageModel.OnGetAsync(null).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.Email.ShouldBeNull();
            pageModel.DisplayName.ShouldBeNull();
        }
    }
}
