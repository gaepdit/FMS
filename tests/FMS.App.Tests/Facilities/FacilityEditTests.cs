using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using System;
using System.Threading.Tasks;
using TestHelpers;
using Xunit;

namespace FMS.Tests.Facilities
{
    public class FacilityEditTests
    {
        [Fact]
        public async Task OnGet_PopulatesThePageModel()
        {
            Guid facilityId = DataHelpers.Facilities[0].Id;
            var facility = DataHelpers.GetFacilityDetail(facilityId);

            var mockRepo = new Mock<IFacilityRepository>();
            mockRepo.Setup(l => l.GetFacilityAsync(It.IsAny<Guid>()))
                .ReturnsAsync(facility)
                .Verifiable();

            var mockSelectListHelper = new Mock<ISelectListHelper>();
            var pageModel = new Pages.Facilities.EditModel(mockRepo.Object, mockSelectListHelper.Object);

            var result = await pageModel.OnGetAsync(facility.Id).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.Id.Should().Be(facility.Id);
            pageModel.Facility.Should().BeEquivalentTo(new FacilityEditDto(facility));
        }

        [Fact]
        public async Task OnGet_NonexistentIdReturnsNotFound()
        {
            var mockRepo = new Mock<IFacilityRepository>();
            mockRepo.Setup(l => l.GetFacilityAsync(It.IsAny<Guid>()))
                .ReturnsAsync((FacilityDetailDto)null)
                .Verifiable();

            var mockSelectListHelper = new Mock<ISelectListHelper>();
            var pageModel = new Pages.Facilities.EditModel(mockRepo.Object, mockSelectListHelper.Object);

            var result = await pageModel.OnGetAsync(default).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.Id.Should().Be(default(Guid));
            pageModel.Facility.Should().BeNull();
        }

        [Fact]
        public async Task OnGet_MissingIdReturnsNotFound()
        {
            var mockRepo = new Mock<IFacilityRepository>();
            var mockSelectListHelper = new Mock<ISelectListHelper>();
            var pageModel = new Pages.Facilities.EditModel(mockRepo.Object, mockSelectListHelper.Object);

            var result = await pageModel.OnGetAsync(null).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.Id.Should().Be(default(Guid));
            pageModel.Facility.Should().BeNull();
        }
    }
}
