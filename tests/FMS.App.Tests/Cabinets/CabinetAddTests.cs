using System;
using System.Threading.Tasks;
using AwesomeAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Pages.Cabinets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NSubstitute;
using NUnit.Framework;

namespace FMS.App.Tests.Cabinets
{
    public class CabinetAddTests
    {
        [Test]
        public async Task OnPost_ValidModel_ReturnsDetailsPage()
        {
            var mockRepo = Substitute.For<ICabinetRepository>();
            mockRepo.CabinetNameExistsAsync(Arg.Any<string>(), Arg.Any<Guid?>()).Returns(false);

            var newCabinet = new CabinetEditDto()
            {
                Name = "New Cabinet",
                FirstFileLabel = "999-9999",
            };

            var pageModel = new AddModel(mockRepo) { NewCabinet = newCabinet };

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<RedirectToPageResult>();
            pageModel.ModelState.IsValid.Should().BeTrue();
            ((RedirectToPageResult) result).PageName.Should().Be("./Details");
            ((RedirectToPageResult) result).RouteValues["id"].Should().Be(newCabinet.Name);
        }

        [Test]
        public async Task OnPost_IfInvalidModel_ReturnsPageWithInvalidModelState()
        {
            var mockRepo = Substitute.For<ICabinetRepository>();
            var pageModel = new AddModel(mockRepo);
            pageModel.ModelState.AddModelError("Error", "Sample error description");

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.Should().BeFalse();
        }
    }
}
