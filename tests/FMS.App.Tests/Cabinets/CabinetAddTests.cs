using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using FluentAssertions;
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
        public async Task OnGet_PopulatesThePageModel()
        {
            const string newCabinetName = "C000";
            var mockRepo = new Mock<ICabinetRepository>();
            mockRepo.Setup(l => l.GetNextCabinetName())
                .ReturnsAsync(newCabinetName)
                .Verifiable();
            var pageModel = new AddModel(mockRepo.Object);

            var result = await pageModel.OnGetAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.NewCabinetName.Should().Be(newCabinetName);
        }

        [Fact]
        public async Task OnPost_AddNew_RedirectsToDetails()
        {
            var newCabinetNumber = Guid.NewGuid(); 
            var mockRepo = new Mock<ICabinetRepository>();
            mockRepo.Setup(l => l.CreateCabinetAsync())
                .ReturnsAsync(newCabinetNumber)
                .Verifiable();
            var pageModel = new AddModel(mockRepo.Object);

            var result = await pageModel.OnPostAsync().ConfigureAwait(false);

            pageModel.ModelState.IsValid.ShouldBeTrue();
            result.Should().BeOfType<RedirectToPageResult>();
            ((RedirectToPageResult) result).PageName.Should().Be("./Edit");
            ((RedirectToPageResult) result).RouteValues["id"].Should().Be(newCabinetNumber);
        }
    }
}