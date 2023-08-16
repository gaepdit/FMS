using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Dto.PaginatedList;
using FMS.Domain.Repositories;
using FMS.Pages.Files;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NSubstitute;
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
            var items = RepositoryData.Files
                .Where(e => e.Active)
                .Select(e => new FileDetailDto(e))
                .ToList();
            var expected = new PaginatedList<FileDetailDto>(items, items.Count, 1, GlobalConstants.PageSize);

            var mockRepository = Substitute.For<IFileRepository>();
            var spec = new FileSpec();
            mockRepository.GetFileListAsync(Arg.Any<FileSpec>(), Arg.Any<int>(), Arg.Any<int>()).Returns(expected);
            var pageModel = new IndexModel(mockRepository);

            var result = await pageModel.OnGetSearchAsync(spec).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeTrue();
            pageModel.ShowResults.ShouldBeTrue();
            pageModel.FileList.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task OnSearch_NoMatch_ReturnsEmptyList()
        {
            var mockRepository = Substitute.For<IFileRepository>();
            var spec = new FileSpec();
            var expected = new PaginatedList<FileDetailDto>(new List<FileDetailDto>(), 0, 1, GlobalConstants.PageSize);
            mockRepository.GetFileListAsync(Arg.Any<FileSpec>(), Arg.Any<int>(), Arg.Any<int>()).Returns(expected);
            var pageModel = new IndexModel(mockRepository);

            var result = await pageModel.OnGetSearchAsync(spec).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeTrue();
            pageModel.ShowResults.ShouldBeTrue();
            pageModel.FileList.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task OnSearch_IfInvalidModel_ReturnPageWithInvalidModelState()
        {
            var mockRepository = Substitute.For<IFileRepository>();
            var pageModel = new IndexModel(mockRepository);
            pageModel.ModelState.AddModelError("Error", "Sample error description");

            var result = await pageModel.OnGetSearchAsync(default).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeFalse();
        }
    }
}
