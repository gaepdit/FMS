using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Repositories;
using FMS.Pages.Cabinets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NSubstitute;
using TestHelpers;
using NUnit.Framework;

namespace FMS.App.Tests.Cabinets
{
    public class CabinetDetailsTests
    {
        [Test]
        public async Task OnGet_PopulatesThePageModel()
        {
            var id = RepositoryData.Cabinets[0].Id;
            var item = ResourceHelper.GetCabinetSummary(id);

            var mockRepo = Substitute.For<ICabinetRepository>();
            mockRepo.GetCabinetSummaryAsync(Arg.Any<string>()).Returns(item);

            var pageModel = new DetailsModel(mockRepo);

            var result = await pageModel.OnGetAsync(item.Name).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.CabinetSummary.Should().BeEquivalentTo(item);
        }

        [Test]
        public async Task OnGet_NonexistentIdReturnsNotFound()
        {
            var mockRepo = Substitute.For<ICabinetRepository>();
            var pageModel = new DetailsModel(mockRepo);

            var result = await pageModel.OnGetAsync("zzz").ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.CabinetSummary.Should().BeNull();
        }

        [Test]
        public async Task OnGet_MissingIdReturnsNotFound()
        {
            var mockRepo = Substitute.For<ICabinetRepository>();
            var pageModel = new DetailsModel(mockRepo);

            var result = await pageModel.OnGetAsync(string.Empty).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.CabinetSummary.Should().BeNull();
        }
    }
}
