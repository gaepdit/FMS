using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHelpers;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace FMS.App.Tests.Cabinets
{
    public class CabinetIndexTests
    {
        private readonly List<CabinetSummaryDto> _cabinets = DataHelpers.Cabinets
            .Select(e => new CabinetSummaryDto(e))
            .ToList();

        [Fact]
        public async Task OnGet_PopulatesThePageModel()
        {
            var mockRepo = new Mock<ICabinetRepository>();
            mockRepo.Setup(l => l.GetCabinetListAsync(false))
                .ReturnsAsync(_cabinets.Where(e => e.Active).ToList())
                .Verifiable();

            var pageModel = new Pages.Cabinets.IndexModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(false).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.Cabinets.Should().BeEquivalentTo(_cabinets.Where(e => e.Active));
            pageModel.ShowInactive.ShouldBeFalse();
            pageModel.NewCabinetId.ShouldBeNull();
        }

        [Fact]
        public async Task OnGet_IncludeInactive_PopulatesThePageModel()
        {
            var mockRepo = new Mock<ICabinetRepository>();
            mockRepo.Setup(l => l.GetCabinetListAsync(true))
                .ReturnsAsync(_cabinets)
                .Verifiable();

            var pageModel = new Pages.Cabinets.IndexModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync(true).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.Cabinets.Should().BeEquivalentTo(_cabinets);
            pageModel.ShowInactive.ShouldBeTrue();
        }

        [Fact]
        public async Task OnPost_AddNew_RedirectsToDetails()
        {
            const string newName = "NewCab";
            var newGuid = Guid.NewGuid();

            var mockRepo = new Mock<ICabinetRepository>();
            mockRepo.Setup(l => l.CabinetNameExistsAsync(It.IsAny<string>(), null))
                .ReturnsAsync(false)
                .Verifiable();
            mockRepo.Setup(l => l.CreateCabinetAsync(It.IsAny<CabinetCreateDto>()))
                .ReturnsAsync(newGuid)
                .Verifiable();
            var pageModel = new Pages.Cabinets.IndexModel(mockRepo.Object)
            {
                CabinetCreate = new CabinetCreateDto() {Name = newName}
            };

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<RedirectToPageResult>();
            ((RedirectToPageResult) result).PageName.Should().Be("./Index");
            pageModel.ModelState.IsValid.ShouldBeTrue();
            pageModel.ShowInactive.ShouldBeFalse();
            pageModel.NewCabinetId.Should().Be(newGuid);
        }

        [Fact]
        public async Task OnPost_InvalidModel_ReturnsPage()
        {
            var mockRepo = new Mock<ICabinetRepository>();
            mockRepo.Setup(l => l.GetCabinetListAsync(false))
                .ReturnsAsync(_cabinets.Where(e => e.Active).ToList());
            var pageModel = new Pages.Cabinets.IndexModel(mockRepo.Object);
            pageModel.ModelState.AddModelError("Error", "Sample error description");

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeFalse();
            pageModel.Cabinets.Should().BeEquivalentTo(_cabinets.Where(e => e.Active));
            pageModel.ShowInactive.ShouldBeFalse();
            pageModel.NewCabinetId.ShouldBeNull();
        }

        [Fact]
        public async Task OnPost_AddExistingName_ReturnsModelError()
        {
            var mockRepo = new Mock<ICabinetRepository>();
            mockRepo.Setup(l => l.GetCabinetListAsync(false))
                .ReturnsAsync(_cabinets.Where(e => e.Active).ToList());
            mockRepo.Setup(l => l.CabinetNameExistsAsync(It.IsAny<string>(), null))
                .ReturnsAsync(true)
                .Verifiable();
            var pageModel = new Pages.Cabinets.IndexModel(mockRepo.Object)
            {
                CabinetCreate = new CabinetCreateDto() {Name = "NewCab"}
            };

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.ShouldBeFalse();
            pageModel.ModelState["CabinetCreate.Name"].Errors[0].ErrorMessage.Should()
                .Be("There is already a Cabinet with that name.");
            pageModel.Cabinets.Should().BeEquivalentTo(_cabinets.Where(e => e.Active));
            pageModel.ShowInactive.ShouldBeFalse();
            pageModel.NewCabinetId.ShouldBeNull();
        }
    }
}