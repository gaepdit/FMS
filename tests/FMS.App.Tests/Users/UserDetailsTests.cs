using FluentAssertions;
using FMS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using System;
using System.Threading.Tasks;
using TestHelpers;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace FMS.App.Tests.Users
{
    public class UserDetailsTests
    {
        [Fact]
        public async Task OnGet_PopulatesThePageModel()
        {
            var user = DataHelpers.GetApplicationUsers()[0];

            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(l => l.GetUserByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(user)
                .Verifiable();

            var pageModel = new Pages.Users.DetailsModel(mockUserService.Object);

            var result = await pageModel.OnGetAsync(user.Id).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.Id.Should().Be(user.Id.ToString());
            pageModel.Email.Should().Be(user.Email);
            pageModel.DisplayName.Should().Be(user.DisplayName);
        }

        [Fact]
        public async Task OnGet_NonexistentIdReturnsNotFound()
        {
            var mockUserService = new Mock<IUserService>();
            var pageModel = new Pages.Users.DetailsModel(mockUserService.Object);

            var result = await pageModel.OnGetAsync(Guid.Empty).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.Email.ShouldBeNull();
            pageModel.DisplayName.ShouldBeNull();
        }

        [Fact]
        public async Task OnGet_MissingIdReturnsNotFound()
        {
            var mockUserService = new Mock<IUserService>();
            var pageModel = new Pages.Users.DetailsModel(mockUserService.Object);

            var result = await pageModel.OnGetAsync(null).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.Email.ShouldBeNull();
            pageModel.DisplayName.ShouldBeNull();
        }
    }
}