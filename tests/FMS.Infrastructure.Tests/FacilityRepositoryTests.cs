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
        public async Task NonexistentFacility_Exists_ReturnsFalse()
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

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetNonexistentFacility_ReturnsNull()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var result = await repository.GetFacilityAsync(default);
            result.ShouldBeNull();
        }

        // CountAsync

        [Fact]
        public async Task FacilityCount_DefaultSpec_ReturnsCountOfActiveFacilities()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec();

            var result = await repository.CountAsync(spec);
            var expected = DataHelpers.Facilities.Count(e => e.Active);

            result.Should().Be(expected);
        }

        [Fact]
        public async Task FacilityCount_WithInactive_ReturnsCountOfAll()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() { ActiveOnly = false };

            var result = await repository.CountAsync(spec);
            var expected = DataHelpers.Facilities.Count;

            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(243)]
        [InlineData(131)]
        public async Task FacilityCount_ByCounty_ReturnsCorrectCount(int countyId)
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() { CountyId = countyId };

            var result = await repository.CountAsync(spec);
            var expected = DataHelpers.Facilities.Count(e => e.CountyId == countyId && e.Active);

            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("00")]
        [InlineData("BRF")]
        public async Task FacilityCount_ByFacilityNumber_ReturnsCorrectCount(string facilityNumber)
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() { FacilityNumber = facilityNumber };

            var result = await repository.CountAsync(spec);
            var expected = DataHelpers.Facilities.Count(e => e.FacilityNumber.Contains(facilityNumber) && e.Active);

            result.Should().Be(expected);
        }

        // GetFacilityListAsync

        [Fact]
        public async Task FacilitySearch_Default_ReturnsActiveFacilities()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();

            var result = await repository.GetFacilityListAsync(new FacilitySpec());
            var expected = DataHelpers.Facilities.Where(e => e.Active)
                .Select(e => DataHelpers.GetFacilitySummary(e.Id));

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task FacilitySearch_WithInactive_ReturnsAllFacilities()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() { ActiveOnly = false };

            var result = await repository.GetFacilityListAsync(spec);
            var expected = DataHelpers.Facilities
                .Select(e => DataHelpers.GetFacilitySummary(e.Id));

            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(243)]
        [InlineData(131)]
        public async Task FacilitySearch_ByCounty_ReturnsCorrectList(int countyId)
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() { CountyId = countyId };

            var result = await repository.GetFacilityListAsync(spec);
            var expected = DataHelpers.Facilities
                .Where(e => e.CountyId == countyId && e.Active)
                .Select(e => DataHelpers.GetFacilitySummary(e.Id));

            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("00")]
        [InlineData("BRF")]
        public async Task FacilitySearch_ByFacilityNumber_ReturnsCorrectList(string facilityNumber)
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() { FacilityNumber = facilityNumber };

            var result = await repository.GetFacilityListAsync(spec);
            var expected = DataHelpers.Facilities
                .Where(e => e.FacilityNumber.Contains(facilityNumber) && e.Active)
                .Select(e => DataHelpers.GetFacilitySummary(e.Id));

            result.Should().BeEquivalentTo(expected);
        }

        // CreateFacilityAsync

        [Fact]
        public async Task CreateFacility_Succeeds()
        {
            var repositoryHelper = new RepositoryHelper();
            Guid facilityId = DataHelpers.Facilities[0].Id;
            FacilityDetailDto sampleFacility;
            Guid newFacilityId;

            using (var repository = repositoryHelper.GetFacilityRepository())
            {
                sampleFacility = await repository.GetFacilityAsync(facilityId);
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
                    FileId = sampleFacility.FileId,
                    Latitude = sampleFacility.Latitude,
                    Location = sampleFacility.Location,
                    Longitude = sampleFacility.Longitude,
                    Name = sampleFacility.Name,
                    OrganizationalUnitId = sampleFacility.OrganizationalUnit.Id,
                    PostalCode = sampleFacility.PostalCode,
                    State = sampleFacility.State
                };

                newFacilityId = await repository.CreateFacilityAsync(newFacility);
            }

            using (var repository = repositoryHelper.GetFacilityRepository())
            {
                var createdFacility = await repository.GetFacilityAsync(newFacilityId);

                // Set sample facility properties to match for comparison
                sampleFacility.Id = newFacilityId;
                createdFacility.Should().BeEquivalentTo(sampleFacility);
            }
        }

        // TODO #19: Generate new File ID if newFacility.FileId is null
        [Fact(Skip ="Not implemented yet")]
        public async Task CreateFacility_EmptyFileID_SucceedsAndCreatesNewFile()
        {
            var repositoryHelper = new RepositoryHelper();
            Guid facilityId = DataHelpers.Facilities[0].Id;
            FacilityDetailDto sampleFacility;
            Guid newFacilityId;

            using (var repository = repositoryHelper.GetFacilityRepository())
            {
                sampleFacility = await repository.GetFacilityAsync(facilityId);
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
                    FileId = null,
                    Latitude = sampleFacility.Latitude,
                    Location = sampleFacility.Location,
                    Longitude = sampleFacility.Longitude,
                    Name = sampleFacility.Name,
                    OrganizationalUnitId = sampleFacility.OrganizationalUnit.Id,
                    PostalCode = sampleFacility.PostalCode,
                    State = sampleFacility.State
                };

                newFacilityId = await repository.CreateFacilityAsync(newFacility);
            }

            using (var repository = repositoryHelper.GetFacilityRepository())
            {
                var createdFacility = await repository.GetFacilityAsync(newFacilityId);

                // Set sample facility properties to match for comparison
                sampleFacility.Id = newFacilityId;
                createdFacility.Should().BeEquivalentTo(sampleFacility);
            }
        }

        // TODO #56: Implement required fields
        [Fact(Skip ="Not implemented yet")]
        public async Task CreateFacility_WithEmptyNumber_ThrowsException()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var FacilityCreate = new FacilityCreateDto();

            Func<Task> action = async () =>
            {
                var result = await repository.CreateFacilityAsync(FacilityCreate);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("Facility Number can not be null or empty.");
        }

        // TODO #19: When adding a new facility number, make sure the number doesn't already exist before trying to save. 
        [Fact(Skip = "Not implemented yet")]
        public async Task CreateFacility_WithExistingNumber_ThrowsException()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var existingNumber = DataHelpers.Facilities[0].FacilityNumber;
            var FacilityCreate = new FacilityCreateDto() { FacilityNumber = existingNumber };

            Func<Task> action = async () =>
            {
                var result = await repository.CreateFacilityAsync(FacilityCreate);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage($"Facility Number '{FacilityCreate.FacilityNumber}' already exists.");
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

                updatedFacility.Should().BeEquivalentTo(expected);
            }
        }

        [Fact]
        public async Task UpdateNonexistentFacility_ThrowsException()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var updates = new FacilityEditDto() { CountyId = 99 };

            Func<Task> action = async () =>
            {
                await repository.UpdateFacilityAsync(default, updates);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("Facility ID not found. (Parameter 'id')");
        }
    }
}
