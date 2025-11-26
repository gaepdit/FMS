using System;
using System.Linq;
using System.Threading.Tasks;
using AwesomeAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using TestHelpers;
using NUnit.Framework;
using static TestHelpers.RepositoryHelper;
using static FMS.TestData.SeedData.SeedData;
using FMS.TestData.SeedData;

namespace FMS.Infrastructure.Tests
{
    public class FacilityRepositoryTests
    {
        // FacilityExistsAsync

        [Test]
        public async Task FacilityExists_Exists_ReturnsTrue()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();
            var result = await repository.FacilityExistsAsync(SeedData.GetFacilities()[0].Id);
            result.Should().BeTrue();
        }

        [Test]
        public async Task FacilityExists_NotExists_ReturnsFalse()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();
            var result = await repository.FacilityExistsAsync(Guid.Empty);
            result.Should().BeFalse();
        }

        // GetFacilityAsync

        [Test]
        public async Task GetFacility_ReturnsCorrectFacility()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();
            var facility = SeedData.GetFacilities()[0];

            var result = await repository.GetFacilityAsync(facility.Id);

            result.Id.Should().Be(facility.Id);
            result.Name.Should().Be(facility.Name);
        }

        [Test]
        public async Task GetNonexistentFacility_ReturnsNull()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();
            var result = await repository.GetFacilityAsync(Guid.Empty);
            result.Should().BeNull();
        }

        // CountAsync

        [Test]
        public async Task FacilityCount_DefaultSpec_ReturnsCountOfActiveFacilities()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();
            var spec = new FacilitySpec();

            var result = await repository.CountAsync(spec);
            var expected = SeedData.GetFacilities().Count(e => e.Active);

            result.Should().Be(expected);
        }

        [Test]
        public async Task FacilityCount_WithDeleted_ReturnsCountOfAll()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();
            var spec = new FacilitySpec { ShowDeleted = true };

            var result = await repository.CountAsync(spec);
            var expected = SeedData.GetFacilities().Count;

            result.Should().Be(expected);
        }

        [Test]
        public async Task FacilityCount_ByCounty_ReturnsCorrectCount()
        {
            const int countyId = 131;
            var spec = new FacilitySpec { CountyId = countyId };

            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();
            var result = await repository.CountAsync(spec);
            var expected = SeedData.GetFacilities()
                .Count(e => e.CountyId == countyId && e.Active);

            result.Should().Be(expected);
        }

        [Test]
        public async Task FacilityCount_ByMissingCounty_ReturnsZero()
        {
            const int countyId = 343;
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();
            var spec = new FacilitySpec { CountyId = countyId };

            var result = await repository.CountAsync(spec);

            result.Should().Be(0);
        }

        [TestCase("ABC")]
        [TestCase("GHI")]
        public async Task FacilityCount_ByFacilityNumber_ReturnsCorrectCount(string facilityNumberSpec)
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();
            var spec = new FacilitySpec { FacilityNumber = facilityNumberSpec };

            var result = await repository.CountAsync(spec);
            var expected = SeedData.GetFacilities()
                .Count(e => e.FacilityNumber.ToLower().Contains(facilityNumberSpec.ToLower()) && e.Active);

            result.Should().Be(expected);
        }
            
        [Test]
        public async Task FacilityCount_ByInactiveFacilityNumber_ReturnsZero()
        {
            const string facilityNumber = "DEF";
            var spec = new FacilitySpec { FacilityNumber = facilityNumber };
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();

            var result = await repository.CountAsync(spec);

            result.Should().Be(0);
        }

        [Test]
        public async Task FacilityCount_ByMissingFacilityNumber_ReturnsZero()
        {
            const string facilityNumber = "zzz";
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();
            var spec = new FacilitySpec { FacilityNumber = facilityNumber };

            var result = await repository.CountAsync(spec);

            result.Should().Be(0);
        }

        // GetFacilityPaginatedListAsync

        [Test]
        public async Task FacilitySearch_DefaultSpec_ReturnsActiveFacilities()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();

            var result = await repository.GetFacilityPaginatedListAsync(new FacilitySpec(), 1, 999);
            var expectedCount = SeedData.GetFacilities().Count(e => e.Active);

            result.TotalCount.Should().Be(expectedCount);
            result.Items.Count.Should().Be(expectedCount);
            result.PageNumber.Should().Be(1);
            result.TotalPages.Should().Be(1);
        }

        [Test]
        public async Task FacilitySearch_WithDeleted_ReturnsAllFacilities()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();
            var spec = new FacilitySpec { ShowDeleted = true };

            var result = await repository.GetFacilityPaginatedListAsync(spec, 1, 999);
            var expectedCount = SeedData.GetFacilities().Count;

            result.TotalCount.Should().Be(expectedCount);
            result.Items.Count.Should().Be(expectedCount);
            result.PageNumber.Should().Be(1);
            result.TotalPages.Should().Be(1);
        }

        [Test]
        public async Task FacilitySearch_ByCounty_ReturnsCorrectList()
        {
            const int countySpec = 261;
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();
            var spec = new FacilitySpec { CountyId = countySpec, ShowDeleted = true };

            var result = await repository.GetFacilityPaginatedListAsync(spec, 1, 999);
            var expectedCount = SeedData.GetFacilities().Count(e => e.CountyId == countySpec);

            result.TotalCount.Should().Be(expectedCount);
            result.Items.Count.Should().Be(expectedCount);
            result.PageNumber.Should().Be(1);
            result.TotalPages.Should().Be(1);
        }

        [TestCase("Highway 92 Woodstock")]
        [TestCase("Kroger Plaza, Woodstock")]
        [TestCase("Shopping center parking lot")]
        [TestCase("")]
        public async Task FacilitySearch_ByLocation_ReturnsCorrectList(string locationSpec)
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();
            var spec = new FacilitySpec { Location = locationSpec, ShowDeleted = true };

            var result = await repository.GetFacilityPaginatedListAsync(spec, 1, 999);
            var expectedCount = SeedData.GetFacilities().Count(e => e.Location.Contains(locationSpec));

            result.TotalCount.Should().Be(expectedCount);
            result.Items.Count.Should().Be(expectedCount);
            result.PageNumber.Should().Be(1);
            result.TotalPages.Should().Be(1);
        }

        [Test]
        public async Task FacilitySearch_ByMissingCounty_ReturnsNone()
        {
            const int countySpec = 131;
            var spec = new FacilitySpec { CountyId = countySpec, ShowDeleted = true };
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();

            var result = await repository.GetFacilityPaginatedListAsync(spec, 1, 999);
            var expectedCount = SeedData.GetFacilities().Count(e => e.CountyId == countySpec);

            result.TotalCount.Should().Be(expectedCount);
            result.Items.Count.Should().Be(expectedCount);
            result.PageNumber.Should().Be(1);
            result.TotalPages.Should().Be(0);
        }

        [TestCase("ADD1")]
        [TestCase("UTF8")]
        public async Task FacilitySearch_ByFacilityNumber_ReturnsCorrectList(string facilityNumberSpec)
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();
            var spec = new FacilitySpec { FacilityNumber = facilityNumberSpec, ShowDeleted = true };

            var result = await repository.GetFacilityPaginatedListAsync(spec, 1, 999);
            var expectedCount = SeedData.GetFacilities()
                .Count(e => e.FacilityNumber.ToLower().Contains(facilityNumberSpec.ToLower()) && e.Active);

            result.TotalCount.Should().Be(expectedCount);
            result.Items.Count.Should().Be(expectedCount);
            result.PageNumber.Should().Be(1);
            result.TotalPages.Should().Be(1);
        }

        [Test]
        public async Task FacilitySearch_ByInactiveFacilityNumber_ReturnsEmpty()
        {
            const string facilityNumberSpec = "DEF";
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();
            var spec = new FacilitySpec { FacilityNumber = facilityNumberSpec };

            var result = await repository.GetFacilityPaginatedListAsync(spec, 1, 999);

            result.TotalCount.Should().Be(0);
            result.Items.Count.Should().Be(0);
            result.PageNumber.Should().Be(1);
            result.TotalPages.Should().Be(0);
        }

        [Test]
        public async Task FacilitySearch_ByMissingFacilityNumber_ReturnsEmpty()
        {
            const string facilityNumberSpec = "zzz";
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();
            var spec = new FacilitySpec { FacilityNumber = facilityNumberSpec };

            var result = await repository.GetFacilityPaginatedListAsync(spec, 1, 999);

            result.TotalCount.Should().Be(0);
            result.Items.Count.Should().Be(0);
            result.PageNumber.Should().Be(1);
            result.TotalPages.Should().Be(0);
        }

        // GetFacilityDetailListAsync

        [Test]
        public async Task GetFacilityDetailListAsync_DefaultSpec_ReturnsActiveFacilities()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();

            var result = await repository.GetFacilityDetailListAsync(new FacilitySpec());
            var expectedCount = SeedData.GetFacilities().Count(e => e.Active);

            result.Count.Should().Be(expectedCount);
        }

        // Additional Tests

        [Test]
        public async Task FacilityNumberExists_Unique_ReturnsFalse()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();
            var result = await repository.FacilityNumberExists("Unique");
            result.Should().BeFalse();
        }

        [Test]
        public async Task FacilityNumberExists_Duplicate_ReturnsTrue()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();

            var result = await repository.FacilityNumberExists("UTF8");

            result.Should().BeTrue();
        }

        [Test]
        public async Task FacilityNumberExists_DuplicateIsIgnored_ReturnsFalse()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();
            var ignoreId = SeedData.GetFacilities()[0].Id;
            var facName = SeedData.GetFacilities()[0].Name;
            var result = await repository.FacilityNumberExists(facName, ignoreId);
            result.Should().BeFalse();
        }

        // FileLabelExists

        [Test]
        public async Task FileLabelExists_Unique_ReturnsFalse()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();
            var result = await repository.FileLabelExists("999-9999");
            result.Should().BeFalse();
        }

        [Test]
        public async Task FileLabelExists_Duplicate_ReturnsTrue()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();
            var fileLabel = SeedData.GetFiles()[0].FileLabel;
            var result = await repository.FileLabelExists(fileLabel);
            result.Should().BeTrue();
        }

        // RetentionRecordExistsAsync

        [Test]
        public async Task RetentionRecord_Exists_ReturnsTrue()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();
            var result = await repository.RetentionRecordExistsAsync(SeedData.GetRetentionRecords()[0].Id);
            result.Should().BeTrue();
        }

        [Test]
        public async Task RetentionRecord_NotExists_ReturnsFalse()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();
            var result = await repository.RetentionRecordExistsAsync(Guid.Empty);
            result.Should().BeFalse();
        }

        // GetRetentionRecordAsync

        [Test]
        public async Task GetRetentionRecord_ReturnsCorrectItem()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();
            var expected = new RetentionRecordDetailDto(SeedData.GetRetentionRecords()[0]);

            var result = await repository.GetRetentionRecordAsync(expected.Id);

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task GetRetentionRecord_Nonexistent_ReturnsNull()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();
            var result = await repository.GetRetentionRecordAsync(Guid.Empty);
            result.Should().BeNull();
        }

        // CreateRetentionRecordAsync

        [Test]
        public async Task CreateRecord_Succeeds()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFacilityRepository();
            var record = new RetentionRecord()
            {
                Active = true,
                BoxNumber = "NewBox",
                ConsignmentNumber = "CN",
                EndYear = 2020,
                FacilityId = SeedData.GetFacilities()[0].Id,
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