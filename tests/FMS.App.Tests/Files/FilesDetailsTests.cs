﻿using System.Threading.Tasks;
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
            var file = SimpleRepositoryData.Files[0];

            var mockRepository = new Mock<IFileRepository>();
            mockRepository.Setup(l => l.GetFileAsync(file.FileLabel))
                .ReturnsAsync(new FileDetailDto(file));
            var pageModel = new DetailsModel(mockRepository.Object);

            var result = await pageModel.OnGetAsync(file.FileLabel).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.FileDetail.Should().BeEquivalentTo(new FileDetailDto(file));
        }

        [Fact]
        public async Task OnGet_NonexistentIdReturnsNotFound()
        {
            var mockRepository = new Mock<IFileRepository>();
            mockRepository.Setup(l => l.GetFileAsync(It.IsAny<string>()))
                .ReturnsAsync((FileDetailDto) null);

            var pageModel = new DetailsModel(mockRepository.Object);

            var result = await pageModel.OnGetAsync("Test").ConfigureAwait(false);

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
    }
}