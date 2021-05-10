using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Repositories;
using FMS.Pages.Cabinets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using TestHelpers;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace FMS.App.Tests.Cabinets
{
    public class CabinetDetailsTests
    {
        [Fact]
        public async Task OnGet_PopulatesThePageModel()
        {
            var id = RepositoryData.Cabinets[0].Id;
            var item = ResourceHelper.GetCabinetSummary(id);

            var mockRepo = new Mock<ICabinetRepository>();
            mockRepo.Setup(l => l.GetCabinetSummaryAsync(It.IsAny<string>()))
                .ReturnsAsync(item)
                .Verifiable();

            var pageModel = new DetailsModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(item.Name).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.CabinetSummary.Should().BeEquivalentTo(item);
        }

        [Fact]
        public async Task OnGet_NonexistentIdReturnsNotFound()
        {
            var mockRepo = new Mock<ICabinetRepository>();
            var pageModel = new DetailsModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync("zzz").ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.CabinetSummary.ShouldBeNull();
        }

        [Fact]
        public async Task OnGet_MissingIdReturnsNotFound()
        {
            var mockRepo = new Mock<ICabinetRepository>();
            var pageModel = new DetailsModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(string.Empty).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.CabinetSummary.ShouldBeNull();
        }
    }
}