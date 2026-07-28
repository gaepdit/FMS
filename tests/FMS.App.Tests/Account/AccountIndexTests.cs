using FMS.Domain.Entities.Users;
using FMS.Domain.Services;
using FMS.Pages.Account;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.App.Tests.Account
{
    public class AccountIndexTests
    {
        [Test]
        public async Task OnGet_PopulatesThePageModel()
        {
            var userView = new UserView(new ApplicationUser
            {
                Id = Guid.Empty,
                Email = "example.one@example.com",
                GivenName = "Sample",
                FamilyName = "User"
            });
            var listHelper = Substitute.For<ISelectListHelper>();

            var mockUserService = Substitute.For<IUserService>();
            mockUserService.GetCurrentUserAsync().Returns(userView);
            var pageModel = new IndexModel(mockUserService, listHelper);

            var result = await pageModel.OnGetAsync(null).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.CurrentUser.Email.Should().Be(userView.Email);
            pageModel.CurrentUser.DisplayName.Should().Be(userView.DisplayName);
        }
    }
}
