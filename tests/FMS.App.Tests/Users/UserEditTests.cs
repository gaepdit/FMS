using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using FMS.Domain.Entities.Users;
using FMS.Domain.Services;
using FMS.Pages.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NSubstitute;
using NUnit.Framework;

namespace FMS.App.Tests.Users
{
    public class UserEditTests
    {
        [Test]
        public async Task OnGet_PopulatesThePageModel()
        {
            var user = new UserView(UserTestData.ApplicationUsers[0]);

            var mockRepo = Substitute.For<IUserService>();
            mockRepo.GetUserByIdAsync(Arg.Any<Guid>()).Returns(user);
            mockRepo.GetUserRolesAsync(Arg.Any<Guid>()).Returns(new List<string>());

            var pageModel = new EditModel(mockRepo);

            var result = await pageModel.OnGetAsync(Guid.Empty).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.UserId.Should().Be(Guid.Empty);
            pageModel.DisplayName.Should().Be(user.DisplayName);
            pageModel.Email.Should().Be(user.Email);
            pageModel.HasFileCreatorRole.Should().BeFalse();
            pageModel.HasFileEditorRole.Should().BeFalse();
            pageModel.HasSiteMaintenanceRole.Should().BeFalse();
            pageModel.HasUserAdminRole.Should().BeFalse();
        }

        [Test]
        public async Task OnGet_WithRoles_PopulatesThePageModel()
        {
            var user = new UserView(UserTestData.ApplicationUsers[0]);
            var roles = new List<string>
            {
                UserRoles.FileCreator,
                UserRoles.FileEditor,
                UserRoles.SiteMaintenance,
                UserRoles.UserMaintenance,
            };

            var mockRepo = Substitute.For<IUserService>();
            mockRepo.GetUserByIdAsync(Arg.Any<Guid>()).Returns(user);
            mockRepo.GetUserRolesAsync(Arg.Any<Guid>()).Returns(roles);

            var pageModel = new EditModel(mockRepo);

            var result = await pageModel.OnGetAsync(Guid.Empty).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.UserId.Should().Be(Guid.Empty);
            pageModel.DisplayName.Should().Be(user.DisplayName);
            pageModel.Email.Should().Be(user.Email);
            pageModel.HasFileCreatorRole.Should().BeTrue();
            pageModel.HasFileEditorRole.Should().BeTrue();
            pageModel.HasSiteMaintenanceRole.Should().BeTrue();
            pageModel.HasUserAdminRole.Should().BeTrue();
        }

        [Test]
        public async Task OnGet_MissingId_ReturnsNotFound()
        {
            var mockRepo = Substitute.For<IUserService>();
            var pageModel = new EditModel(mockRepo);

            var result = await pageModel.OnGetAsync(null).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.UserId.Should().Be(Guid.Empty);
        }

        [Test]
        public async Task OnGet_NonexistentId_ReturnsNotFound()
        {
            var mockRepo = Substitute.For<IUserService>();
            mockRepo.GetUserByIdAsync(Arg.Any<Guid>()).Returns((UserView)null);
            var pageModel = new EditModel(mockRepo);

            var result = await pageModel.OnGetAsync(Guid.Empty).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.UserId.Should().Be(Guid.Empty);
        }

        [Test]
        public async Task OnPost_ValidModel_ReturnsDetailsPage()
        {
            var mockRepo = Substitute.For<IUserService>();
            mockRepo.UpdateUserRolesAsync(Arg.Any<Guid>(), Arg.Any<Dictionary<string, bool>>()).Returns(IdentityResult.Success);

            var pageModel = new EditModel(mockRepo)
            {
                UserId = Guid.Empty
            };

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            pageModel.ModelState.IsValid.Should().BeTrue();
            result.Should().BeOfType<RedirectToPageResult>();
            ((RedirectToPageResult)result).PageName.Should().Be("./Details");
            ((RedirectToPageResult)result).RouteValues["id"].Should().Be(Guid.Empty);
        }

        [Test]
        public async Task OnPost_InvalidModel_ReturnsPageWithInvalidModelState()
        {
            var mockRepo = Substitute.For<IUserService>();
            var pageModel = new EditModel(mockRepo);
            pageModel.ModelState.AddModelError("Error", "Sample error description");

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.Should().BeFalse();
            pageModel.ModelState["Error"].Errors[0].ErrorMessage.Should().Be("Sample error description");
        }

        [Test]
        public async Task OnPost_UpdateRolesFails_ReturnsPageWithInvalidModelState()
        {
            var user = new UserView(UserTestData.ApplicationUsers[0]);
            var identityResult = IdentityResult.Failed(
                new IdentityError { Code = "CODE", Description = "DESCRIPTION" });

            var mockRepo = Substitute.For<IUserService>();
            mockRepo.UpdateUserRolesAsync(Arg.Any<Guid>(), Arg.Any<Dictionary<string, bool>>()).Returns(identityResult);
            mockRepo.GetUserByIdAsync(Arg.Any<Guid>()).Returns(user);
            mockRepo.GetUserRolesAsync(Arg.Any<Guid>()).Returns(new List<string>());

            var pageModel = new EditModel(mockRepo);

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.Should().BeFalse();
            pageModel.ModelState[string.Empty].Errors[0].ErrorMessage.Should().Be("CODE: DESCRIPTION");
        }
    }
}
