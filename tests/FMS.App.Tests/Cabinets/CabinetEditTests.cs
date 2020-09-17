using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using System;
using System.Threading.Tasks;
using TestHelpers;
using Xunit;

namespace FMS.App.Tests.Cabinets
{
    public class CabinetEditTests
    {
        [Fact]
        public async Task OnGet_PopulatesThePageModel()
        {
            var cabinet = DataHelpers.Cabinets[0];
            var expected = new CabinetSummaryDto(cabinet);
            var mockRepo = new Mock<ICabinetRepository>();
            mockRepo.Setup(l => l.GetCabinetSummaryAsync(cabinet.Id))
                .ReturnsAsync(expected)
                .Verifiable();
            var pageModel = new Pages.Cabinets.EditModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(cabinet.Id).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.Id.Should().Be(cabinet.Id);
            pageModel.OriginalCabinetName.Should().Be(cabinet.Name);
            pageModel.CabinetEdit.Should().BeEquivalentTo(new CabinetEditDto(expected));
        }

        [Fact]
        public async Task OnGet_NonexistentId_ReturnsNotFound()
        {
            var mockRepo = new Mock<ICabinetRepository>();
            mockRepo.Setup(l => l.GetCabinetSummaryAsync(It.IsAny<Guid>()))
                .ReturnsAsync((CabinetSummaryDto)null)
                .Verifiable();

            var pageModel = new Pages.Cabinets.EditModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(default).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.Id.Should().Be(default(Guid));
            pageModel.OriginalCabinetName.Should().BeNull();
            pageModel.CabinetEdit.Should().Be(default);
        }

        [Fact]
        public async Task OnGet_MissingId_ReturnsNotFound()
        {
            var mockRepo = new Mock<ICabinetRepository>();
            var pageModel = new Pages.Cabinets.EditModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(null).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.Id.Should().Be(default(Guid));
            pageModel.OriginalCabinetName.Should().BeNull();
            pageModel.CabinetEdit.Should().Be(default);
        }

        [Fact]
        public async Task OnPost_IfValidModel_ReturnsDetailsPage()
        {
            var newName = "NewCab";
            var mockRepo = new Mock<ICabinetRepository>();
            mockRepo.Setup(l => l.UpdateCabinetAsync(It.IsAny<Guid>(), It.IsAny<CabinetEditDto>()));
            mockRepo.Setup(l => l.CabinetNameExistsAsync(It.IsAny<string>(), It.IsAny<Guid>()))
                .ReturnsAsync(false)
                .Verifiable();

            var pageModel = new Pages.Cabinets.EditModel(mockRepo.Object)
            {
                Id = Guid.NewGuid(),
                CabinetEdit = new CabinetEditDto() { Name = newName }
            };

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<RedirectToPageResult>();
            pageModel.ModelState.IsValid.Should().BeTrue();
            ((RedirectToPageResult)result).PageName.Should().Be("./Details");
            ((RedirectToPageResult)result).RouteValues["id"].Should().Be(newName);
        }

        [Fact]
        public async Task OnPost_IfInvalidModel_ReturnsPageWithInvalidModelState()
        {
            var mockRepo = new Mock<ICabinetRepository>();
            var pageModel = new Pages.Cabinets.EditModel(mockRepo.Object);
            pageModel.ModelState.AddModelError("Error", "Sample error description");

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.Should().BeFalse();
            pageModel.ModelState["Error"].Errors[0].ErrorMessage.Should().Be("Sample error description");
        }

        [Fact]
        public async Task OnPost_IfNameExists_ReturnsPageWithInvalidModelState()
        {
            var cabinet = new CabinetSummaryDto(DataHelpers.Cabinets[0]);

            var mockRepo = new Mock<ICabinetRepository>();
            mockRepo.Setup(l => l.GetCabinetSummaryAsync(It.IsAny<Guid>()))
                .ReturnsAsync(cabinet);
            mockRepo.Setup(l => l.CabinetNameExistsAsync(It.IsAny<string>(), It.IsAny<Guid>()))
                .ReturnsAsync(true)
                .Verifiable();

            var pageModel = new Pages.Cabinets.EditModel(mockRepo.Object)
            {
                Id = Guid.NewGuid(),
                CabinetEdit = new CabinetEditDto(cabinet)
            };

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.Should().BeFalse();
            pageModel.ModelState["CabinetEdit.Name"].Errors[0].ErrorMessage.Should().Be("There is already a Cabinet with that name.");
            pageModel.OriginalCabinetName.Should().Be(cabinet.Name);
        }
    }
}
