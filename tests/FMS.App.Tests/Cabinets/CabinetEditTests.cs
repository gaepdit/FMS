using System;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Pages.Cabinets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NSubstitute;
using TestHelpers;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace FMS.App.Tests.Cabinets
{
    public class CabinetEditTests
    {
        [Fact]
        public async Task OnGet_PopulatesThePageModel()
        {
            var cabinet = RepositoryData.Cabinets[0];
            var expected = new CabinetSummaryDto(cabinet);
            var mockRepo = Substitute.For<ICabinetRepository>();
            mockRepo.GetCabinetSummaryAsync(cabinet.Id).Returns(expected);
            var pageModel = new EditModel(mockRepo);

            var result = await pageModel.OnGetAsync(cabinet.Id).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.Id.Should().Be(cabinet.Id);
            pageModel.OriginalCabinetName.Should().Be(cabinet.Name);
            pageModel.CabinetEdit.Should().BeEquivalentTo(new CabinetEditDto(expected));
        }

        [Fact]
        public async Task OnGet_NonexistentId_ReturnsNotFound()
        {
            var mockRepo = Substitute.For<ICabinetRepository>();
            mockRepo.GetCabinetSummaryAsync(Arg.Any<Guid>()).Returns((CabinetSummaryDto)null);

            var pageModel = new EditModel(mockRepo);

            var result = await pageModel.OnGetAsync(Guid.Empty).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.Id.Should().Be(Guid.Empty);
            pageModel.OriginalCabinetName.ShouldBeNull();
            pageModel.CabinetEdit.Should().Be(default);
        }

        [Fact]
        public async Task OnGet_MissingId_ReturnsNotFound()
        {
            var mockRepo = Substitute.For<ICabinetRepository>();
            var pageModel = new EditModel(mockRepo);

            var result = await pageModel.OnGetAsync(null).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.Id.Should().Be(Guid.Empty);
            pageModel.OriginalCabinetName.ShouldBeNull();
            pageModel.CabinetEdit.Should().Be(default);
        }

        [Fact]
        public async Task OnPost_ValidModel_ReturnsDetailsPage()
        {
            var cabinet = new CabinetSummaryDto(RepositoryData.Cabinets[0]);

            var mockRepo = Substitute.For<ICabinetRepository>();

            mockRepo.CabinetNameExistsAsync(Arg.Any<string>(), Arg.Any<Guid?>()).Returns(false);
            
            var pageModel = new EditModel(mockRepo)
            {
                Id = Guid.NewGuid(),
                CabinetEdit = new CabinetEditDto(cabinet),
            };

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            pageModel.ModelState.IsValid.ShouldBeTrue();
            result.Should().BeOfType<RedirectToPageResult>();
            ((RedirectToPageResult) result).PageName.Should().Be("./Details");
            ((RedirectToPageResult) result).RouteValues["id"].Should().Be(cabinet.Name);
        }

        [Fact]
        public async Task OnPost_InvalidModel_ReturnsPageWithInvalidModelState()
        {
            var cabinet = new CabinetSummaryDto(RepositoryData.Cabinets[0]);

            var mockRepo = Substitute.For<ICabinetRepository>();
            mockRepo.GetCabinetSummaryAsync(Arg.Any<Guid>()).Returns(cabinet);
            
            var pageModel = new EditModel(mockRepo);
            pageModel.ModelState.AddModelError("Error", "Sample error description");

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeFalse();
            pageModel.ModelState["Error"].Errors[0].ErrorMessage.Should().Be("Sample error description");
        }

        [Fact]
        public async Task OnPost_NameExists_ReturnsPageWithInvalidModelState()
        {
            var cabinet = new CabinetSummaryDto(RepositoryData.Cabinets[0]);

            var mockRepo = Substitute.For<ICabinetRepository>();
            mockRepo.CabinetNameExistsAsync(Arg.Any<string>(), Arg.Any<Guid>()).Returns(true);
            mockRepo.GetCabinetSummaryAsync(Arg.Any<Guid>()).Returns(cabinet);

            var pageModel = new EditModel(mockRepo)
            {
                Id = Guid.NewGuid(),
                CabinetEdit = new CabinetEditDto(cabinet)
            };

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeFalse();
            pageModel.ModelState["CabinetEdit.Name"].Errors[0].ErrorMessage.Should()
                .Be("There is already a Cabinet with that name.");
            pageModel.OriginalCabinetName.Should().Be(cabinet.Name);
        }

        [Fact]
        public async Task OnPost_InvalidFileLabel_ReturnsPageWithInvalidModelState()
        {
            const string newFileLabel = "abc";
            var cabinet = new CabinetSummaryDto(RepositoryData.Cabinets[0]);

            var mockRepo = Substitute.For<ICabinetRepository>();
            mockRepo.CabinetNameExistsAsync(Arg.Any<string>(), Arg.Any<Guid>()).Returns(false);
            mockRepo.GetCabinetSummaryAsync(Arg.Any<Guid>()).Returns(cabinet);

            var pageModel = new EditModel(mockRepo)
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
