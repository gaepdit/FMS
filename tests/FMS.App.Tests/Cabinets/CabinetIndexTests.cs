using System.Threading.Tasks;
using AwesomeAssertions;
using FMS.Domain.Repositories;
using FMS.Pages.Cabinets;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NSubstitute;
using TestHelpers;
using NUnit.Framework;

namespace FMS.App.Tests.Cabinets
{
    public class CabinetIndexTests
    {
        [Test]
        public async Task OnGet_PopulatesThePageModel()
        {
            var cabinets = ResourceHelper.GetCabinetSummaries(true);

            var mockRepo = Substitute.For<ICabinetRepository>();
            mockRepo.GetCabinetListAsync(true).Returns(cabinets);

            var pageModel = new IndexModel(mockRepo);

            var result = await pageModel.OnGetAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.Cabinets.Should().BeEquivalentTo(cabinets);
        }
    }
}
