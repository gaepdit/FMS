using System;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Pages.Facilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NSubstitute;
using TestHelpers;
using NUnit.Framework;

namespace FMS.App.Tests.Facilities
{
    public class FacilityEditTests
    {
        [Test]
        public async Task OnGet_PopulatesThePageModel()
        {
            var facilityId = RepositoryData.Facilities()[0].Id;
            var facility = ResourceHelper.GetFacilityDetail(facilityId);

            var mockRepo = Substitute.For<IFacilityRepository>();
            var mockType = Substitute.For<IFacilityTypeRepository>();
            mockRepo.GetFacilityAsync(Arg.Any<Guid>()).Returns(facility);

            var mockSelectListHelper = Substitute.For<ISelectListHelper>();
            var pageModel = new EditModel(mockRepo, mockType, mockSelectListHelper);

            var result = await pageModel.OnGetAsync(facility.Id);

            result.Should().BeOfType<PageResult>();
            pageModel.Id.Should().Be(facility.Id);
            pageModel.Facility.Should().BeEquivalentTo(new FacilityEditDto(facility));
        }

        [Test]
        public async Task OnGet_NonexistentId_ReturnsNotFound()
        {
            var mockRepo = Substitute.For<IFacilityRepository>();
            var mockType = Substitute.For<IFacilityTypeRepository>();
            mockRepo.GetFacilityAsync(Arg.Any<Guid>()).Returns((FacilityDetailDto)null);

            var mockSelectListHelper = Substitute.For<ISelectListHelper>();
            var pageModel = new EditModel(mockRepo, mockType, mockSelectListHelper);

            var result = await pageModel.OnGetAsync(Guid.Empty);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.Id.Should().Be(Guid.Empty);
            pageModel.Facility.Should().BeNull();
        }

        [Test]
        public async Task OnGet_MissingId_ReturnsNotFound()
        {
            var mockRepo = Substitute.For<IFacilityRepository>();
            var mockType = Substitute.For<IFacilityTypeRepository>();
            var mockSelectListHelper = Substitute.For<ISelectListHelper>();
            var pageModel = new EditModel(mockRepo, mockType, mockSelectListHelper);

            var result = await pageModel.OnGetAsync(null);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.Id.Should().Be(Guid.Empty);
            pageModel.Facility.Should().BeNull();
        }

        [Test]
        public async Task OnPost_IfValidModel_ReturnsDetailsPage()
        {
            var id = Guid.NewGuid();
            var mockRepo = Substitute.For<IFacilityRepository>();
            var mockType = Substitute.For<IFacilityTypeRepository>();

            var mockSelectListHelper = Substitute.For<ISelectListHelper>();
            var pageModel = new EditModel(mockRepo, mockType, mockSelectListHelper)
            {
                Id = id,
                Facility = new FacilityEditDto()
            };

            var result = await pageModel.OnPostAsync();

            result.Should().BeOfType<RedirectToPageResult>();
            pageModel.ModelState.IsValid.Should().BeTrue();
            ((RedirectToPageResult)result).PageName.Should().Be("./Details");
            ((RedirectToPageResult)result).RouteValues["id"].Should().Be(id);
        }

        //[Fact]
        //public async Task OnPost_IfInvalidModel_ReturnsPageWithInvalidModelState()
        //{
        //    var mockRepo = Substitute.For<IFacilityRepository>();
        //    var mockType = Substitute.For<IFacilityTypeRepository>();
        //    var mockSelectListHelper = Substitute.For<ISelectListHelper>();
        //    var pageModel = new EditModel(mockRepo, mockType, mockSelectListHelper);
        //    pageModel.ModelState.AddModelError("Error", "Sample error description");

        //    var result = await pageModel.OnPostAsync();

        //    result.Should().BeOfType<PageResult>();
        //    pageModel.ModelState.IsValid.ShouldBeFalse();
        //    pageModel.ModelState["Error"].Errors[0].ErrorMessage.Should().Be("Sample error description");
        //}

        //[Fact]
        //public async Task OnPost_IfInActiveModel_ReturnsDetailsPage()
        //{
        //    var id = Guid.NewGuid();
        //    var mockRepo = Substitute.For<IFacilityRepository>();
        //    var mockType = Substitute.For<IFacilityTypeRepository>();

        //    mockRepo.GetFacilityAsync(Arg.Any<Guid>()).Returns(new FacilityDetailDto(new Domain.Entities.Facility { Active = false, File = new Domain.Entities.File(111, 0001) }));

        //    await mockType.GetFacilityTypeAsync(new Guid("B7224976-5D67-40F8-8112-273AE3B91419"));

        //    var mockSelectListHelper = Substitute.For<ISelectListHelper>();
        //    var pageModel = new EditModel(mockRepo, mockType, mockSelectListHelper) { Id = id };

        //    var result = await pageModel.OnPostAsync();

        //    result.Should().BeOfType<RedirectToPageResult>();
        //    pageModel.ModelState.IsValid.ShouldBeTrue();
        //    ((RedirectToPageResult)result).PageName.Should().Be("./Details");
        //    ((RedirectToPageResult)result).RouteValues["id"].Should().Be(id);
        //}
    }
}
