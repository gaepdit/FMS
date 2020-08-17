using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Infrastructure.Tests.DataHelpers;
using FMS.Infrastructure.Tests.RepositoryHelpers;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace FMS.Infrastructure.Tests
{
    public class FacilityRepositoryTests
    {
        // FacilityExistsAsync

        [Fact]
        public async Task ExistingFacilityExists()
        {
            using var repository = new FacilityRepositoryHelper().GetFacilityRepository();
            var result = await repository.FacilityExistsAsync(FacilityDataHelpers.Facilities[0].Id);
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task NonexistantFacilityDoesNotExist()
        {
            using var repository = new FacilityRepositoryHelper().GetFacilityRepository();
            var result = await repository.FacilityExistsAsync(default);
            result.ShouldBeFalse();
        }

        // GetFacilityAsync

        [Fact]
        public async Task GetFacilityReturnsCorrectFacility()
        {
            using var repository = new FacilityRepositoryHelper().GetFacilityRepository();
            Guid facilityId = FacilityDataHelpers.Facilities[0].Id;

            var expected = FacilityDataHelpers.GetFacilityDetail(facilityId);
            var result = await repository.GetFacilityAsync(facilityId);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetNonexistantFacilityReturnsNull()
        {
            using var repository = new FacilityRepositoryHelper().GetFacilityRepository();
            var result = await repository.GetFacilityAsync(default);
            result.ShouldBeNull();
        }

        // CountAsync

        [Fact]
        public async Task DefaultFacilityCountReturnsCountOfAll()
        {
            using var repository = new FacilityRepositoryHelper().GetFacilityRepository();
            var result = await repository.CountAsync(new FacilitySpec());
            result.Should().Be(FacilityDataHelpers.Facilities.Length);
        }

        [Fact]
        public async Task FacilityCountWithSpecReturnsCorrectCount()
        {
            using var repository = new FacilityRepositoryHelper().GetFacilityRepository();
            int countyId = 243;
            var spec = new FacilitySpec() { CountyId = countyId };

            var expected = FacilityDataHelpers.Facilities.Count(e => e.CountyId == countyId);
            var result = await repository.CountAsync(spec);

            result.Should().Be(expected);
        }

        // TODO: GetFacilityListAsync

        // TODO: CreateFacilityAsync

        // UpdateFacilityAsync

        [Fact]
        public async Task UpdateFacilitySucceeds()
        {
            var repositoryHelper = new FacilityRepositoryHelper();
            int newCountyId = 99;
            Guid facilityId = FacilityDataHelpers.Facilities[0].Id;

            using (var repository = repositoryHelper.GetFacilityRepository())
            {
                var facility = FacilityDataHelpers.GetFacilityDetail(facilityId);
                var updates = new FacilityEditDto(facility) { CountyId = newCountyId };

                await repository.UpdateFacilityAsync(facilityId, updates);
            }

            using (var repository = repositoryHelper.GetFacilityRepository())
            {
                var expected = FacilityDataHelpers.GetFacilityDetail(facilityId);
                expected.County = FacilityDataHelpers.GetCounty(newCountyId);

                var updatedFacility = await repository.GetFacilityAsync(expected.Id);

                updatedFacility.Should().BeEquivalentTo(expected);
            }
        }

        [Fact]
        public async Task UpdateNonexistantFacilityThrowsException()
        {
            using var repository = new FacilityRepositoryHelper().GetFacilityRepository();
            var updates = new FacilityEditDto() { CountyId = 99 };

            Func<Task> action = async () =>
            {
                await repository.UpdateFacilityAsync(default, updates);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("Facility ID not found (Parameter 'id')");
        }
    }
}
