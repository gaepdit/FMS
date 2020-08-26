using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading.Tasks;
using TestHelpers;
using Xunit;

namespace FMS.Tests.Facilities
{
    public class FacilityEditTests
    {
        [Fact(Skip = "Not implemented yet")]
        public async Task OnGet_PopulatesThePageModel()
        {
            // Remove after FmsDbContext in Page has been replaced with repositories
            var optionsBuilder = new DbContextOptionsBuilder<FmsDbContext>()
               .UseInMemoryDatabase("InMemoryDb");
            var mockContext = new Mock<FmsDbContext>(optionsBuilder.Options);

            Guid facilityId = DataHelpers.Facilities[0].Id;
            var facility = DataHelpers.GetFacilityDetail(facilityId);

            var mockRepo = new Mock<IFacilityRepository>();
            mockRepo.Setup(l => l.GetFacilityAsync(It.IsAny<Guid>()))
                .ReturnsAsync(facility)
                .Verifiable();

            var pageModel = new Pages.Facilities.EditModel(mockRepo.Object, mockContext.Object);

            // This fails because FmsDbContext.Counties not fully mocked
            var result = await pageModel.OnGetAsync(facility.Id).ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.ShowSuccessMessage.Should().BeFalse();
            pageModel.Id.Should().Be(facility.Id);
            pageModel.Facility.Should().BeEquivalentTo(new FacilityEditDto(facility));
        }

        [Fact]
        public async Task OnGet_NonexistantIdReturnsNotFound()
        {
            // Remove after FmsDbContext in Page has been replaced with repositories
            var optionsBuilder = new DbContextOptionsBuilder<FmsDbContext>()
               .UseInMemoryDatabase("InMemoryDb");
            var mockContext = new Mock<FmsDbContext>(optionsBuilder.Options);

            var mockRepo = new Mock<IFacilityRepository>();
            mockRepo.Setup(l => l.GetFacilityAsync(It.IsAny<Guid>()))
                .ReturnsAsync((FacilityDetailDto)null)
                .Verifiable();

            var pageModel = new Pages.Facilities.EditModel(mockRepo.Object, mockContext.Object);

            var result = await pageModel.OnGetAsync(default).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.Id.Should().Be(default(Guid));
            pageModel.Facility.Should().BeNull();
        }

        [Fact]
        public async Task OnGet_MissingIdReturnsNotFound()
        {
            // Remove after FmsDbContext in Page has been replaced with repositories
            var optionsBuilder = new DbContextOptionsBuilder<FmsDbContext>()
               .UseInMemoryDatabase("InMemoryDb");
            var mockContext = new Mock<FmsDbContext>(optionsBuilder.Options);

            var mockRepo = new Mock<IFacilityRepository>();
            var pageModel = new Pages.Facilities.EditModel(mockRepo.Object, mockContext.Object);

            var result = await pageModel.OnGetAsync(null).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundResult>();
            pageModel.Id.Should().Be(default(Guid));
            pageModel.Facility.Should().BeNull();
        }
    }
}
