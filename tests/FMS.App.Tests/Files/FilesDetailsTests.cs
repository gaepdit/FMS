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
using TestHelpers;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace FMS.App.Tests.Files
{
    public class FilesDetailsTests
    {
        [Fact]
        public async Task OnGet_PopulatesThePageModel()
        {
            var file = DataHelpers.Files[0];
            var mockRepository = new Mock<IFileRepository>();
            mockRepository.Setup(l => l.GetFileAsync(It.IsAny<string>()))
                .ReturnsAsync(new FileDetailDto(file))
                .Verifiable();
            mockRepository.Setup(l => l.GetCabinetsForFileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<CabinetSummaryDto>());
            mockRepository.Setup(l => l.GetCabinetsAvailableForFileAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<CabinetSummaryDto>());
            var pageModel = new DetailsModel(mockRepository.Object);

            var result = await pageModel.OnGetAsync(file.FileLabel).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.FileDetail.Should().BeEquivalentTo(new FileDetailDto(file));
        }

        [Fact]
        public async Task OnGet_NonexistentIdReturnsNotFound()
        {
            var mockRepository = new Mock<IFileRepository>();
            var pageModel = new DetailsModel(mockRepository.Object);

            var result = await pageModel.OnGetAsync("abcd").ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.FileDetail.ShouldBeNull();
        }

        [Fact]
        public async Task OnGet_MissingIdReturnsNotFound()
        {
            var mockRepository = new Mock<IFileRepository>();
            var pageModel = new DetailsModel(mockRepository.Object);

            var result = await pageModel.OnGetAsync(null).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.FileDetail.ShouldBeNull();
        }

        // Todo #49: OnPostAddCabinetAsync tests

        // Todo #49: OnPostRemoveCabinetAsync tests
    }
}