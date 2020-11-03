using System;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Dto;
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
    public class FacilityEditTests
    {
        [Fact]
        public async Task OnGet_PopulatesThePageModel()
        {
            var facilityId = DataHelpers.Facilities[0].Id;
            var facility = DataHelpers.GetFacilityDetail(facilityId);

            var mockRepo = new Mock<IFacilityRepository>();
            mockRepo.Setup(l => l.GetFacilityAsync(It.IsAny<Guid>()))
                .ReturnsAsync(facility)
                .Verifiable();

            var mockSelectListHelper = new Mock<ISelectListHelper>();
            var pageModel = new EditModel(mockRepo.Object, mockSelectListHelper.Object);

            var result = await pageModel.OnGetAsync(facility.Id).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.Id.Should().Be(facility.Id);
            pageModel.Facility.Should().BeEquivalentTo(new FacilityEditDto(facility));
        }

        [Fact]
        public async Task OnGet_NonexistentId_ReturnsNotFound()
        {
            var mockRepo = new Mock<IFacilityRepository>();
            mockRepo.Setup(l => l.GetFacilityAsync(It.IsAny<Guid>()))
                .ReturnsAsync((FacilityDetailDto) null)
                .Verifiable();

            var mockSelectListHelper = new Mock<ISelectListHelper>();
            var pageModel = new EditModel(mockRepo.Object, mockSelectListHelper.Object);

            var result = await pageModel.OnGetAsync(Guid.Empty).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.Id.Should().Be(Guid.Empty);
            pageModel.Facility.ShouldBeNull();
        }

        [Fact]
        public async Task OnGet_MissingId_ReturnsNotFound()
        {
            var mockRepo = new Mock<IFacilityRepository>();
            var mockSelectListHelper = new Mock<ISelectListHelper>();
            var pageModel = new EditModel(mockRepo.Object, mockSelectListHelper.Object);

            var result = await pageModel.OnGetAsync(null).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.Id.Should().Be(Guid.Empty);
            pageModel.Facility.ShouldBeNull();
        }

        [Fact]
        public async Task OnPost_IfValidModel_ReturnsDetailsPage()
        {
            var id = Guid.NewGuid();
            var mockRepo = new Mock<IFacilityRepository>();
            mockRepo.Setup(l => l.UpdateFacilityAsync(It.IsAny<Guid>(), It.IsAny<FacilityEditDto>()));

            var mockSelectListHelper = new Mock<ISelectListHelper>();
            var pageModel = new EditModel(mockRepo.Object, mockSelectListHelper.Object)
            {
                Id = id,
                Facility = new FacilityEditDto()
            };

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<RedirectToPageResult>();
            pageModel.ModelState.IsValid.ShouldBeTrue();
            ((RedirectToPageResult) result).PageName.Should().Be("./Details");
            ((RedirectToPageResult) result).RouteValues["id"].Should().Be(id);
        }

        [Fact]
        public async Task OnPost_IfInvalidModel_ReturnsPageWithInvalidModelState()
        {
            var mockRepo = new Mock<IFacilityRepository>();
            var mockSelectListHelper = new Mock<ISelectListHelper>();
            var pageModel = new EditModel(mockRepo.Object, mockSelectListHelper.Object);
            pageModel.ModelState.AddModelError("Error", "Sample error description");

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeFalse();
            pageModel.ModelState["Error"].Errors[0].ErrorMessage.Should().Be("Sample error description");
        }
    }
}