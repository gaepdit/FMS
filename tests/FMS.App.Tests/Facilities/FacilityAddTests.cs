using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using System;
using System.Threading.Tasks;
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
            var pageModel = new Pages.Facilities.AddModel(mockRepo.Object, mockSelectListHelper.Object);

            var result = await pageModel.OnGetAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.Facility.Should().BeEquivalentTo(new FacilityCreateDto {State = "Georgia"});
        }

        [Fact]
        public async Task OnPost_IfValidModel_ReturnsDetailsPage()
        {
            var newId = Guid.NewGuid();
            var mockRepo = new Mock<IFacilityRepository>();
            mockRepo.Setup(l => l.CreateFacilityAsync(It.IsAny<FacilityCreateDto>()))
                .ReturnsAsync(newId)
                .Verifiable();
            mockRepo.Setup(l => l.FileLabelExists(It.IsAny<string>()))
                .ReturnsAsync(true);

            var mockSelectListHelper = new Mock<ISelectListHelper>();
            var pageModel = new Pages.Facilities.AddModel(mockRepo.Object, mockSelectListHelper.Object)
            {
                Facility = new FacilityCreateDto {State = "Georgia"}
            };

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<RedirectToPageResult>();
            pageModel.ModelState.IsValid.ShouldBeTrue();
            ((RedirectToPageResult) result).PageName.Should().Be("./Details");
            ((RedirectToPageResult) result).RouteValues["id"].Should().Be(newId);
        }

        [Fact]
        public async Task OnPost_IfInvalidModel_ReturnsPageWithInvalidModelState()
        {
            var mockRepo = new Mock<IFacilityRepository>();
            var mockSelectListHelper = new Mock<ISelectListHelper>();
            var pageModel = new Pages.Facilities.AddModel(mockRepo.Object, mockSelectListHelper.Object);
            pageModel.ModelState.AddModelError("Error", "Sample error description");

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeFalse();
        }
    }
}