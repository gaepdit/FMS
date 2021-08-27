using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Entities.Users;
using FMS.Domain.Services;
using FMS.Pages.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace FMS.App.Tests.Users
{
    public class UserEditTests
    {
        [Fact]
        public async Task OnGet_PopulatesThePageModel()
        {
            var user = new UserView(UserTestData.ApplicationUsers[0]);

            var mockRepo = new Mock<IUserService>();
            mockRepo.Setup(l => l.GetUserByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(user);
            mockRepo.Setup(l => l.GetUserRolesAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<string>());

            var pageModel = new EditModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(Guid.Empty).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.UserId.ShouldEqual(Guid.Empty);
            pageModel.DisplayName.ShouldEqual(user.DisplayName);
            pageModel.Email.ShouldEqual(user.Email);
            pageModel.HasFileCreatorRole.ShouldBeFalse();
            pageModel.HasFileEditorRole.ShouldBeFalse();
            pageModel.HasSiteMaintenanceRole.ShouldBeFalse();
            pageModel.HasUserAdminRole.ShouldBeFalse();
        }

        [Fact]
        public async Task OnGet_WithRoles_PopulatesThePageModel()
        {
            var user = new UserView(UserTestData.ApplicationUsers[0]);
            var roles = new List<string>()
            {
                UserRoles.FileCreator,
                UserRoles.FileEditor,
                UserRoles.SiteMaintenance,
                UserRoles.UserMaintenance,
            };

            var mockRepo = new Mock<IUserService>();
            mockRepo.Setup(l => l.GetUserByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(user);
            mockRepo.Setup(l => l.GetUserRolesAsync(It.IsAny<Guid>()))
                .ReturnsAsync(roles);

            var pageModel = new EditModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(Guid.Empty).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.UserId.ShouldEqual(Guid.Empty);
            pageModel.DisplayName.ShouldEqual(user.DisplayName);
            pageModel.Email.ShouldEqual(user.Email);
            pageModel.HasFileCreatorRole.ShouldBeTrue();
            pageModel.HasFileEditorRole.ShouldBeTrue();
            pageModel.HasSiteMaintenanceRole.ShouldBeTrue();
            pageModel.HasUserAdminRole.ShouldBeTrue();
        }

        [Fact]
        public async Task OnGet_MissingId_ReturnsNotFound()
        {
            var mockRepo = new Mock<IUserService>();
            var pageModel = new EditModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(null).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.UserId.Should().Be(Guid.Empty);
        }

        [Fact]
        public async Task OnGet_NonexistentId_ReturnsNotFound()
        {
            var mockRepo = new Mock<IUserService>();
            mockRepo.Setup(l => l.GetUserByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((UserView)null);
            var pageModel = new EditModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(Guid.Empty).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.UserId.Should().Be(Guid.Empty);
        }

        [Fact]
        public async Task OnPost_ValidModel_ReturnsDetailsPage()
        {
            var mockRepo = new Mock<IUserService>();
            mockRepo.Setup(l => l.UpdateUserRolesAsync(It.IsAny<Guid>(), It.IsAny<Dictionary<string, bool>>()))
                .ReturnsAsync(IdentityResult.Success);

            var pageModel = new EditModel(mockRepo.Object)
            {
                UserId = Guid.Empty
            };

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            pageModel.ModelState.IsValid.ShouldBeTrue();
            result.Should().BeOfType<RedirectToPageResult>();
            ((RedirectToPageResult)result).PageName.Should().Be("./Details");
            ((RedirectToPageResult)result).RouteValues["id"].Should().Be(Guid.Empty);
        }

        [Fact]
        public async Task OnPost_InvalidModel_ReturnsPageWithInvalidModelState()
        {
            var mockRepo = new Mock<IUserService>();
            var pageModel = new EditModel(mockRepo.Object);
            pageModel.ModelState.AddModelError("Error", "Sample error description");

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeFalse();
            pageModel.ModelState["Error"].Errors[0].ErrorMessage.Should().Be("Sample error description");
        }

        [Fact]
        public async Task OnPost_UpdateRolesFails_ReturnsPageWithInvalidModelState()
        {
            var user = new UserView(UserTestData.ApplicationUsers[0]);
            var identityResult = IdentityResult.Failed(
                new IdentityError() { Code = "CODE", Description = "DESCRIPTION" });

            var mockRepo = new Mock<IUserService>();
            mockRepo.Setup(l => l.UpdateUserRolesAsync(It.IsAny<Guid>(), It.IsAny<Dictionary<string, bool>>()))
                .ReturnsAsync(identityResult);
            mockRepo.Setup(l => l.GetUserByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(user);
            mockRepo.Setup(l => l.GetUserRolesAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<string>());

            var pageModel = new EditModel(mockRepo.Object);

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeFalse();
            pageModel.ModelState[string.Empty].Errors[0].ErrorMessage.Should().Be("CODE: DESCRIPTION");
        }
    }
}