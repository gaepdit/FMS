using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Pages.Facilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
using System.Security.Principal;

namespace FMS.App.Tests.Facilities
{
    public class FacilityAddTests
    {
        [Test]
        public async Task OnGet_PopulatesThePageModel()
        {
            var mockRepo = Substitute.For<IFacilityRepository>();
            var mockType = Substitute.For<IFacilityTypeRepository>();
            var mockStatus = Substitute.For<IFacilityStatusRepository>();
            var mockSelectListHelper = Substitute.For<ISelectListHelper>();

            // Mock user & page context
            var httpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new GenericIdentity("Name")) };
            var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor());
            var pageContext = new PageContext(actionContext);

            var pageModel = new AddModel(mockRepo, mockType, mockStatus, mockSelectListHelper) { PageContext = pageContext };

            var result = await pageModel.OnGetAsync();

            result.Should().BeOfType<PageResult>();
            pageModel.Facility.Should().BeEquivalentTo(new FacilityCreateDto { State = "Georgia" });
        }

        [Test]
        public async Task OnPost_ValidModel_NoNearbyFacilities_ReturnsDetailsPage()
        {
            var nearbyFacilities = new List<FacilityMapSummaryDto>();
            var newId = Guid.NewGuid();
            var mockRepo = Substitute.For<IFacilityRepository>();
            var mockType = Substitute.For<IFacilityTypeRepository>();
            var mockStatus = Substitute.For<IFacilityStatusRepository>();
            mockRepo.CreateFacilityAsync(Arg.Any<FacilityCreateDto>()).Returns(newId);
            mockRepo.GetFacilityListAsync(Arg.Any<FacilityMapSpec>()).Returns(nearbyFacilities);

            var mockSelectListHelper = Substitute.For<ISelectListHelper>();
            var pageModel = new AddModel(mockRepo, mockType, mockStatus, mockSelectListHelper)
            {
                Facility = new FacilityCreateDto
                    { State = "Georgia", Latitude = 31, Longitude = -81, FacilityNumber = "a" },
            };

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<RedirectToPageResult>();
            pageModel.ModelState.IsValid.Should().BeTrue();
            pageModel.ConfirmFacility.Should().BeFalse();
            pageModel.NearbyFacilities.Should().BeEquivalentTo(nearbyFacilities);
            ((RedirectToPageResult)result).PageName.Should().Be("./Details");
            ((RedirectToPageResult)result).RouteValues["id"].Should().Be(newId);
        }

        [Test]
        public async Task OnPost_ValidModel_NearbyFacilities_ReturnsConfirmationPage()
        {
            var nearbyFacilities = new List<FacilityMapSummaryDto>
            {
                new FacilityMapSummaryDto
                {
                    Id = Guid.Empty,
                    FacilityNumber = "test",
                }
            };

            var mockRepo = Substitute.For<IFacilityRepository>();
            var mockType = Substitute.For<IFacilityTypeRepository>();
            var mockStatus = Substitute.For<IFacilityStatusRepository>();
            mockRepo.GetFacilityListAsync(Arg.Any<FacilityMapSpec>()).Returns(nearbyFacilities);

            var mockSelectListHelper = Substitute.For<ISelectListHelper>();
            var pageModel = new AddModel(mockRepo, mockType, mockStatus, mockSelectListHelper)
            {
                Facility = new FacilityCreateDto
                    { State = "Georgia", Latitude = 31, Longitude = -81, FacilityNumber = "a" },
            };

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.Should().BeTrue();
            pageModel.ConfirmFacility.Should().BeTrue();
            pageModel.NearbyFacilities.Should().BeEquivalentTo(nearbyFacilities);
        }

        [Test]
        public async Task OnPostConfirm_ValidModel_ReturnsDetailsPage()
        {
            var newId = Guid.NewGuid();
            var mockRepo = Substitute.For<IFacilityRepository>();
            var mockType = Substitute.For<IFacilityTypeRepository>();
            var mockStatus = Substitute.For<IFacilityStatusRepository>();
            mockRepo.CreateFacilityAsync(Arg.Any<FacilityCreateDto>()).Returns(newId);
            mockRepo.FileLabelExists(Arg.Any<string>()).Returns(true);

            var mockSelectListHelper = Substitute.For<ISelectListHelper>();
            var pageModel = new AddModel(mockRepo, mockType, mockStatus, mockSelectListHelper)
            {
                Facility = new FacilityCreateDto
                    { State = "Georgia", Latitude = 31, Longitude = -81, FacilityNumber = "a" },
            };

            var result = await pageModel.OnPostConfirmAsync().ConfigureAwait(false);

            result.Should().BeOfType<RedirectToPageResult>();
            pageModel.ModelState.IsValid.Should().BeTrue();
            pageModel.ConfirmFacility.Should().BeFalse();
            ((RedirectToPageResult)result).PageName.Should().Be("./Details");
            ((RedirectToPageResult)result).RouteValues["id"].Should().Be(newId);
        }

        [Test]
        public async Task OnPostConfirm_InvalidModel_ReturnsPageWithInvalidModelState()
        {
            var mockRepo = Substitute.For<IFacilityRepository>();
            var mockType = Substitute.For<IFacilityTypeRepository>();
            var mockSelectListHelper = Substitute.For<ISelectListHelper>();
            var mockStatus = Substitute.For<IFacilityStatusRepository>();
            var pageModel = new AddModel(mockRepo, mockType, mockStatus, mockSelectListHelper);
            pageModel.ModelState.AddModelError("Error", "Sample error description");

            var result = await pageModel.OnPostConfirmAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ConfirmFacility.Should().BeFalse();
            pageModel.ModelState.IsValid.Should().BeFalse();
        }
    }
}
