using System;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Pages.Cabinets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace FMS.App.Tests.Cabinets
{
    public class CabinetAddTests
    {
        [Fact]
        public async Task OnPost_ValidModel_ReturnsDetailsPage()
        {
            var mockRepo = new Mock<ICabinetRepository>();
            mockRepo.Setup(l => l.CabinetNameExistsAsync(It.IsAny<string>(), It.IsAny<Guid?>()))
                .ReturnsAsync(false);
            mockRepo.Setup(l => l.CreateCabinetAsync(It.IsAny<CabinetEditDto>()));

            var newCabinet = new CabinetEditDto()
            {
                Name = "New Cabinet",
                FirstFileLabel = "999-9999",
            };

            var pageModel = new AddModel(mockRepo.Object) {NewCabinet = newCabinet};

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<RedirectToPageResult>();
            pageModel.ModelState.IsValid.ShouldBeTrue();
            ((RedirectToPageResult) result).PageName.Should().Be("./Details");
            ((RedirectToPageResult) result).RouteValues["id"].Should().Be(newCabinet.Name);
        }

        [Fact]
        public async Task OnPost_IfInvalidModel_ReturnsPageWithInvalidModelState()
        {
            var mockRepo = new Mock<ICabinetRepository>();
            var pageModel = new AddModel(mockRepo.Object);
            pageModel.ModelState.AddModelError("Error", "Sample error description");

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeFalse();
        }
    }
}