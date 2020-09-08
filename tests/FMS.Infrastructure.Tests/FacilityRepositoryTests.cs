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
        public async Task ExistingFacility_Exists_ReturnsTrue()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var result = await repository.FacilityExistsAsync(DataHelpers.Facilities[0].Id);
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task NonexistantFacility_Exists_ReturnsFalse()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var result = await repository.FacilityExistsAsync(default);
            result.ShouldBeFalse();
        }

        // GetFacilityAsync

        [Fact]
        public async Task GetFacility_ReturnsCorrectFacility()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            Guid facilityId = DataHelpers.Facilities[0].Id;

            var expected = DataHelpers.GetFacilityDetail(facilityId);
            var result = await repository.GetFacilityAsync(facilityId);
            // TODO: Following line makes test work, but need to decide whether child property should be returned or not
            result.File.Facilities = null;

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetNonexistantFacility_ReturnsNull()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var result = await repository.GetFacilityAsync(default);
            result.ShouldBeNull();
        }

        // CountAsync

        [Fact(Skip = "CountAsync not implemented yet")]
        public async Task FacilityCount_DefaultSpec_ReturnsCorrectCount()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var result = await repository.CountAsync(new FacilitySpec());
            result.Should().Be(DataHelpers.Facilities.Length);
        }

        [Theory(Skip = "CountAsync not implemented yet")]
        [InlineData(243)]
        [InlineData(131)]
        public async Task FacilityCount_ByCounty_ReturnsCorrectCount(int countyId)
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() { CountyId = countyId };

            var expected = DataHelpers.Facilities.Count(e => e.CountyId == countyId);
            var result = await repository.CountAsync(spec);

            result.Should().Be(expected);
        }

        [Theory(Skip = "CountAsync not implemented yet")]
        [InlineData("00")]
        [InlineData("BRF")]
        public async Task FacilityCount_ByFacilityNumber_ReturnsCorrectCount(string facilityNumber)
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() { FacilityNumber = facilityNumber };

            var expected = DataHelpers.Facilities.Count(e => e.FacilityNumber.Contains(facilityNumber));
            var result = await repository.CountAsync(spec);

            result.Should().Be(expected);
        }

        // GetFacilityListAsync

        [Theory]
        [InlineData(243)]
        [InlineData(131)]
        public async Task FacilitySearch_ByCounty_ReturnsCorrectList(int countyId)
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() { CountyId = countyId };

            var expected = DataHelpers.Facilities.Where(e => e.CountyId == countyId)
                .Select(e => new FacilitySummaryDto(e));
            var result = await repository.GetFacilityListAsync(spec);

            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("00")]
        [InlineData("BRF")]
        public async Task FacilitySearch_ByFacilityNumber_ReturnsCorrectList(string facilityNumber)
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() { FacilityNumber = facilityNumber };

            var expected = DataHelpers.Facilities.Where(e => e.FacilityNumber.Contains(facilityNumber))
                .Select(e => new FacilitySummaryDto(e));
            var result = await repository.GetFacilityListAsync(spec);

            result.Should().BeEquivalentTo(expected);
        }

        // CreateFacilityAsync

        [Fact]
        public async Task CreateFacility_Succeeds()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();

            Guid facilityId = DataHelpers.Facilities[0].Id;
            var sampleFacility = await repository.GetFacilityAsync(facilityId);
            var newFacility = new FacilityCreateDto()
            {
                Address = sampleFacility.Address,
                BudgetCodeId = sampleFacility.BudgetCode.Id,
                City = sampleFacility.City,
                ComplianceOfficerId = sampleFacility.ComplianceOfficer.Id,
                CountyId = sampleFacility.County.Id,
                EnvironmentalInterestId = sampleFacility.EnvironmentalInterest.Id,
                FacilityNumber = sampleFacility.FacilityNumber,
                FacilityStatusId = sampleFacility.FacilityStatus.Id,
                FacilityTypeId = sampleFacility.FacilityType.Id,
                FileId = sampleFacility.File.Id,
                Latitude = sampleFacility.Latitude,
                Location = sampleFacility.Location,
                Longitude = sampleFacility.Longitude,
                Name = sampleFacility.Name,
                OrganizationalUnitId = sampleFacility.OrganizationalUnit.Id,
                PostalCode = sampleFacility.PostalCode,
                State = sampleFacility.State
            };

            var result = await repository.CreateFacilityAsync(newFacility);
            var createdFacility = await repository.GetFacilityAsync(result);

            // Clear File.Facilities sub-property since it is cyclic
            createdFacility.File.Facilities = null;
            sampleFacility.File.Facilities = null;

            // Set sample facility properties to match
            sampleFacility.Id = result;
            createdFacility.Should().BeEquivalentTo(sampleFacility);
        }

        // UpdateFacilityAsync

        [Fact]
        public async Task UpdateFacilityCounty_Succeeds()
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
                // TODO: Following line makes test work, but need to decide whether child property should be returned or not
                updatedFacility.File.Facilities = null;

                updatedFacility.Should().BeEquivalentTo(expected);
            }
        }

        [Fact]
        public async Task UpdateFacilityState_Succeeds()
        {
            var repositoryHelper = new RepositoryHelper();
            string newState = "Alabama";
            Guid facilityId = DataHelpers.Facilities[0].Id;

            using (var repository = repositoryHelper.GetFacilityRepository())
            {
                var facility = DataHelpers.GetFacilityDetail(facilityId);
                var updates = new FacilityEditDto(facility) { State = newState };

                await repository.UpdateFacilityAsync(facilityId, updates);
            }

            using (var repository = repositoryHelper.GetFacilityRepository())
            {
                var expected = DataHelpers.GetFacilityDetail(facilityId);
                expected.State = newState;

                var updatedFacility = await repository.GetFacilityAsync(expected.Id);
                // TODO: Following line makes test work, but need to decide whether child property should be returned or not
                updatedFacility.File.Facilities = null;

                updatedFacility.Should().BeEquivalentTo(expected);
            }
        }

        [Fact]
        public async Task UpdateNonexistantFacility_ThrowsException()
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
