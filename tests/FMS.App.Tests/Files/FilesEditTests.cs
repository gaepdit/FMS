using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using System;
using System.Threading.Tasks;
using TestHelpers;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace FMS.App.Tests.Files
{
    public class FilesEditTests
    {
        [Fact]
        public async Task OnGet_PopulatesThePageModel()
        {
            var file = DataHelpers.Files[0];
            var mockRepo = new Mock<IFileRepository>();
            mockRepo.Setup(l => l.GetFileAsync(file.Id))
                .ReturnsAsync(new FileDetailDto(file))
                .Verifiable();
            var pageModel = new Pages.Files.EditModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(file.Id).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.Id.Should().Be(file.Id);
            pageModel.FileLabel.Should().Be(file.Name);
            pageModel.Active.Should().Be(file.Active);
        }

        [Fact]
        public async Task OnGet_NonexistentId_ReturnsNotFound()
        {
            var mockRepo = new Mock<IFileRepository>();
            mockRepo.Setup(l => l.GetFileAsync(It.IsAny<Guid>()))
                .ReturnsAsync((FileDetailDto)null)
                .Verifiable();
            var pageModel = new Pages.Files.EditModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(default).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.Id.Should().Be((Guid)default);
            pageModel.FileLabel.Should().Be(default);
            pageModel.Active.ShouldBeFalse();
        }

        [Fact]
        public async Task OnGet_MissingId_ReturnsNotFound()
        {
            var mockRepo = new Mock<IFileRepository>();
            var pageModel = new Pages.Files.EditModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(null).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.Id.Should().Be(default(Guid));
            pageModel.FileLabel.Should().Be(default);
            pageModel.Active.ShouldBeFalse();
        }

        [Fact]
        public async Task OnPost_IfValidModel_ReturnsDetailsPage()
        {
            var file = DataHelpers.Files[0];
            var mockRepo = new Mock<IFileRepository>();
            mockRepo.Setup(l => l.FileHasActiveFacilities(It.IsAny<Guid>()))
                .ReturnsAsync(false);
            mockRepo.Setup(l => l.UpdateFileAsync(It.IsAny<Guid>(), It.IsAny<bool>()));
            mockRepo.Setup(l => l.GetFileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new FileDetailDto(file))
                .Verifiable();
            var pageModel = new Pages.Files.EditModel(mockRepo.Object)
            {
                Id = file.Id,
                Active = true
            };

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<RedirectToPageResult>();
            pageModel.ModelState.IsValid.ShouldBeTrue();
            ((RedirectToPageResult)result).PageName.Should().Be("./Details");
            ((RedirectToPageResult)result).RouteValues["id"].Should().Be(file.FileLabel);
        }

        [Fact]
        public async Task OnPost_IfInvalidModel_ReturnsPageWithInvalidModelState()
        {
            var file = DataHelpers.Files[0];
            var mockRepo = new Mock<IFileRepository>();
            mockRepo.Setup(l => l.GetFileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new FileDetailDto(file))
                .Verifiable();
            var pageModel = new Pages.Files.EditModel(mockRepo.Object)
            {
                Id = Guid.NewGuid()
            };
            pageModel.ModelState.AddModelError("Error", "Sample error description");

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeFalse();
            pageModel.ModelState["Error"].Errors[0].ErrorMessage.Should().Be("Sample error description");
        }

        [Fact]
        public async Task OnPost_IfFileHasActiveFacilities_ReturnsPageWithInvalidModelState()
        {
            var file = new FileDetailDto(DataHelpers.Files[0]);

            var mockRepo = new Mock<IFileRepository>();
            mockRepo.Setup(l => l.FileHasActiveFacilities(It.IsAny<Guid>()))
                .ReturnsAsync(true)
                .Verifiable();
            mockRepo.Setup(l => l.GetFileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(file);

            var pageModel = new Pages.Files.EditModel(mockRepo.Object)
            {
                Id = Guid.NewGuid()
            };

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<RedirectToPageResult>();
            pageModel.ModelState.IsValid.ShouldBeTrue();
            ((RedirectToPageResult)result).PageName.Should().Be("./Details");
            ((RedirectToPageResult)result).RouteValues["id"].Should().Be(file.FileLabel);
        }
    }
}
