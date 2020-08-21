using FluentAssertions;
using FMS.Domain.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestHelpers;
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
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var result = await repository.FacilityExistsAsync(DataHelpers.Facilities[0].Id);
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task NonexistantFacilityDoesNotExist()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var result = await repository.FacilityExistsAsync(default);
            result.ShouldBeFalse();
        }

        // GetFacilityAsync

        [Fact]
        public async Task GetFacilityReturnsCorrectFacility()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            Guid facilityId = DataHelpers.Facilities[0].Id;

            var expected = DataHelpers.GetFacilityDetail(facilityId);
            var result = await repository.GetFacilityAsync(facilityId);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetNonexistantFacilityReturnsNull()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var result = await repository.GetFacilityAsync(default);
            result.ShouldBeNull();
        }

        // CountAsync

        [Fact(Skip ="Not implemented yet")]
        public async Task DefaultFacilityCountReturnsCountOfAll()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var result = await repository.CountAsync(new FacilitySpec());
            result.Should().Be(DataHelpers.Facilities.Length);
        }

        [Fact(Skip = "Not implemented yet")]
        public async Task FacilityCountWithSpecReturnsCorrectCount()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            int countyId = 243;
            var spec = new FacilitySpec() { CountyId = countyId };

            var expected = DataHelpers.Facilities.Count(e => e.CountyId == countyId);
            var result = await repository.CountAsync(spec);

            result.Should().Be(expected);
        }

        // TODO: GetFacilityListAsync

        // TODO: CreateFacilityAsync

        // UpdateFacilityAsync

        [Fact]
        public async Task UpdateFacilitySucceeds()
        {
            var repositoryHelper = new RepositoryHelper();
            int newCountyId = 99;
            Guid facilityId = DataHelpers.Facilities[0].Id;

            using (var repository = repositoryHelper.GetFacilityRepository())
            {
                var facility = DataHelpers.GetFacilityDetail(facilityId);
                var updates = new FacilityEditDto(facility) { CountyId = newCountyId };

                await repository.UpdateFacilityAsync(facilityId, updates);
            }

            using (var repository = repositoryHelper.GetFacilityRepository())
            {
                var expected = DataHelpers.GetFacilityDetail(facilityId);
                expected.County = DataHelpers.GetCounty(newCountyId);

                var updatedFacility = await repository.GetFacilityAsync(expected.Id);

                updatedFacility.Should().BeEquivalentTo(expected);
            }
        }

        [Fact]
        public async Task UpdateNonexistantFacilityThrowsException()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
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
