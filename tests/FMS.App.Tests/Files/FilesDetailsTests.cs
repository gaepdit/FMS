﻿using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Pages.Files;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NSubstitute;
using TestHelpers;
using NUnit.Framework;

namespace FMS.App.Tests.Files
{
    public class FilesDetailsTests
    {
        [Test]
        public async Task OnGet_PopulatesThePageModel()
        {
            var file = RepositoryData.Files[0];

            var mockRepository = Substitute.For<IFileRepository>();
            mockRepository.GetFileAsync(file.FileLabel).Returns(new FileDetailDto(file));
            var pageModel = new DetailsModel(mockRepository);

            var result = await pageModel.OnGetAsync(file.FileLabel).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.FileDetail.Should().BeEquivalentTo(new FileDetailDto(file));
        }

        [Test]
        public async Task OnGet_NonexistentIdReturnsNotFound()
        {
            var mockRepository = Substitute.For<IFileRepository>();
            mockRepository.GetFileAsync(Arg.Any<string>()).Returns((FileDetailDto) null);

            var pageModel = new DetailsModel(mockRepository);

            var result = await pageModel.OnGetAsync("Test").ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.FileDetail.Should().BeNull();
        }

        [Test]
        public async Task OnGet_MissingIdReturnsNotFound()
        {
            var mockRepository = Substitute.For<IFileRepository>();
            var pageModel = new DetailsModel(mockRepository);

            var result = await pageModel.OnGetAsync(null).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.FileDetail.Should().BeNull();
        }
    }
}
