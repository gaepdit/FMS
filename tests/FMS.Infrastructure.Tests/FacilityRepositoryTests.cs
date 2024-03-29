using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using TestHelpers;
using Xunit;
using Xunit.Extensions.AssertExtensions;
using static TestHelpers.RepositoryHelper;

namespace FMS.Infrastructure.Tests
{
    public class FacilityRepositoryTests
    {
        // FacilityExistsAsync

        [Fact]
        public async Task FacilityExists_Exists_ReturnsTrue()
        {
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var result = await repository.FacilityExistsAsync(RepositoryData.Facilities()[0].Id);
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task FacilityExists_NotExists_ReturnsFalse()
        {
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var result = await repository.FacilityExistsAsync(Guid.Empty);
            result.ShouldBeFalse();
        }

        // GetFacilityAsync

        [Fact]
        public async Task GetFacility_ReturnsCorrectFacility()
        {
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var facility = RepositoryData.Facilities()[0];

            var result = await repository.GetFacilityAsync(facility.Id);

            result.Id.Should().Be(facility.Id);
            result.Name.Should().Be(facility.Name);
        }

        [Fact]
        public async Task GetNonexistentFacility_ReturnsNull()
        {
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var result = await repository.GetFacilityAsync(Guid.Empty);
            result.ShouldBeNull();
        }

        // CountAsync

        [Fact]
        public async Task FacilityCount_DefaultSpec_ReturnsCountOfActiveFacilities()
        {
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec();

            var result = await repository.CountAsync(spec);
            var expected = RepositoryData.Facilities().Count(e => e.Active);

            result.Should().Be(expected);
        }

        [Fact]
        public async Task FacilityCount_WithDeleted_ReturnsCountOfAll()
        {
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() {ShowDeleted = true};

            var result = await repository.CountAsync(spec);
            var expected = RepositoryData.Facilities().Count;

            result.Should().Be(expected);
        }

        [Fact]
        public async Task FacilityCount_ByCounty_ReturnsCorrectCount()
        {
            const int countyId = 131;
            var spec = new FacilitySpec() {CountyId = countyId};

            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var result = await repository.CountAsync(spec);
            var expected = RepositoryData.Facilities()
                .Count(e => e.CountyId == countyId && e.Active);

            result.Should().Be(expected);
        }

        [Fact]
        public async Task FacilityCount_ByMissingCounty_ReturnsZero()
        {
            const int countyId = 243;
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() {CountyId = countyId};

            var result = await repository.CountAsync(spec);

            result.Should().Be(0);
        }

        [Theory]
        [InlineData("ABC")]
        [InlineData("GHI")]
        // [InlineData("ghi")] Sqlite is case-sensitive by default
        public async Task FacilityCount_ByFacilityNumber_ReturnsCorrectCount(string facilityNumberSpec)
        {
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() {FacilityNumber = facilityNumberSpec};

            var result = await repository.CountAsync(spec);
            var expected = RepositoryData.Facilities()
                .Count(e => e.FacilityNumber.ToLower().Contains(facilityNumberSpec.ToLower()) && e.Active);

            result.Should().Be(expected);
        }

        [Fact]
        public async Task FacilityCount_ByInactiveFacilityNumber_ReturnsZero()
        {
            const string facilityNumber = "DEF";
            var spec = new FacilitySpec() {FacilityNumber = facilityNumber};
            using var repository = CreateRepositoryHelper().GetFacilityRepository();

            var result = await repository.CountAsync(spec);

            result.Should().Be(0);
        }

        [Fact]
        public async Task FacilityCount_ByMissingFacilityNumber_ReturnsZero()
        {
            const string facilityNumber = "zzz";
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() {FacilityNumber = facilityNumber};

            var result = await repository.CountAsync(spec);

            result.Should().Be(0);
        }

        // GetFacilityPaginatedListAsync

        [Fact]
        public async Task FacilitySearch_DefaultSpec_ReturnsActiveFacilities()
        {
            using var repository = CreateRepositoryHelper().GetFacilityRepository();

            var result = await repository.GetFacilityPaginatedListAsync(new FacilitySpec(), 1, 999);
            var expectedCount = RepositoryData.Facilities().Count(e => e.Active);

            result.TotalCount.ShouldEqual(expectedCount);
            result.Items.Count.ShouldEqual(expectedCount);
            result.PageNumber.ShouldEqual(1);
            result.TotalPages.ShouldEqual(1);
        }

        [Fact]
        public async Task FacilitySearch_WithDeleted_ReturnsAllFacilities()
        {
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() {ShowDeleted = true};

            var result = await repository.GetFacilityPaginatedListAsync(spec, 1, 999);
            var expectedCount = RepositoryData.Facilities().Count;

            result.TotalCount.ShouldEqual(expectedCount);
            result.Items.Count.ShouldEqual(expectedCount);
            result.PageNumber.ShouldEqual(1);
            result.TotalPages.ShouldEqual(1);
        }

        [Fact]
        public async Task FacilitySearch_ByCounty_ReturnsCorrectList()
        {
            const int countySpec = 131;
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() {CountyId = countySpec, ShowDeleted = true};

            var result = await repository.GetFacilityPaginatedListAsync(spec, 1, 999);
            var expectedCount = RepositoryData.Facilities().Count(e => e.CountyId == countySpec);

            result.TotalCount.ShouldEqual(expectedCount);
            result.Items.Count.ShouldEqual(expectedCount);
            result.PageNumber.ShouldEqual(1);
            result.TotalPages.ShouldEqual(1);
        }

        [Theory]
        [InlineData("where")]
        [InlineData("somewhere")]
        [InlineData("here")]
        [InlineData("")]
        public async Task FacilitySearch_ByLocation_ReturnsCorrectList(string locationSpec)
        {
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() {Location = locationSpec, ShowDeleted = true};

            var result = await repository.GetFacilityPaginatedListAsync(spec, 1, 999);
            var expectedCount = RepositoryData.Facilities().Count(e => e.Location.Contains(locationSpec));

            result.TotalCount.ShouldEqual(expectedCount);
            result.Items.Count.ShouldEqual(expectedCount);
            result.PageNumber.ShouldEqual(1);
            result.TotalPages.ShouldEqual(1);
        }

        [Fact]
        public async Task FacilitySearch_ByMissingCounty_ReturnsNone()
        {
            const int countySpec = 243;
            var spec = new FacilitySpec() {CountyId = countySpec, ShowDeleted = true};
            using var repository = CreateRepositoryHelper().GetFacilityRepository();

            var result = await repository.GetFacilityPaginatedListAsync(spec, 1, 999);
            var expectedCount = RepositoryData.Facilities().Count(e => e.CountyId == countySpec);

            result.TotalCount.ShouldEqual(expectedCount);
            result.Items.Count.ShouldEqual(expectedCount);
            result.PageNumber.ShouldEqual(1);
            result.TotalPages.ShouldEqual(0);
        }

        [Theory]
        [InlineData("ABC")]
        [InlineData("GHI")]
        // [InlineData("ghi")] Sqlite is case-sensitive by default
        public async Task FacilitySearch_ByFacilityNumber_ReturnsCorrectList(string facilityNumberSpec)
        {
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() {FacilityNumber = facilityNumberSpec, ShowDeleted = true};

            var result = await repository.GetFacilityPaginatedListAsync(spec, 1, 999);
            var expectedCount = RepositoryData.Facilities()
                .Count(e => e.FacilityNumber.ToLower().Contains(facilityNumberSpec.ToLower()) && e.Active);

            result.TotalCount.ShouldEqual(expectedCount);
            result.Items.Count.ShouldEqual(expectedCount);
            result.PageNumber.ShouldEqual(1);
            result.TotalPages.ShouldEqual(1);
        }

        [Fact]
        public async Task FacilitySearch_ByInactiveFacilityNumber_ReturnsEmpty()
        {
            const string facilityNumberSpec = "DEF";
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() {FacilityNumber = facilityNumberSpec};

            var result = await repository.GetFacilityPaginatedListAsync(spec, 1, 999);

            result.TotalCount.ShouldEqual(0);
            result.Items.Count.ShouldEqual(0);
            result.PageNumber.ShouldEqual(1);
            result.TotalPages.ShouldEqual(0);
        }

        [Fact]
        public async Task FacilitySearch_ByMissingFacilityNumber_ReturnsEmpty()
        {
            const string facilityNumberSpec = "zzz";
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() {FacilityNumber = facilityNumberSpec};

            var result = await repository.GetFacilityPaginatedListAsync(spec, 1, 999);

            result.TotalCount.ShouldEqual(0);
            result.Items.Count.ShouldEqual(0);
            result.PageNumber.ShouldEqual(1);
            result.TotalPages.ShouldEqual(0);
        }

        [Fact]
        public async Task FacilitySearch_Page2_ReturnsSecondPage()
        {
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() {ShowDeleted = true};

            var result = await repository.GetFacilityPaginatedListAsync(spec, 2, 2);
            var expectedPages = (int) Math.Ceiling(RepositoryData.Facilities().Count / 2m);

            result.TotalCount.ShouldEqual(RepositoryData.Facilities().Count);
            result.Items.Count.ShouldEqual(2);
            result.PageNumber.ShouldEqual(2);
            result.TotalPages.ShouldEqual(expectedPages);
        }

        [Fact]
        public async Task FacilitySearch_BeyondLastPage_ReturnsEmptyList()
        {
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() {ShowDeleted = true};

            var result = await repository.GetFacilityPaginatedListAsync(spec, 999, 999);

            result.TotalCount.ShouldEqual(RepositoryData.Facilities().Count);
            result.Items.Count.ShouldEqual(0);
            result.PageNumber.ShouldEqual(999);
            result.TotalPages.ShouldEqual(1);
        }

        // GetFacilityDetailListAsync

        [Fact]
        public async Task GetFacilityDetailListAsync_DefaultSpec_ReturnsActiveFacilities()
        {
            using var repository = CreateRepositoryHelper().GetFacilityRepository();

            var result = await repository.GetFacilityDetailListAsync(new FacilitySpec());
            var expectedCount = RepositoryData.Facilities().Count(e => e.Active);

            result.Count.ShouldEqual(expectedCount);
        }

        // CreateFacilityAsync

        [Fact]
        public async Task CreateFacility_Succeeds()
        {
            var newFacility = new FacilityCreateDto()
            {
                FacilityNumber = "QRS1",
                Name = "New Facility",
                FileLabel = RepositoryData.Files[0].Name,
                FacilityTypeId = RepositoryData.FacilityTypes[0].Id,
                OrganizationalUnitId = RepositoryData.OrganizationalUnits[0].Id,
                BudgetCodeId = RepositoryData.BudgetCodes[0].Id,
                FacilityStatusId = RepositoryData.FacilityStatuses[0].Id,
                Location = "Description of Location",
                Address = "123 Fake St.",
                City = "WOODSTOCK",
                State = "Georgia",
                PostalCode = "30188",
                Latitude = 34.114309m,
                Longitude = -84.470057m,
                CountyId = 243, // Cherokee
            };

            using var repositoryHelper = CreateRepositoryHelper();
            using var repository = repositoryHelper.GetFacilityRepository();
            var newFacilityId = await repository.CreateFacilityAsync(newFacility);
            repositoryHelper.ClearChangeTracker();


            var createdFacility = await repository.GetFacilityAsync(newFacilityId);

            createdFacility.Id.Should().Be(newFacilityId);
            createdFacility.FileLabel.Should().Be(newFacility.FileLabel);
            createdFacility.Name.Should().Be(newFacility.Name);
        }

        [Fact]
        public async Task CreateFacility_WithoutFileLabel_SucceedsAndCreatesNewFile()
        {
            Guid newFacilityId;
            var newFacility = new FacilityCreateDto()
            {
                FacilityNumber = "QRS1",
                Name = "New Facility",
                FileLabel = null,
                FacilityTypeId = RepositoryData.FacilityTypes[0].Id,
                OrganizationalUnitId = RepositoryData.OrganizationalUnits[0].Id,
                BudgetCodeId = RepositoryData.BudgetCodes[0].Id,
                FacilityStatusId = RepositoryData.FacilityStatuses[0].Id,
                Location = "Description of Location",
                Address = "123 Fake St.",
                City = "WOODSTOCK",
                State = "Georgia",
                PostalCode = "30188",
                Latitude = 34.114309m,
                Longitude = -84.470057m,
                CountyId = 243, // Cherokee
            };

            var repositoryHelper = CreateRepositoryHelper();
            using var repository = repositoryHelper.GetFacilityRepository();
            newFacilityId = await repository.CreateFacilityAsync(newFacility);
            repositoryHelper.ClearChangeTracker();


            var createdFacility = await repository.GetFacilityAsync(newFacilityId);

            createdFacility.Id.Should().Be(newFacilityId);
            createdFacility.FileLabel.Should().NotBeNullOrEmpty();
            createdFacility.FileLabel.Should().StartWith(newFacility.CountyId.ToString());
            createdFacility.Name.Should().Be(newFacility.Name);
        }

        [Fact]
        public async Task CreateFacility_WithWhitespaceFacilityNumber_ThrowsException()
        {
            var newFacility = new FacilityCreateDto()
            {
                FacilityNumber = " ",
                Name = "New Facility",
                FileLabel = null,
                FacilityTypeId = RepositoryData.FacilityTypes[0].Id,
                OrganizationalUnitId = RepositoryData.OrganizationalUnits[0].Id,
                BudgetCodeId = RepositoryData.BudgetCodes[0].Id,
                FacilityStatusId = RepositoryData.FacilityStatuses[0].Id,
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
                using var repository = CreateRepositoryHelper().GetFacilityRepository();
                await repository.CreateFacilityAsync(newFacility);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("Facility Number is required.");
        }

        [Fact]
        public async Task CreateFacility_WithExistingNumber_ThrowsException()
        {
            var existingNumber = RepositoryData.Facilities()[0].FacilityNumber;
            var facilityCreate = new FacilityCreateDto() {FacilityNumber = existingNumber, CountyId = 123};

            Func<Task> action = async () =>
            {
                using var repository = CreateRepositoryHelper().GetFacilityRepository();
                await repository.CreateFacilityAsync(facilityCreate);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage($"Facility Number '{facilityCreate.FacilityNumber}' already exists.");
        }

        [Fact]
        public async Task CreateFacility_WithNonexistentCounty_ThrowsException()
        {
            var facilityCreate = new FacilityCreateDto() {CountyId = 999, FacilityNumber = "zzz"};

            Func<Task> action = async () =>
            {
                using var repository = CreateRepositoryHelper().GetFacilityRepository();
                await repository.CreateFacilityAsync(facilityCreate);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("County ID 999 does not exist.");
        }

        [Fact]
        public async Task CreateFacility_WithWhitespaceFileLabel_SucceedsAndCreatesNewFile()
        {
            Guid newFacilityId;
            var newFacility = new FacilityCreateDto()
            {
                FacilityNumber = "QRS1",
                Name = "New Facility",
                FileLabel = " ",
                FacilityTypeId = RepositoryData.FacilityTypes[0].Id,
                OrganizationalUnitId = RepositoryData.OrganizationalUnits[0].Id,
                BudgetCodeId = RepositoryData.BudgetCodes[0].Id,
                FacilityStatusId = RepositoryData.FacilityStatuses[0].Id,
                Location = "Description of Location",
                Address = "123 Fake St.",
                City = "WOODSTOCK",
                State = "Georgia",
                PostalCode = "30188",
                Latitude = 34.114309m,
                Longitude = -84.470057m,
                CountyId = 243, // Cherokee
            };

            using var repositoryHelper = CreateRepositoryHelper();
            using var repository = repositoryHelper.GetFacilityRepository();
            newFacilityId = await repository.CreateFacilityAsync(newFacility);
            repositoryHelper.ClearChangeTracker();

            var createdFacility = await repository.GetFacilityAsync(newFacilityId);

            createdFacility.Id.Should().Be(newFacilityId);
            createdFacility.FileLabel.Should().NotBeNullOrEmpty();
            createdFacility.FileLabel.Should().StartWith(newFacility.CountyId.ToString());
            createdFacility.Name.Should().Be(newFacility.Name);
        }

        [Fact]
        public async Task CreateFacility_WithNonexistentFileLabel_ThrowsException()
        {
            var newFacility = new FacilityCreateDto()
            {
                FacilityNumber = "QRS1",
                Name = "New Facility",
                FileLabel = "ABC999",
                FacilityTypeId = RepositoryData.FacilityTypes[0].Id,
                OrganizationalUnitId = RepositoryData.OrganizationalUnits[0].Id,
                BudgetCodeId = RepositoryData.BudgetCodes[0].Id,
                FacilityStatusId = RepositoryData.FacilityStatuses[0].Id,
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
                using var repository = CreateRepositoryHelper().GetFacilityRepository();
                await repository.CreateFacilityAsync(newFacility);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage($"File Label {newFacility.FileLabel} does not exist.");
        }

        // UpdateFacilityAsync

        [Fact]
        public async Task UpdateFacility_County_Succeeds()
        {
            const int newCountyId = 99;
            var facilityId = RepositoryData.Facilities()[0].Id;

            using var repositoryHelper = CreateRepositoryHelper();
            using var repository = repositoryHelper.GetFacilityRepository();
            var facility = ResourceHelper.GetFacilityDetail(facilityId);
            var updates = new FacilityEditDto(facility) {CountyId = newCountyId};
            await repository.UpdateFacilityAsync(facilityId, updates);
            repositoryHelper.ClearChangeTracker();

            var expected = ResourceHelper.GetFacilityDetail(facilityId);
            expected.County = ResourceHelper.GetCounty(newCountyId);

            var updatedFacility = await repository.GetFacilityAsync(facilityId);

            updatedFacility.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdateFacility_State_Succeeds()
        {
            const string newState = "Alabama";
            var facilityId = RepositoryData.Facilities()[0].Id;

            using var repositoryHelper = CreateRepositoryHelper();
            using var repository = repositoryHelper.GetFacilityRepository();
            var facility = ResourceHelper.GetFacilityDetail(facilityId);
            var updates = new FacilityEditDto(facility) {State = newState};

            await repository.UpdateFacilityAsync(facilityId, updates);
            repositoryHelper.ClearChangeTracker();

            var expected = ResourceHelper.GetFacilityDetail(facilityId);
            expected.State = newState;

            var updatedFacility = await repository.GetFacilityAsync(facilityId);

            updatedFacility.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdateFacility_Longitude_Succeeds()
        {
            const decimal newLongitude = 1.0m;
            var facilityId = RepositoryData.Facilities()[0].Id;

            using var repositoryHelper = CreateRepositoryHelper();
            using var repository = repositoryHelper.GetFacilityRepository();
            var facility = ResourceHelper.GetFacilityDetail(facilityId);
            var updates = new FacilityEditDto(facility) {Longitude = newLongitude};

            await repository.UpdateFacilityAsync(facilityId, updates);
            repositoryHelper.ClearChangeTracker();

            var updatedFacility = await repository.GetFacilityAsync(facilityId);
            updatedFacility.Longitude.Should().Be(newLongitude);
        }

        [Fact]
        public async Task UpdateFacility_ChangeFile_Succeeds()
        {
            var facilityId = RepositoryData.Facilities()[0].Id;
            var newFile = RepositoryData.Files[1];

            using var repositoryHelper = CreateRepositoryHelper();
            using var repository = repositoryHelper.GetFacilityRepository();
            var facility = ResourceHelper.GetFacilityDetail(facilityId);
            var updates = new FacilityEditDto(facility) {FileLabel = newFile.FileLabel};

            await repository.UpdateFacilityAsync(facilityId, updates);
            repositoryHelper.ClearChangeTracker();

            var updatedFacility = await repository.GetFacilityAsync(facilityId);

            updatedFacility.FileLabel.Should().Be(newFile.FileLabel);
            updatedFacility.FileId.Should().Be(newFile.Id);
        }

        [Fact]
        public async Task UpdateFacility_WithBlankFile_SucceedsAndCreatesNewFile()
        {
            var facilityId = RepositoryData.Facilities()[0].Id;

            using var repositoryHelper = CreateRepositoryHelper();
            using var repository = repositoryHelper.GetFacilityRepository();
            var facility = ResourceHelper.GetFacilityDetail(facilityId);
            var updates = new FacilityEditDto(facility) {FileLabel = ""};

            await repository.UpdateFacilityAsync(facilityId, updates);
            repositoryHelper.ClearChangeTracker();

            var updatedFacility = await repository.GetFacilityAsync(facilityId);

            updatedFacility.FileLabel.ShouldNotBeNull();
            updatedFacility.FileLabel.Should().StartWith(RepositoryData.Facilities()[0].CountyId.ToString());
        }

        [Fact]
        public async Task UpdateFacility_MissingFacilityNumber_ThrowsException()
        {
            var updates = new FacilityEditDto() {CountyId = 99};

            Func<Task> action = async () =>
            {
                using var repository = CreateRepositoryHelper().GetFacilityRepository();
                await repository.UpdateFacilityAsync(Guid.Empty, updates);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("Facility Number is required.");
        }

        [Fact]
        public async Task UpdateFacility_NonexistentId_ThrowsException()
        {
            var updates = new FacilityEditDto() {CountyId = 99, FacilityNumber = "zzz"};

            Func<Task> action = async () =>
            {
                using var repository = CreateRepositoryHelper().GetFacilityRepository();
                await repository.UpdateFacilityAsync(Guid.Empty, updates);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("Facility ID not found. (Parameter 'id')");
        }

        [Fact]
        public async Task UpdateFacility_InvalidCounty_ThrowsException()
        {
            var updates = new FacilityEditDto() {CountyId = 999, FacilityNumber = "zzz"};

            Func<Task> action = async () =>
            {
                using var repository = CreateRepositoryHelper().GetFacilityRepository();
                await repository.UpdateFacilityAsync(Guid.Empty, updates);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage($"County ID {updates.CountyId} does not exist.");
        }

        [Fact]
        public async Task UpdateFacility_WithNonexistentFile_ThrowsException()
        {
            const string newFileLabel = "999-9999";

            Func<Task> action = async () =>
            {
                using var repository = CreateRepositoryHelper().GetFacilityRepository();

                var facilityId = RepositoryData.Facilities()[0].Id;
                var facility = ResourceHelper.GetFacilityDetail(facilityId);
                var updates = new FacilityEditDto(facility) {FileLabel = newFileLabel};

                await repository.UpdateFacilityAsync(facilityId, updates);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage($"File Label {newFileLabel} does not exist.");
        }

        // GetNextSequenceForCounty

        [Fact]
        public async Task GetNextSequenceForCounty_Succeeds()
        {
            const int countyNum = 111;
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var result = await repository.GetNextSequenceForCountyAsync(countyNum);
            result.Should().Be(2);
        }


        [Fact]
        public async Task GetNextSequenceForCounty_TwoDigit_Succeeds()
        {
            const int countyNum = 99;
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var result = await repository.GetNextSequenceForCountyAsync(countyNum);
            result.Should().Be(2);
        }

        [Fact]
        public async Task GetNextSequenceForCounty_NoCurrentLabel_ReturnsOne()
        {
            const int countyNum = 101;
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var result = await repository.GetNextSequenceForCountyAsync(countyNum);
            result.Should().Be(1);
        }

        [Fact]
        public async Task GetNextSequenceForCounty_CurrentLabelSkipsNumber_Succeeds()
        {
            const int countyNum = 102;
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var result = await repository.GetNextSequenceForCountyAsync(countyNum);
            result.Should().Be(4);
        }

        [Fact]
        public async Task GetNextSequenceForCounty_LatestLabelInactive_Succeeds()
        {
            const int countyNum = 103;
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var result = await repository.GetNextSequenceForCountyAsync(countyNum);
            result.Should().Be(3);
        }

        [Fact]
        public async Task GetNextSequenceForCounty_NoSuchCounty_ThrowsException()
        {
            const int countyNum = 999;

            Func<Task> action = async () =>
            {
                using var repository = CreateRepositoryHelper().GetFacilityRepository();
                await repository.GetNextSequenceForCountyAsync(countyNum);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage($"County ID {countyNum} does not exist. (Parameter 'countyNum')");
        }

        // Delete/Undelete

        [Fact]
        public async Task DeleteFacility_Succeeds()
        {
            var facility = RepositoryData.Facilities().First(e => !e.Active);

            using var repositoryHelper = CreateRepositoryHelper();
            using var repository = repositoryHelper.GetFacilityRepository();
            await repository.DeleteFacilityAsync(facility.Id);
            repositoryHelper.ClearChangeTracker();

            var updatedFacility = await repository.GetFacilityAsync(facility.Id);

            updatedFacility.Active.ShouldBeFalse();
        }

        [Fact]
        public async Task DeleteFacility_Nonexistent_ThrowsException()
        {
            Func<Task> action = async () =>
            {
                using var repository = CreateRepositoryHelper().GetFacilityRepository();
                await repository.DeleteFacilityAsync(Guid.Empty);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("Facility ID not found. (Parameter 'id')");
        }

        [Fact]
        public async Task UndeleteFacility_Succeeds()
        {
            var facility = RepositoryData.Facilities().First(e => e.Active);

            using var repositoryHelper = CreateRepositoryHelper();
            using var repository = repositoryHelper.GetFacilityRepository();
            await repository.UndeleteFacilityAsync(facility.Id);
            repositoryHelper.ClearChangeTracker();

            var updatedFacility = await repository.GetFacilityAsync(facility.Id);

            updatedFacility.Active.ShouldBeTrue();
        }

        [Fact]
        public async Task UndeleteFacility_Nonexistent_ThrowsException()
        {
            Func<Task> action = async () =>
            {
                using var repository = CreateRepositoryHelper().GetFacilityRepository();
                await repository.UndeleteFacilityAsync(Guid.Empty);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("Facility ID not found. (Parameter 'id')");
        }

        // FacilityNumberExists

        [Fact]
        public async Task FacilityNumberExists_Unique_ReturnsFalse()
        {
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var result = await repository.FacilityNumberExists("Unique");
            result.ShouldBeFalse();
        }

        [Fact]
        public async Task FacilityNumberExists_Duplicate_ReturnsTrue()
        {
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var result = await repository.FacilityNumberExists("ABC");
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task FacilityNumberExists_DuplicateIsIgnored_ReturnsFalse()
        {
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var ignoreId = RepositoryData.Facilities()[0].Id;
            var facName = RepositoryData.Facilities()[0].Name;
            var result = await repository.FacilityNumberExists(facName, ignoreId);
            result.ShouldBeFalse();
        }

        // FileLabelExists

        [Fact]
        public async Task FileLabelExists_Unique_ReturnsFalse()
        {
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var result = await repository.FileLabelExists("999-9999");
            result.ShouldBeFalse();
        }

        [Fact]
        public async Task FileLabelExists_Duplicate_ReturnsTrue()
        {
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var fileLabel = RepositoryData.Files[0].FileLabel;
            var result = await repository.FileLabelExists(fileLabel);
            result.ShouldBeTrue();
        }

        // RetentionRecordExistsAsync

        [Fact]
        public async Task RetentionRecord_Exists_ReturnsTrue()
        {
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var result = await repository.RetentionRecordExistsAsync(RepositoryData.RetentionRecords[0].Id);
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task RetentionRecord_NotExists_ReturnsFalse()
        {
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var result = await repository.RetentionRecordExistsAsync(Guid.Empty);
            result.ShouldBeFalse();
        }

        // GetRetentionRecordAsync

        [Fact]
        public async Task GetRetentionRecord_ReturnsCorrectItem()
        {
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var expected = new RetentionRecordDetailDto(RepositoryData.RetentionRecords[0]);

            var result = await repository.GetRetentionRecordAsync(expected.Id);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetRetentionRecord_Nonexistent_ReturnsNull()
        {
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var result = await repository.GetRetentionRecordAsync(Guid.Empty);
            result.ShouldBeNull();
        }

        // CreateRetentionRecordAsync

        [Fact]
        public async Task CreateRecord_Succeeds()
        {
            using var repository = CreateRepositoryHelper().GetFacilityRepository();
            var record = new RetentionRecord()
            {
                Active = true,
                BoxNumber = "NewBox",
                ConsignmentNumber = "CN",
                EndYear = 2020,
                FacilityId = RepositoryData.Facilities()[0].Id,
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