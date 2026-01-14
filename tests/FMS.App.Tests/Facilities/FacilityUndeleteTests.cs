using System;
using System.Threading.Tasks;
using AwesomeAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Pages.Facilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NSubstitute;
using TestHelpers;
using NUnit.Framework;
using FMS.TestData.SeedData;

namespace FMS.App.Tests.Facilities
{
    public class FacilityUndeleteTests
    {
        [Test]
        public async Task OnGet_PopulatesThePageModel()
        {
            var facilityId = SeedData.GetFacilities()[0].Id;
            var facility = ResourceHelper.GetFacilityDetail(facilityId);

            var mockRepo = Substitute.For<IFacilityRepository>();
            mockRepo.GetFacilityAsync(Arg.Any<Guid>()).Returns(facility);
            var pageModel = new UndeleteModel(mockRepo);

            var result = await pageModel.OnGetAsync(facility.Id).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.Id.Should().Be(facility.Id);
            pageModel.FacilityDetail.Should().BeEquivalentTo(facility);
        }

        [Test]
        public async Task OnGet_NonexistentId_ReturnsNotFound()
        {
            var mockRepo = Substitute.For<IFacilityRepository>();
            mockRepo.GetFacilityAsync(Arg.Any<Guid>()).Returns((FacilityDetailDto) null);

            var pageModel = new UndeleteModel(mockRepo);

            var result = await pageModel.OnGetAsync(Guid.Empty).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.Id.Should().Be(Guid.Empty);
            pageModel.FacilityDetail.Should().BeNull();
        }

        [Test]
        public async Task OnGet_MissingId_ReturnsNotFound()
        {
            var mockRepo = Substitute.For<IFacilityRepository>();
            var pageModel = new UndeleteModel(mockRepo);

            var result = await pageModel.OnGetAsync(null).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.Id.Should().Be(Guid.Empty);
            pageModel.FacilityDetail.Should().BeNull();
        }

        [Test]
        public async Task OnPost_IfValidModel_ReturnsDetailsPage()
        {
            var id = Guid.NewGuid();
            var mockRepo = Substitute.For<IFacilityRepository>();
            
            var pageModel = new UndeleteModel(mockRepo)
            {
                Id = id,
            };

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<RedirectToPageResult>();
            pageModel.ModelState.IsValid.Should().BeTrue();
            ((RedirectToPageResult) result).PageName.Should().Be("./Details");
            ((RedirectToPageResult) result).RouteValues["id"].Should().Be(id);
        }
    }
}
