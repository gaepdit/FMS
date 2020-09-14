﻿using FluentAssertions;
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

namespace FMS.Tests.Cabinets
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
            pageModel.ShowInactive.Should().BeFalse();
            pageModel.NewCabinetId.Should().BeNull();
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
            pageModel.ShowInactive.Should().BeTrue();
        }

        [Fact]
        public async Task OnPost_AddNew_RedirectsToDetails()
        {
            var newName = "NewCab";
            var newGuid = Guid.NewGuid();
            var expectedMessage = new DisplayMessage(Context.Success, $"Cabinet {newName} successfully created.");

            var mockRepo = new Mock<ICabinetRepository>();
            mockRepo.Setup(l => l.CabinetNameExistsAsync(It.IsAny<string>(), null))
                .ReturnsAsync(false)
                .Verifiable();
            mockRepo.Setup(l => l.CreateCabinetAsync(It.IsAny<CabinetCreateDto>()))
                .ReturnsAsync(newGuid)
                .Verifiable();
            var pageModel = new Pages.Cabinets.IndexModel(mockRepo.Object)
            {
                CabinetCreate = new CabinetCreateDto() { Name = newName }
            };

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<RedirectToPageResult>();
            ((RedirectToPageResult)result).PageName.Should().Be("Index");
            pageModel.ModelState.IsValid.Should().BeTrue();
            pageModel.ShowInactive.Should().BeFalse();
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
            pageModel.ModelState.IsValid.Should().BeFalse();
            pageModel.Cabinets.Should().BeEquivalentTo(_cabinets.Where(e => e.Active));
            pageModel.ShowInactive.Should().BeFalse();
            pageModel.NewCabinetId.Should().BeNull();
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
                CabinetCreate = new CabinetCreateDto() { Name = "NewCab" }
            };

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.Should().BeFalse();
            pageModel.ModelState["CabinetCreate.Name"].Errors[0].ErrorMessage.Should().Be("There is already a Cabinet with that name.");
            pageModel.Cabinets.Should().BeEquivalentTo(_cabinets.Where(e => e.Active));
            pageModel.ShowInactive.Should().BeFalse();
            pageModel.NewCabinetId.Should().BeNull();
        }
    }
}
