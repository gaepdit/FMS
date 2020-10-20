using System;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Repositories;
using FMS.Pages.Cabinets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using TestHelpers.SimpleRepository;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace FMS.App.Tests.Cabinets
{
    public class CabinetDetailsTests
    {
        [Fact]
        public async Task OnGet_PopulatesThePageModel()
        {
            var id = SimpleRepositoryData.Cabinets[0].Id;
            var item = SimpleRepositoryData.GetCabinetDetail(id);

            var mockRepo = new Mock<ICabinetRepository>();
            mockRepo.Setup(l => l.GetCabinetDetailsAsync(It.IsAny<int>()))
                .ReturnsAsync(item)
                .Verifiable();

            var pageModel = new DetailsModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(item.CabinetNumber).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.CabinetDetail.Should().BeEquivalentTo(item);
        }

        [Fact]
        public async Task OnGet_NonexistentIdReturnsNotFound()
        {
            var mockRepo = new Mock<ICabinetRepository>();
            var pageModel = new DetailsModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(int.MinValue).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.CabinetDetail.ShouldBeNull();
        }

        [Fact]
        public async Task OnGet_MissingIdReturnsNotFound()
        {
            var mockRepo = new Mock<ICabinetRepository>();
            var pageModel = new DetailsModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(null).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.CabinetDetail.ShouldBeNull();
        }
    }
}