using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHelpers;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace FMS.App.Tests.Files
{
    public class FilesIndexTests
    {
        [Fact]
        public async Task OnSearch_DefaultSpec_ReturnsActiveFiles()
        {
            var expected = DataHelpers.Files
                .Where(e => e.Active)
                .Select(e => new FileDetailDto(e))
                .ToList();
            var mockRepository = new Mock<IFileRepository>();
            var spec = new FileSpec();
            mockRepository.Setup(l => l.GetFileListAsync(spec))
                .ReturnsAsync(expected).Verifiable();
            var pageModel = new Pages.Files.IndexModel(mockRepository.Object);

            var result = await pageModel.OnGetSearchAsync(spec).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeTrue();
            pageModel.ShowResults.ShouldBeTrue();
            pageModel.Files.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task OnSearch_NoMatch_ReturnsEmptyList()
        {
            var mockRepository = new Mock<IFileRepository>();
            var spec = new FileSpec();
            mockRepository.Setup(l => l.GetFileListAsync(spec))
                .ReturnsAsync(new List<FileDetailDto>()).Verifiable();
            var pageModel = new Pages.Files.IndexModel(mockRepository.Object);

            var result = await pageModel.OnGetSearchAsync(spec).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeTrue();
            pageModel.ShowResults.ShouldBeTrue();
            pageModel.Files.Should().BeEquivalentTo(new List<FileDetailDto>());
        }

        [Fact]
        public async Task OnSearch_IfInvalidModel_ReturnPageWithInvalidModelState()
        {
            var mockRepository = new Mock<IFileRepository>();
            var pageModel = new Pages.Files.IndexModel(mockRepository.Object);
            pageModel.ModelState.AddModelError("Error", "Sample error description");

            var result = await pageModel.OnGetSearchAsync(default).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeFalse();
        }
    }
}
