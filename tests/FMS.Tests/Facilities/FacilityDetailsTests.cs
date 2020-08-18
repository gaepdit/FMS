using FluentAssertions;
using FMS.Domain.Repositories;
using FMS.Pages.Facilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using System;
using System.Threading.Tasks;
using TestHelpers;
using Xunit;

namespace FMS.Tests.Facilities
{
    public class FacilityDetailsTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        [InlineData(null)]
        public async Task OnGet_PopulatesThePageModel(bool? success)
        {
            Guid facilityId = DataHelpers.Facilities[0].Id;
            var facility = DataHelpers.GetFacilityDetail(facilityId);

            var mockRepo = new Mock<IFacilityRepository>();
            mockRepo.Setup(l => l.GetFacilityAsync(It.IsAny<Guid>()))
                .ReturnsAsync(facility)
                .Verifiable();

            var pageModel = new DetailsModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(facility.Id, success).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ShowSuccessMessage.Should().Be(success ?? false);
            pageModel.FacilityDetail.Should().BeEquivalentTo(facility);
        }

        [Fact]
        public async Task OnGet_NonexistantIdReturnsNotFound()
        {
            var mockRepo = new Mock<IFacilityRepository>();
            var pageModel = new DetailsModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(default, default).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.ShowSuccessMessage.Should().BeFalse();
            pageModel.FacilityDetail.Should().BeNull();
        }

        [Fact]
        public async Task OnGet_MissingIdReturnsNotFound()
        {
            var mockRepo = new Mock<IFacilityRepository>();
            var pageModel = new DetailsModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(null, null).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.ShowSuccessMessage.Should().BeFalse();
            pageModel.FacilityDetail.Should().BeNull();
        }
    }
}
