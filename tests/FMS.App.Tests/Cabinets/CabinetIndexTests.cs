using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Pages.Cabinets;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using TestHelpers.SimpleRepository;
using Xunit;

namespace FMS.App.Tests.Cabinets
{
    public class CabinetIndexTests
    {
        private readonly List<CabinetSummaryDto> _cabinets = SimpleRepositoryData.Cabinets
            .Select(e => new CabinetSummaryDto(e))
            .ToList();

        [Fact]
        public async Task OnGet_PopulatesThePageModel()
        {
            var mockRepo = new Mock<ICabinetRepository>();
            mockRepo.Setup(l => l.GetCabinetListAsync(true))
                .ReturnsAsync(_cabinets)
                .Verifiable();

            var pageModel = new IndexModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.Cabinets.Should().BeEquivalentTo(_cabinets);
        }
    }
}