using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Repositories;
using FMS.Pages.Cabinets;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using TestHelpers;
using Xunit;

namespace FMS.App.Tests.Cabinets
{
    public class CabinetIndexTests
    {
        [Fact]
        public async Task OnGet_PopulatesThePageModel()
        {
            var cabinets = SimpleRepositoryData.GetCabinetSummaries(true);

            var mockRepo = new Mock<ICabinetRepository>();
            mockRepo.Setup(l => l.GetCabinetListAsync(true))
                .ReturnsAsync(cabinets)
                .Verifiable();

            var pageModel = new IndexModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.Cabinets.Should().BeEquivalentTo(cabinets);
        }
    }
}