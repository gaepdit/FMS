using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Pages.Files;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using TestHelpers.SimpleRepository;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace FMS.App.Tests.Files
{
    public class FilesEditCabinetsTests
    {
        [Fact]
        public async Task OnGet_PopulatesThePageModel()
        {
            var file = SimpleRepositoryData.Files[0];

            var mockRepository = new Mock<IFileRepository>();
            mockRepository.Setup(l => l.GetFileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new FileDetailDto(file));
            mockRepository.Setup(l => l.GetCabinetsForFileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<CabinetSummaryDto>());
            mockRepository.Setup(l => l.GetCabinetsAvailableForFileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<CabinetSummaryDto>());

            var pageModel = new EditCabinetsModel(mockRepository.Object);

            var result = await pageModel.OnGetAsync(Guid.Empty).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.FileDetail.Should().BeEquivalentTo(new FileDetailDto(file));
            pageModel.FileId.Should().Be(file.Id);
        }

        [Fact]
        public async Task OnGet_NonexistentIdReturnsNotFound()
        {
            var mockRepository = new Mock<IFileRepository>();
            var pageModel = new EditCabinetsModel(mockRepository.Object);
            mockRepository.Setup(l => l.GetFileAsync(It.IsAny<Guid>()))
                .ReturnsAsync((FileDetailDto) null);

            var result = await pageModel.OnGetAsync(Guid.Empty).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.FileDetail.ShouldBeNull();
        }

        [Fact]
        public async Task OnGet_MissingIdReturnsNotFound()
        {
            var mockRepository = new Mock<IFileRepository>();
            var pageModel = new EditCabinetsModel(mockRepository.Object);

            var result = await pageModel.OnGetAsync(null).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.FileDetail.ShouldBeNull();
        }

        // Add Cabinet

        [Fact]
        public async Task OnPostAdd_ValidModel_ReturnsPageRedirect()
        {
            var file = SimpleRepositoryData.Files[0];

            var mockRepository = new Mock<IFileRepository>();
            mockRepository.Setup(l => l.GetFileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new FileDetailDto(file));
            mockRepository.Setup(l => l.GetCabinetsForFileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<CabinetSummaryDto>());
            mockRepository.Setup(l => l.GetCabinetsAvailableForFileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<CabinetSummaryDto>());
            mockRepository.Setup(l => l.AddCabinetToFileAsync(It.IsAny<Guid>(), It.IsAny<Guid>()));

            var pageModel = new EditCabinetsModel(mockRepository.Object)
            {
                FileId = Guid.Empty,
                CabinetToAdd = Guid.Empty,
            };

            var result = await pageModel.OnPostAddCabinetAsync().ConfigureAwait(false);

            result.Should().BeOfType<RedirectToPageResult>();
            pageModel.ModelState.IsValid.ShouldBeTrue();
            pageModel.FileDetail.Should().BeEquivalentTo(new FileDetailDto(file));
            pageModel.FileId.Should().Be(file.Id);
        }

        [Fact]
        public async Task OnPostAdd_MissingCabinetId_ReturnsPageWithInvalidModelState()
        {
            var file = SimpleRepositoryData.Files[0];

            var mockRepository = new Mock<IFileRepository>();
            mockRepository.Setup(l => l.GetFileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new FileDetailDto(file));
            mockRepository.Setup(l => l.GetCabinetsForFileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<CabinetSummaryDto>());
            mockRepository.Setup(l => l.GetCabinetsAvailableForFileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<CabinetSummaryDto>());

            var pageModel = new EditCabinetsModel(mockRepository.Object)
            {
                FileId = Guid.Empty,
            };

            var result = await pageModel.OnPostAddCabinetAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeFalse();
            pageModel.ModelState["CabinetToAdd"].Errors[0].ErrorMessage.Should().Be("Select a Cabinet.");
            pageModel.FileDetail.Should().BeEquivalentTo(new FileDetailDto(file));
            pageModel.FileId.Should().Be(file.Id);
        }

        // Remove Cabinet

        [Fact]
        public async Task OnPostRemove_ValidModel_ReturnsPage()
        {
            var file = SimpleRepositoryData.Files[0];

            var mockRepository = new Mock<IFileRepository>();
            mockRepository.Setup(l => l.GetFileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new FileDetailDto(file));
            mockRepository.Setup(l => l.GetCabinetsForFileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<CabinetSummaryDto>());
            mockRepository.Setup(l => l.GetCabinetsAvailableForFileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<CabinetSummaryDto>());
            mockRepository.Setup(l => l.RemoveCabinetFromFileAsync(It.IsAny<Guid>(), It.IsAny<Guid>()));

            var pageModel = new EditCabinetsModel(mockRepository.Object)
            {
                FileId = Guid.Empty,
                CabinetToRemove = Guid.Empty,
            };

            var result = await pageModel.OnPostRemoveCabinetAsync().ConfigureAwait(false);

            result.Should().BeOfType<RedirectToPageResult>();
            pageModel.ModelState.IsValid.ShouldBeTrue();
            pageModel.FileDetail.Should().BeEquivalentTo(new FileDetailDto(file));
            pageModel.FileId.Should().Be(file.Id);
        }

        [Fact]
        public async Task OnPostRemove_MissingCabinetId_ReturnsPageWithInvalidModelState()
        {
            var file = SimpleRepositoryData.Files[0];

            var mockRepository = new Mock<IFileRepository>();
            mockRepository.Setup(l => l.GetFileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new FileDetailDto(file));
            mockRepository.Setup(l => l.GetCabinetsForFileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<CabinetSummaryDto>());
            mockRepository.Setup(l => l.GetCabinetsAvailableForFileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<CabinetSummaryDto>());

            var pageModel = new EditCabinetsModel(mockRepository.Object)
            {
                FileId = Guid.Empty,
            };

            var result = await pageModel.OnPostRemoveCabinetAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeFalse();
            pageModel.ModelState["CabinetToRemove"].Errors[0].ErrorMessage.Should().Be("Select a Cabinet.");
            pageModel.FileDetail.Should().BeEquivalentTo(new FileDetailDto(file));
            pageModel.FileId.Should().Be(file.Id);
        }
    }
}