using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestHelpers;
using Xunit;

namespace FMS.App.Tests.Files
{
    public class FilesDetailsTests
    {
        [Fact]
        public async Task OnGet_PopulatesThePageModel()
        {
            File file = DataHelpers.Files[0];
            var mockRepository = new Mock<IFileRepository>();
            mockRepository.Setup(l => l.GetFileAsync(It.IsAny<string>()))
                .ReturnsAsync(new FileDetailDto(file))
                .Verifiable();
            mockRepository.Setup(l => l.GetCabinetsForFileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<CabinetSummaryDto>());
            mockRepository.Setup(l => l.GetCabinetsAvailableForFileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<CabinetSummaryDto>());
            var pageModel = new Pages.Files.DetailsModel(mockRepository.Object);

            var result = await pageModel.OnGetAsync(file.FileLabel).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.FileDetail.Should().BeEquivalentTo(new FileDetailDto(file));
        }

        [Fact]
        public async Task OnGet_NonexistantIdReturnsNotFound()
        {
            var mockRepository = new Mock<IFileRepository>();
            var pageModel = new Pages.Files.DetailsModel(mockRepository.Object);

            var result = await pageModel.OnGetAsync(default).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.FileDetail.Should().BeNull();
        }

        [Fact]
        public async Task OnGet_MissingIdReturnsNotFound()
        {
            var mockRepository = new Mock<IFileRepository>();
            var pageModel = new Pages.Files.DetailsModel(mockRepository.Object);

            var result = await pageModel.OnGetAsync(null).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.FileDetail.Should().BeNull();
        }
    }
}
