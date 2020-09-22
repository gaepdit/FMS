using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
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
        public async Task FacilityExists_Exists_ReturnsTrue()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var result = await repository.FacilityExistsAsync(DataHelpers.Facilities[0].Id);
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task FacilityExists_NotExists_ReturnsFalse()
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

            Guid newFacilityId;
            var newFacility = new FacilityCreateDto()
            {
                FacilityNumber = "ABC",
                Name = "New Facility",
                FileLabel = "243-0001",
                EnvironmentalInterestId = new Guid("FC2A0444-6287-432F-9285-6BA0E7AA73C6"), // RCRA
                FacilityTypeId = new Guid("3FE94D7D-563E-4CA1-A094-BB6E217990D2"), // GEN
                OrganizationalUnitId = new Guid("57B8BEB5-368A-4056-872D-0DB0ADE175E3"), // Org Unit
                BudgetCodeId = new Guid("457D191A-D2B1-4C38-8633-9061C4268E37"), // HWRCRA
                ComplianceOfficerId = new Guid("FCE1195E-BF17-4513-B617-029EE8766A6E"), // 01069946 
                FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"), // NON-RCRA
                Location = "Description of Location",
                Address = "123 Fake St.",
                City = "WOODSTOCK",
                State = "Georgia",
                PostalCode = "30188",
                Latitude = 34.114309m,
                Longitude = -84.470057m,
                CountyId = 243, // Cherokee
            };

            using (var repository = repositoryHelper.GetFacilityRepository())
            {
                newFacilityId = await repository.CreateFacilityAsync(newFacility);
            }

            using (var repository = repositoryHelper.GetFacilityRepository())
            {
                var createdFacility = await repository.GetFacilityAsync(newFacilityId);

                createdFacility.Id.Should().Be(newFacilityId);
                createdFacility.FileLabel.Should().Be(newFacility.FileLabel);
                createdFacility.Name.Should().Be(newFacility.Name);
            }
        }

        [Fact]
        public async Task CreateFacility_WithoutFileLabel_SucceedsAndCreatesNewFile()
        {
            var repositoryHelper = new RepositoryHelper();

            Guid newFacilityId;
            var newFacility = new FacilityCreateDto()
            {
                FacilityNumber = "ABC",
                Name = "New Facility",
                FileLabel = null,
                EnvironmentalInterestId = new Guid("FC2A0444-6287-432F-9285-6BA0E7AA73C6"), // RCRA
                FacilityTypeId = new Guid("3FE94D7D-563E-4CA1-A094-BB6E217990D2"), // GEN
                OrganizationalUnitId = new Guid("57B8BEB5-368A-4056-872D-0DB0ADE175E3"), // Org Unit
                BudgetCodeId = new Guid("457D191A-D2B1-4C38-8633-9061C4268E37"), // HWRCRA
                ComplianceOfficerId = new Guid("FCE1195E-BF17-4513-B617-029EE8766A6E"), // 01069946 
                FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"), // NON-RCRA
                Location = "Description of Location",
                Address = "123 Fake St.",
                City = "WOODSTOCK",
                State = "Georgia",
                PostalCode = "30188",
                Latitude = 34.114309m,
                Longitude = -84.470057m,
                CountyId = 243, // Cherokee
            };

            using (var repository = repositoryHelper.GetFacilityRepository())
            {
                newFacilityId = await repository.CreateFacilityAsync(newFacility);
            }

            using (var repository = repositoryHelper.GetFacilityRepository())
            {
                var createdFacility = await repository.GetFacilityAsync(newFacilityId);

                createdFacility.Id.Should().Be(newFacilityId);
                createdFacility.FileLabel.Should().NotBeNullOrEmpty();
                createdFacility.FileLabel.Should().StartWith(newFacility.CountyId.ToString());
                createdFacility.Name.Should().Be(newFacility.Name);
            }
        }

        [Fact]
        public async Task CreateFacility_WithEmptyFacilityNumber_ThrowsException()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
        
            var newFacility = new FacilityCreateDto()
            {
                FacilityNumber = " ",
                Name = "New Facility",
                FileLabel = null,
                EnvironmentalInterestId = new Guid("FC2A0444-6287-432F-9285-6BA0E7AA73C6"), // RCRA
                FacilityTypeId = new Guid("3FE94D7D-563E-4CA1-A094-BB6E217990D2"), // GEN
                OrganizationalUnitId = new Guid("57B8BEB5-368A-4056-872D-0DB0ADE175E3"), // Org Unit
                BudgetCodeId = new Guid("457D191A-D2B1-4C38-8633-9061C4268E37"), // HWRCRA
                ComplianceOfficerId = new Guid("FCE1195E-BF17-4513-B617-029EE8766A6E"), // 01069946 
                FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"), // NON-RCRA
                Location = "Description of Location",
                Address = "123 Fake St.",
                City = "WOODSTOCK",
                State = "Georgia",
                PostalCode = "30188",
                Latitude = 34.114309m,
                Longitude = -84.470057m,
                CountyId = 243, // Cherokee
            };

            Func<Task> action = async () =>
            {
                var result = await repository.CreateFacilityAsync(newFacility);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("Facility Number is required.");
        }

        // TODO #66: When adding a new facility number, make sure the number doesn't already exist before trying to save. 
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


        [Fact]
        public async Task CreateFacility_WithWhitespaceFileLabel_SucceedsAndCreatesNewFile()
        {
            var repositoryHelper = new RepositoryHelper();

            Guid newFacilityId;
            var newFacility = new FacilityCreateDto()
            {
                FacilityNumber = "ABC",
                Name = "New Facility",
                FileLabel = " ",
                EnvironmentalInterestId = new Guid("FC2A0444-6287-432F-9285-6BA0E7AA73C6"), // RCRA
                FacilityTypeId = new Guid("3FE94D7D-563E-4CA1-A094-BB6E217990D2"), // GEN
                OrganizationalUnitId = new Guid("57B8BEB5-368A-4056-872D-0DB0ADE175E3"), // Org Unit
                BudgetCodeId = new Guid("457D191A-D2B1-4C38-8633-9061C4268E37"), // HWRCRA
                ComplianceOfficerId = new Guid("FCE1195E-BF17-4513-B617-029EE8766A6E"), // 01069946 
                FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"), // NON-RCRA
                Location = "Description of Location",
                Address = "123 Fake St.",
                City = "WOODSTOCK",
                State = "Georgia",
                PostalCode = "30188",
                Latitude = 34.114309m,
                Longitude = -84.470057m,
                CountyId = 243, // Cherokee
            };

            using (var repository = repositoryHelper.GetFacilityRepository())
            {
                newFacilityId = await repository.CreateFacilityAsync(newFacility);
            }

            using (var repository = repositoryHelper.GetFacilityRepository())
            {
                var createdFacility = await repository.GetFacilityAsync(newFacilityId);

                createdFacility.Id.Should().Be(newFacilityId);
                createdFacility.FileLabel.Should().NotBeNullOrEmpty();
                createdFacility.FileLabel.Should().StartWith(newFacility.CountyId.ToString());
                createdFacility.Name.Should().Be(newFacility.Name);
            }
        }

        [Fact]
        public async Task CreateFacility_WithNonexistentFileLabel_ThrowsException()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();

            var newFacility = new FacilityCreateDto()
            {
                FacilityNumber = "ABC",
                Name = "New Facility",
                FileLabel = "ABC999",
                EnvironmentalInterestId = new Guid("FC2A0444-6287-432F-9285-6BA0E7AA73C6"), // RCRA
                FacilityTypeId = new Guid("3FE94D7D-563E-4CA1-A094-BB6E217990D2"), // GEN
                OrganizationalUnitId = new Guid("57B8BEB5-368A-4056-872D-0DB0ADE175E3"), // Org Unit
                BudgetCodeId = new Guid("457D191A-D2B1-4C38-8633-9061C4268E37"), // HWRCRA
                ComplianceOfficerId = new Guid("FCE1195E-BF17-4513-B617-029EE8766A6E"), // 01069946 
                FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"), // NON-RCRA
                Location = "Description of Location",
                Address = "123 Fake St.",
                City = "WOODSTOCK",
                State = "Georgia",
                PostalCode = "30188",
                Latitude = 34.114309m,
                Longitude = -84.470057m,
                CountyId = 243, // Cherokee
            };

            Func<Task> action = async () =>
            {
                var result = await repository.CreateFacilityAsync(newFacility);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage($"File Label {newFacility.FileLabel} does not exist.");
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

        // RetentionRecordExistsAsync

        [Fact]
        public async Task RetentionRecord_Exists_ReturnsTrue()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var result = await repository.RetentionRecordExistsAsync(DataHelpers.RetentionRecords[0].Id);
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task RetentionRecord_NotExists_ReturnsFalse()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var result = await repository.RetentionRecordExistsAsync(default);
            result.ShouldBeFalse();
        }

        // GetRetentionRecordAsync

        [Fact]
        public async Task GetRetentionRecord_ReturnsCorrectItem()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var expected = new RetentionRecordDetailDto(DataHelpers.RetentionRecords[0]);

            var result = await repository.GetRetentionRecordAsync(expected.Id);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetNonexistentRetentionRecord_ReturnsNull()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var result = await repository.GetRetentionRecordAsync(default);
            result.ShouldBeNull();
        }

        // CreateRetentionRecordAsync

        [Fact]
        public async Task CreateRecord_Succeeds()
        {
            using var repository = new RepositoryHelper().GetFacilityRepository();
            var record = new RetentionRecord()
            {
                Active = true,
                BoxNumber = "NewBox",
                ConsignmentNumber = "CN",
                EndYear = 2020,
                FacilityId = DataHelpers.Facilities[0].Id,
                RetentionSchedule = "RS",
                ShelfNumber = "SN",
                StartYear = 2000,
            };
            var newRecord = new RetentionRecordCreateDto()
            {
                BoxNumber = record.BoxNumber,
                ConsignmentNumber = record.ConsignmentNumber,
                EndYear = record.EndYear,
                RetentionSchedule = record.RetentionSchedule,
                ShelfNumber = record.ShelfNumber,
                StartYear = record.StartYear

            };

            record.Id = await repository.CreateRetentionRecordAsync(record.FacilityId, newRecord);
            var result = await repository.GetRetentionRecordAsync(record.Id);

            result.Should().BeEquivalentTo(new RetentionRecordDetailDto(record));
        }

        // UpdateRetentionRecordAsync

        // GetFacilityForRetentionRecord

    }
}
