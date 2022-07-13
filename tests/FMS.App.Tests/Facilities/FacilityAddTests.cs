using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Pages.Facilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace FMS.App.Tests.Facilities
{
    public class FacilityAddTests
    {
        [Fact]
        public async Task OnGet_PopulatesThePageModel()
        {
            var mockRepo = new Mock<IFacilityRepository>();
            var mockSelectListHelper = new Mock<ISelectListHelper>();
            var pageModel = new AddModel(mockRepo.Object, mockSelectListHelper.Object);

            var result = await pageModel.OnGetAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.Facility.Should().BeEquivalentTo(new FacilityCreateDto {State = "Georgia"});
        }

        [Fact]
        public async Task OnPost_ValidModel_NoNearbyFacilities_ReturnsDetailsPage()
        {
            // ReSharper disable once CollectionNeverUpdated.Local
            var nearbyFacilities = new List<FacilityMapSummaryDto>();
            var newId = Guid.NewGuid();
            var mockRepo = new Mock<IFacilityRepository>();
            mockRepo.Setup(l => l.CreateFacilityAsync(It.IsAny<FacilityCreateDto>()))
                .ReturnsAsync(newId);
            mockRepo.Setup(l => l.GetFacilityListAsync(It.IsAny<FacilityMapSpec>()))
                .ReturnsAsync(nearbyFacilities);

            var mockSelectListHelper = new Mock<ISelectListHelper>();
            var pageModel = new AddModel(mockRepo.Object, mockSelectListHelper.Object)
            {
                Facility = new FacilityCreateDto {State = "Georgia", Latitude = 0, Longitude = 0},
            };

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<RedirectToPageResult>();
            pageModel.ModelState.IsValid.ShouldBeTrue();
            pageModel.ConfirmFacility.ShouldBeFalse();
            pageModel.NearbyFacilities.Should().BeEquivalentTo(nearbyFacilities);
            ((RedirectToPageResult) result).PageName.Should().Be("./Details");
            ((RedirectToPageResult) result).RouteValues["id"].Should().Be(newId);
        }

        [Fact]
        public async Task OnPost_ValidModel_NearbyFacilities_ReturnsConfirmationPage()
        {
            var nearbyFacilities = new List<FacilityMapSummaryDto>()
            {
                new FacilityMapSummaryDto()
                {
                    Id = Guid.Empty,
                    FacilityNumber = "test",
                }
            };

            var mockRepo = new Mock<IFacilityRepository>();
            mockRepo.Setup(l => l.GetFacilityListAsync(It.IsAny<FacilityMapSpec>()))
                .ReturnsAsync(nearbyFacilities);

            var mockSelectListHelper = new Mock<ISelectListHelper>();
            var pageModel = new AddModel(mockRepo.Object, mockSelectListHelper.Object)
            {
                Facility = new FacilityCreateDto {State = "Georgia", Latitude = 0, Longitude = 0},
            };

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeTrue();
            pageModel.ConfirmFacility.ShouldBeTrue();
            pageModel.NearbyFacilities.Should().BeEquivalentTo(nearbyFacilities);
        }

        [Fact]
        public async Task OnPost_InvalidModel_ReturnsPageWithInvalidModelState()
        {
            var mockRepo = new Mock<IFacilityRepository>();
            var mockSelectListHelper = new Mock<ISelectListHelper>();
            var pageModel = new AddModel(mockRepo.Object, mockSelectListHelper.Object);
            pageModel.ModelState.AddModelError("Error", "Sample error description");

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ConfirmFacility.ShouldBeFalse();
            pageModel.ModelState.IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task OnPostConfirm_ValidModel_ReturnsDetailsPage()
        {
            var newId = Guid.NewGuid();
            var mockRepo = new Mock<IFacilityRepository>();
            mockRepo.Setup(l => l.CreateFacilityAsync(It.IsAny<FacilityCreateDto>()))
                .ReturnsAsync(newId)
                .Verifiable();
            mockRepo.Setup(l => l.FileLabelExists(It.IsAny<string>()))
                .ReturnsAsync(true);

            var mockSelectListHelper = new Mock<ISelectListHelper>();
            var pageModel = new AddModel(mockRepo.Object, mockSelectListHelper.Object)
            {
                Facility = new FacilityCreateDto {State = "Georgia"}
            };

            var result = await pageModel.OnPostConfirmAsync().ConfigureAwait(false);

            result.Should().BeOfType<RedirectToPageResult>();
            pageModel.ModelState.IsValid.ShouldBeTrue();
            pageModel.ConfirmFacility.ShouldBeFalse();
            ((RedirectToPageResult) result).PageName.Should().Be("./Details");
            ((RedirectToPageResult) result).RouteValues["id"].Should().Be(newId);
        }

        [Fact]
        public async Task OnPostConfirm_InvalidModel_ReturnsPageWithInvalidModelState()
        {
            var mockRepo = new Mock<IFacilityRepository>();
            var mockSelectListHelper = new Mock<ISelectListHelper>();
            var pageModel = new AddModel(mockRepo.Object, mockSelectListHelper.Object);
            pageModel.ModelState.AddModelError("Error", "Sample error description");

            var result = await pageModel.OnPostConfirmAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ConfirmFacility.ShouldBeFalse();
            pageModel.ModelState.IsValid.ShouldBeFalse();
        }
    }
}