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
    public class FacilityUndeleteTests
    {
        [Fact]
        public async Task OnGet_PopulatesThePageModel()
        {
            var facilityId = RepositoryData.Facilities()[0].Id;
            var facility = ResourceHelper.GetFacilityDetail(facilityId);

            var mockRepo = new Mock<IFacilityRepository>();
            mockRepo.Setup(l => l.GetFacilityAsync(It.IsAny<Guid>()))
                .ReturnsAsync(facility)
                .Verifiable();
            var pageModel = new UndeleteModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(facility.Id).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.Id.Should().Be(facility.Id);
            pageModel.FacilityDetail.Should().BeEquivalentTo(facility);
        }

        [Fact]
        public async Task OnGet_NonexistentId_ReturnsNotFound()
        {
            var mockRepo = new Mock<IFacilityRepository>();
            mockRepo.Setup(l => l.GetFacilityAsync(It.IsAny<Guid>()))
                .ReturnsAsync((FacilityDetailDto) null)
                .Verifiable();

            var pageModel = new UndeleteModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(Guid.Empty).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.Id.Should().Be(Guid.Empty);
            pageModel.FacilityDetail.ShouldBeNull();
        }

        [Fact]
        public async Task OnGet_MissingId_ReturnsNotFound()
        {
            var mockRepo = new Mock<IFacilityRepository>();
            var pageModel = new UndeleteModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(null).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.Id.Should().Be(Guid.Empty);
            pageModel.FacilityDetail.ShouldBeNull();
        }

        [Fact]
        public async Task OnPost_IfValidModel_ReturnsDetailsPage()
        {
            var id = Guid.NewGuid();
            var mockRepo = new Mock<IFacilityRepository>();
            mockRepo.Setup(l => l.UndeleteFacilityAsync(It.IsAny<Guid>()));

            var pageModel = new UndeleteModel(mockRepo.Object)
            {
                Id = id,
            };

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<RedirectToPageResult>();
            pageModel.ModelState.IsValid.ShouldBeTrue();
            ((RedirectToPageResult) result).PageName.Should().Be("./Details");
            ((RedirectToPageResult) result).RouteValues["id"].Should().Be(id);
        }
    }
}