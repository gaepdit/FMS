using System;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Repositories;
using FMS.Pages.Facilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using TestHelpers;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace FMS.App.Tests.Facilities
{
    public class FacilityDetailsTests
    {
        [Fact]
        public async Task OnGet_PopulatesThePageModel()
        {
            var facilityId = SimpleRepositoryData.Facilities[0].Id;
            var facility = SimpleRepositoryData.GetFacilityDetail(facilityId);

            var mockRepo = new Mock<IFacilityRepository>();
            mockRepo.Setup(l => l.GetFacilityAsync(It.IsAny<Guid>()))
                .ReturnsAsync(facility)
                .Verifiable();

            var pageModel = new DetailsModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(facility.Id, null).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.FacilityDetail.Should().BeEquivalentTo(facility);
        }

        [Fact]
        public async Task OnGet_NonexistentIdReturnsNotFound()
        {
            var mockRepo = new Mock<IFacilityRepository>();
            var pageModel = new DetailsModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(Guid.Empty, null).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.FacilityDetail.ShouldBeNull();
        }

        [Fact]
        public async Task OnGet_MissingIdReturnsNotFound()
        {
            var mockRepo = new Mock<IFacilityRepository>();
            var pageModel = new DetailsModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(null, null).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.FacilityDetail.ShouldBeNull();
        }
    }
}