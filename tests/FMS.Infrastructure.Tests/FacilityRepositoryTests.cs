using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using TestHelpers;
using TestHelpers.SimpleRepository;
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
            using var repository = new SimpleRepositoryHelper().GetFacilityRepository();
            var result = await repository.FacilityExistsAsync(SimpleRepositoryData.Facilities[0].Id);
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task FacilityExists_NotExists_ReturnsFalse()
        {
            using var repository = new SimpleRepositoryHelper().GetFacilityRepository();
            var result = await repository.FacilityExistsAsync(default);
            result.ShouldBeFalse();
        }

        // GetFacilityAsync

        [Fact]
        public async Task GetFacility_ReturnsCorrectFacility()
        {
            using var repository = new SimpleRepositoryHelper().GetFacilityRepository();
            var facility = SimpleRepositoryData.Facilities[0];

            var result = await repository.GetFacilityAsync(facility.Id);

            result.Id.Should().Be(facility.Id);
            result.Name.Should().Be(facility.Name);
        }

        [Fact]
        public async Task GetNonexistentFacility_ReturnsNull()
        {
            using var repository = new SimpleRepositoryHelper().GetFacilityRepository();
            var result = await repository.GetFacilityAsync(default);
            result.ShouldBeNull();
        }

        // CountAsync

        [Fact]
        public async Task FacilityCount_DefaultSpec_ReturnsCountOfActiveFacilities()
        {
            using var repository = new SimpleRepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec();

            var result = await repository.CountAsync(spec);
            var expected = SimpleRepositoryData.Facilities.Count(e => e.Active);

            result.Should().Be(expected);
        }

        [Fact]
        public async Task FacilityCount_WithInactive_ReturnsCountOfAll()
        {
            using var repository = new SimpleRepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() {ActiveOnly = false};

            var result = await repository.CountAsync(spec);
            var expected = SimpleRepositoryData.Facilities.Count;

            result.Should().Be(expected);
        }

        [Fact]
        public async Task FacilityCount_ByCounty_ReturnsCorrectCount()
        {
            const int countyId = 131;
            using var repository = new SimpleRepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() {CountyId = countyId};

            var result = await repository.CountAsync(spec);
            var expected = SimpleRepositoryData.Facilities
                .Count(e => e.CountyId == countyId && e.Active);

            result.Should().Be(expected);
        }

        [Fact]
        public async Task FacilityCount_ByMissingCounty_ReturnsZero()
        {
            const int countyId = 243;
            using var repository = new SimpleRepositoryHelper().GetFacilityRepository();
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
            using var repository = new SimpleRepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() {FacilityNumber = facilityNumberSpec};

            var result = await repository.CountAsync(spec);
            var expected = SimpleRepositoryData.Facilities
                .Count(e => e.FacilityNumber.ToLower().Contains(facilityNumberSpec.ToLower()) && e.Active);

            result.Should().Be(expected);
        }

        [Fact]
        public async Task FacilityCount_ByInactiveFacilityNumber_ReturnsZero()
        {
            const string facilityNumber = "DEF";
            using var repository = new SimpleRepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() {FacilityNumber = facilityNumber};

            var result = await repository.CountAsync(spec);

            result.Should().Be(0);
        }

        [Fact]
        public async Task FacilityCount_ByMissingFacilityNumber_ReturnsZero()
        {
            const string facilityNumber = "zzz";
            using var repository = new SimpleRepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() {FacilityNumber = facilityNumber};

            var result = await repository.CountAsync(spec);

            result.Should().Be(0);
        }

        // GetFacilityPaginatedListAsync

        [Fact]
        public async Task FacilitySearch_DefaultSpec_ReturnsActiveFacilities()
        {
            using var repository = new SimpleRepositoryHelper().GetFacilityRepository();

            var result = await repository.GetFacilityPaginatedListAsync(new FacilitySpec(), 1, 999);
            var expectedCount = SimpleRepositoryData.Facilities.Count(e => e.Active);

            result.TotalCount.ShouldEqual(expectedCount);
            result.Items.Count.ShouldEqual(expectedCount);
            result.PageNumber.ShouldEqual(1);
            result.TotalPages.ShouldEqual(1);
        }

        [Fact]
        public async Task FacilitySearch_WithInactive_ReturnsAllFacilities()
        {
            using var repository = new SimpleRepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() {ActiveOnly = false};

            var result = await repository.GetFacilityPaginatedListAsync(spec, 1, 999);
            var expectedCount = SimpleRepositoryData.Facilities.Count;

            result.TotalCount.ShouldEqual(expectedCount);
            result.Items.Count.ShouldEqual(expectedCount);
            result.PageNumber.ShouldEqual(1);
            result.TotalPages.ShouldEqual(1);
        }

        [Fact]
        public async Task FacilitySearch_ByCounty_ReturnsCorrectList()
        {
            const int countySpec = 131;
            using var repository = new SimpleRepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() {CountyId = countySpec, ActiveOnly = false};

            var result = await repository.GetFacilityPaginatedListAsync(spec, 1, 999);
            var expectedCount = SimpleRepositoryData.Facilities.Count(e => e.CountyId == countySpec);

            result.TotalCount.ShouldEqual(expectedCount);
            result.Items.Count.ShouldEqual(expectedCount);
            result.PageNumber.ShouldEqual(1);
            result.TotalPages.ShouldEqual(1);
        }

        [Fact]
        public async Task FacilitySearch_ByMissingCounty_ReturnsNone()
        {
            const int countySpec = 243;
            using var repository = new SimpleRepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() {CountyId = countySpec, ActiveOnly = false};

            var result = await repository.GetFacilityPaginatedListAsync(spec, 1, 999);
            var expectedCount = SimpleRepositoryData.Facilities.Count(e => e.CountyId == countySpec);

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
            using var repository = new SimpleRepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() {FacilityNumber = facilityNumberSpec, ActiveOnly = false};

            var result = await repository.GetFacilityPaginatedListAsync(spec, 1, 999);
            var expectedCount = SimpleRepositoryData.Facilities
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
            using var repository = new SimpleRepositoryHelper().GetFacilityRepository();
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
            using var repository = new SimpleRepositoryHelper().GetFacilityRepository();
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
            using var repository = new SimpleRepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() {ActiveOnly = false};

            var result = await repository.GetFacilityPaginatedListAsync(spec, 2, 2);
            var expectedPages = (int) Math.Ceiling(SimpleRepositoryData.Facilities.Count / 2m);

            result.TotalCount.ShouldEqual(SimpleRepositoryData.Facilities.Count);
            result.Items.Count.ShouldEqual(2);
            result.PageNumber.ShouldEqual(2);
            result.TotalPages.ShouldEqual(expectedPages);
        }

        [Fact]
        public async Task FacilitySearch_BeyondLastPage_ReturnsEmptyList()
        {
            using var repository = new SimpleRepositoryHelper().GetFacilityRepository();
            var spec = new FacilitySpec() {ActiveOnly = false};

            var result = await repository.GetFacilityPaginatedListAsync(spec, 999, 999);

            result.TotalCount.ShouldEqual(SimpleRepositoryData.Facilities.Count);
            result.Items.Count.ShouldEqual(0);
            result.PageNumber.ShouldEqual(999);
            result.TotalPages.ShouldEqual(1);
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
                using var repository = new RepositoryHelper().GetFacilityRepository();
                await repository.CreateFacilityAsync(newFacility);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("Facility Number is required.");
        }

        [Fact]
        public async Task CreateFacility_WithExistingNumber_ThrowsException()
        {
            var existingNumber = DataHelpers.Facilities[0].FacilityNumber;
            var facilityCreate = new FacilityCreateDto() {FacilityNumber = existingNumber};

            Func<Task> action = async () =>
            {
                using var repository = new RepositoryHelper().GetFacilityRepository();
                await repository.CreateFacilityAsync(facilityCreate);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage($"Facility Number '{facilityCreate.FacilityNumber}' already exists.");
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
                using var repository = new RepositoryHelper().GetFacilityRepository();
                await repository.CreateFacilityAsync(newFacility);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage($"File Label {newFacility.FileLabel} does not exist.");
        }

        // UpdateFacilityAsync

        [Fact]
        public async Task UpdateFacility_County_Succeeds()
        {
            var repositoryHelper = new RepositoryHelper();
            int newCountyId = 99;
            Guid facilityId = DataHelpers.Facilities[0].Id;

            using (var repository = repositoryHelper.GetFacilityRepository())
            {
                var facility = DataHelpers.GetFacilityDetail(facilityId);
                var updates = new FacilityEditDto(facility) {CountyId = newCountyId};

                await repository.UpdateFacilityAsync(facilityId, updates);
            }

            using (var repository = repositoryHelper.GetFacilityRepository())
            {
                var expected = DataHelpers.GetFacilityDetail(facilityId);
                expected.County = DataHelpers.GetCounty(newCountyId);

                var updatedFacility = await repository.GetFacilityAsync(facilityId);

                updatedFacility.Should().BeEquivalentTo(expected);
            }
        }

        [Fact]
        public async Task UpdateFacility_State_Succeeds()
        {
            var repositoryHelper = new RepositoryHelper();
            string newState = "Alabama";
            Guid facilityId = DataHelpers.Facilities[0].Id;

            using (var repository = repositoryHelper.GetFacilityRepository())
            {
                var facility = DataHelpers.GetFacilityDetail(facilityId);
                var updates = new FacilityEditDto(facility) {State = newState};

                await repository.UpdateFacilityAsync(facilityId, updates);
            }

            using (var repository = repositoryHelper.GetFacilityRepository())
            {
                var expected = DataHelpers.GetFacilityDetail(facilityId);
                expected.State = newState;

                var updatedFacility = await repository.GetFacilityAsync(facilityId);

                updatedFacility.Should().BeEquivalentTo(expected);
            }
        }

        [Fact]
        public async Task UpdateFacility_ChangeFile_Succeeds()
        {
            var repositoryHelper = new RepositoryHelper();
            Guid facilityId = DataHelpers.Facilities[0].Id;
            File newFile = DataHelpers.Files.Single(e => e.FileLabel == "248-0001");

            using (var repository = repositoryHelper.GetFacilityRepository())
            {
                var facility = DataHelpers.GetFacilityDetail(facilityId);
                var updates = new FacilityEditDto(facility) {FileLabel = newFile.FileLabel};

                await repository.UpdateFacilityAsync(facilityId, updates);
            }

            using (var repository = repositoryHelper.GetFacilityRepository())
            {
                var updatedFacility = await repository.GetFacilityAsync(facilityId);

                updatedFacility.FileLabel.Should().Be(newFile.FileLabel);
                updatedFacility.FileId.Should().Be(newFile.Id);
            }
        }

        [Fact]
        public async Task UpdateFacility_WithBlankFile_SucceedsAndCreatesNewFile()
        {
            var repositoryHelper = new RepositoryHelper();
            Guid facilityId = DataHelpers.Facilities[0].Id;

            using (var repository = repositoryHelper.GetFacilityRepository())
            {
                var facility = DataHelpers.GetFacilityDetail(facilityId);
                var updates = new FacilityEditDto(facility) {FileLabel = ""};

                await repository.UpdateFacilityAsync(facilityId, updates);
            }

            using (var repository = repositoryHelper.GetFacilityRepository())
            {
                var updatedFacility = await repository.GetFacilityAsync(facilityId);

                updatedFacility.FileLabel.ShouldNotBeNull();
                updatedFacility.FileLabel.Should().StartWith(DataHelpers.Facilities[0].CountyId.ToString());
            }
        }

        [Fact]
        public async Task UpdateFacility_NonexistentId_ThrowsException()
        {
            var updates = new FacilityEditDto() {CountyId = 99};

            Func<Task> action = async () =>
            {
                using var repository = new RepositoryHelper().GetFacilityRepository();
                await repository.UpdateFacilityAsync(default, updates);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("Facility ID not found. (Parameter 'id')");
        }

        [Fact]
        public async Task UpdateFacility_WithNonexistentFile_ThrowsException()
        {
            const string newFileLabel = "999-9999";

            var facilityId = DataHelpers.Facilities[0].Id;
            var facility = DataHelpers.GetFacilityDetail(facilityId);
            var updates = new FacilityEditDto(facility) {FileLabel = newFileLabel};

            Func<Task> action = async () =>
            {
                using var repository = new RepositoryHelper().GetFacilityRepository();
                await repository.UpdateFacilityAsync(facilityId, updates);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage($"File Label {newFileLabel} does not exist.");
        }

        [Fact]
        public async Task UpdateFacility_WithExistingNumber_ThrowsException()
        {
            var existingNumber = DataHelpers.Facilities[1].FacilityNumber;
            var facilityId = DataHelpers.Facilities[0].Id;
            var facility = DataHelpers.GetFacilityDetail(facilityId);
            var updates = new FacilityEditDto(facility) {FacilityNumber = existingNumber};

            Func<Task> action = async () =>
            {
                using var repository = new RepositoryHelper().GetFacilityRepository();
                await repository.UpdateFacilityAsync(facilityId, updates);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage($"Facility Number '{existingNumber}' already exists.");
        }

        // FacilityNumberExists

        [Fact]
        public async Task FacilityNumberExists_Unique_ReturnsFalse()
        {
            using var repository = new SimpleRepositoryHelper().GetFacilityRepository();
            var result = await repository.FacilityNumberExists("Unique", default);
            result.ShouldBeFalse();
        }

        [Fact]
        public async Task FacilityNumberExists_Duplicate_ReturnsTrue()
        {
            using var repository = new SimpleRepositoryHelper().GetFacilityRepository();
            var result = await repository.FacilityNumberExists("ABC", default);
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task FacilityNumberExists_DuplicateIsIgnored_ReturnsFalse()
        {
            using var repository = new SimpleRepositoryHelper().GetFacilityRepository();
            var ignoreId = SimpleRepositoryData.Facilities[0].Id;
            var facName = SimpleRepositoryData.Facilities[0].Name;
            var result = await repository.FacilityNumberExists(facName, ignoreId);
            result.ShouldBeFalse();
        }

        // FileLabelExists

        [Fact]
        public async Task FileLabelExists_Unique_ReturnsFalse()
        {
            using var repository = new SimpleRepositoryHelper().GetFacilityRepository();
            var result = await repository.FileLabelExists("999-9999");
            result.ShouldBeFalse();
        }

        [Fact]
        public async Task FileLabelExists_Duplicate_ReturnsTrue()
        {
            using var repository = new SimpleRepositoryHelper().GetFacilityRepository();
            var fileLabel = SimpleRepositoryData.Files[0].FileLabel;
            var result = await repository.FileLabelExists(fileLabel);
            result.ShouldBeTrue();
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