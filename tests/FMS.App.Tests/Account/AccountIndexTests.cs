using System;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Entities.Users;
using FMS.Domain.Services;
using FMS.Pages.Account;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using Xunit;

namespace FMS.App.Tests.Account
{
    public class AccountIndexTests
    {
        [Fact]
        public async Task OnGet_PopulatesThePageModel()
        {
            var userView = new UserView(new ApplicationUser
            {
                Id = Guid.Empty,
                Email = "example.one@example.com",
                GivenName = "Sample",
                FamilyName = "User"
            });

            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(l => l.GetCurrentUserAsync())
                .ReturnsAsync(userView)
                .Verifiable();
            var pageModel = new IndexModel(mockUserService.Object);

            var result = await pageModel.OnGetAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.CurrentUser.Email.Should().Be(userView.Email);
            pageModel.CurrentUser.DisplayName.Should().Be(userView.DisplayName);
        }
    }
}