using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FMS.Tests.Facilities
{
    public class FacilityAddTests
    {
        [Fact]
        public async Task OnGet_PopulatesThePageModel()
        {
            var mockRepo = new Mock<IFacilityRepository>();
            var mockSelectListHelper = new Mock<ISelectListHelper>();
            var pageModel = new Pages.Facilities.AddModel(mockRepo.Object, mockSelectListHelper.Object);

            var result = await pageModel.OnGetAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.Facility.Should().BeEquivalentTo(new FacilityCreateDto { State = "Georgia" });
        }

        [Fact]
        public async Task OnPost_IfInvalidModel_ReturnPageWithInvalidModelState()
        {
            var mockRepo = new Mock<IFacilityRepository>();
            var mockSelectListHelper = new Mock<ISelectListHelper>();
            var pageModel = new Pages.Facilities.AddModel(mockRepo.Object, mockSelectListHelper.Object);
            pageModel.ModelState.AddModelError("Error", "Sample error description");

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task OnPost_IfValidModel_ReturnDetailsPage()
        {
            var newId = Guid.NewGuid();
            var mockRepo = new Mock<IFacilityRepository>();
            mockRepo.Setup(l => l.CreateFacilityAsync(It.IsAny<FacilityCreateDto>()))
                .ReturnsAsync(newId)
                .Verifiable();
            var mockSelectListHelper = new Mock<ISelectListHelper>();
            var pageModel = new Pages.Facilities.AddModel(mockRepo.Object, mockSelectListHelper.Object);

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<RedirectToPageResult>();
            pageModel.ModelState.IsValid.Should().BeTrue();
            ((RedirectToPageResult)result).PageName.Should().Be("Details");
            ((RedirectToPageResult)result).RouteValues["id"].Should().Be(newId);
        }
    }
}
