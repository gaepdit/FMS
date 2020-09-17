using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestHelpers;
using Xunit;

namespace FMS.App.Tests.Cabinets
{
    public class CabinetDetailsTests
    {
        [Fact]
        public async Task OnGet_PopulatesThePageModel()
        {
            Guid id = DataHelpers.Cabinets[0].Id;
            var item = DataHelpers.GetCabinetDetail(id);

            var mockRepo = new Mock<ICabinetRepository>();
            mockRepo.Setup(l => l.GetCabinetDetailsAsync(It.IsAny<string>()))
                .ReturnsAsync(item)
                .Verifiable();

            var pageModel = new Pages.Cabinets.DetailsModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(item.Name).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.CabinetDetail.Should().BeEquivalentTo(item);
        }

        [Fact]
        public async Task OnGet_NonexistantIdReturnsNotFound()
        {
            var mockRepo = new Mock<ICabinetRepository>();
            var pageModel = new Pages.Cabinets.DetailsModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(default).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.CabinetDetail.Should().BeNull();
        }

        [Fact]
        public async Task OnGet_MissingIdReturnsNotFound()
        {
            var mockRepo = new Mock<ICabinetRepository>();
            var pageModel = new Pages.Cabinets.DetailsModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(null).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.CabinetDetail.Should().BeNull();
        }
    }
}
