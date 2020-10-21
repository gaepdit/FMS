using System;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Pages.Cabinets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using TestHelpers.SimpleRepository;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace FMS.App.Tests.Cabinets
{
    public class CabinetEditTests
    {
        [Fact]
        public async Task OnGet_PopulatesThePageModel()
        {
            var cabinet = SimpleRepositoryData.Cabinets[0];
            var expected = new CabinetSummaryDto(cabinet);
            var mockRepo = new Mock<ICabinetRepository>();
            mockRepo.Setup(l => l.GetCabinetSummaryAsync(cabinet.Id))
                .ReturnsAsync(expected)
                .Verifiable();
            var pageModel = new EditModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(cabinet.Id).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.Id.Should().Be(cabinet.Id);
            pageModel.CabinetEdit.Should().BeEquivalentTo(new CabinetEditDto(expected));
        }

        [Fact]
        public async Task OnGet_NonexistentId_ReturnsNotFound()
        {
            var mockRepo = new Mock<ICabinetRepository>();
            mockRepo.Setup(l => l.GetCabinetSummaryAsync(It.IsAny<Guid>()))
                .ReturnsAsync((CabinetSummaryDto) null)
                .Verifiable();

            var pageModel = new EditModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(Guid.Empty).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.Id.Should().Be(Guid.Empty);
            pageModel.CabinetEdit.Should().Be(default);
        }

        [Fact]
        public async Task OnGet_MissingId_ReturnsNotFound()
        {
            var mockRepo = new Mock<ICabinetRepository>();
            var pageModel = new EditModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(null).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.Id.Should().Be(Guid.Empty);
            pageModel.CabinetEdit.Should().Be(default);
        }

        [Fact]
        public async Task OnPost_ValidModel_ReturnsDetailsPage()
        {
            var cabinet = new CabinetSummaryDto(SimpleRepositoryData.Cabinets[0]);

            var mockRepo = new Mock<ICabinetRepository>();
            mockRepo.Setup(l => l.UpdateCabinetAsync(It.IsAny<Guid>(), It.IsAny<CabinetEditDto>()));
            mockRepo.Setup(l => l.GetCabinetSummaryAsync(It.IsAny<Guid>()))
                .ReturnsAsync(cabinet);

            var pageModel = new EditModel(mockRepo.Object)
            {
                Id = Guid.NewGuid(),
                CabinetEdit = new CabinetEditDto(cabinet)
            };

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<RedirectToPageResult>();
            pageModel.ModelState.IsValid.ShouldBeTrue();
            ((RedirectToPageResult) result).PageName.Should().Be("./Details");
            ((RedirectToPageResult) result).RouteValues["id"].Should().Be(cabinet.CabinetNumber);
        }

        [Fact]
        public async Task OnPost_InvalidModel_ReturnsPageWithInvalidModelState()
        {
            var mockRepo = new Mock<ICabinetRepository>();
            var pageModel = new EditModel(mockRepo.Object);
            pageModel.ModelState.AddModelError("Error", "Sample error description");

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeFalse();
            pageModel.ModelState["Error"].Errors[0].ErrorMessage.Should().Be("Sample error description");
        }

        [Fact]
        public async Task OnPost_InvalidFileLabel_ReturnsPageWithInvalidModelState()
        {
            const string newFileLabel = "abc";
            var mockRepo = new Mock<ICabinetRepository>();
            var cabinet = new CabinetSummaryDto(SimpleRepositoryData.Cabinets[0]);

            var pageModel = new EditModel(mockRepo.Object)
            {
                Id = Guid.NewGuid(),
                CabinetEdit = new CabinetEditDto(cabinet)
                {
                    FirstFileLabel = newFileLabel
                }
            };

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeFalse();
            pageModel.ModelState["CabinetEdit.FirstFileLabel"].Errors[0].ErrorMessage.Should()
                .Be("The File Label is invalid.");
        }
    }
}