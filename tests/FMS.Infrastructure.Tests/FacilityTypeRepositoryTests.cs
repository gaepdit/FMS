using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.Tests
{
    [TestFixture]
    public class FacilityTypeRepositoryTests
    {
        private FmsDbContext _context;
        private FacilityTypeRepository _repository;
        private bool _disposed = false;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<FmsDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;

            var httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            _context = new FmsDbContext(options, httpContextAccessor);
            _repository = new FacilityTypeRepository(_context);

            _context.FacilityTypes.Add(new FacilityType
            {
                Id = Guid.NewGuid(),
                Name = "Initial Facility",
                Description = "Initial description",
                Active = true
            });
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _context.Database.EnsureDeleted();
                _context.Dispose();
                _repository.Dispose();
                _disposed = true;
            }
        }

        // Test for FacilityTypeExistsAsync
        [Test]
        public async Task FacilityTypeExistsAsync_ReturnsTrue_WhenFacilityTypeExists()
        {
            var existingId = await _context.FacilityTypes.Select(ft => ft.Id).FirstAsync();
            var result = await _repository.FacilityTypeExistsAsync(existingId);
            result.Should().BeTrue();
        }

        [Test]
        public async Task FacilityTypeExistsAsync_ReturnsFalse_WhenFacilityTypeDoesNotExist()
        {
            var nonExistentId = Guid.NewGuid();
            var result = await _repository.FacilityTypeExistsAsync(nonExistentId);
            result.Should().BeFalse();
        }

        // Test for FacilityTypeNameExistsAsync
        [Test]
        public async Task FacilityTypeNameExistsAsync_ReturnsTrue_WhenNameExists()
        {
            var result = await _repository.FacilityTypeNameExistsAsync("Initial Facility");
            result.Should().BeTrue();
        }

        [Test]
        public async Task FacilityTypeNameExistsAsync_ReturnsFalse_WhenNameDoesNotExist()
        {
            var result = await _repository.FacilityTypeNameExistsAsync("NonExistentName");
            result.Should().BeFalse();
        }

        [Test]
        public async Task FacilityTypeNameExistsAsync_IgnoresGivenId_WhenChecking()
        {
            var facilityType = await _context.FacilityTypes.FirstAsync();
            var result = await _repository.FacilityTypeNameExistsAsync("Initial Facility", facilityType.Id);
            result.Should().BeFalse();
        }

        // Test for FacilityTypeDescriptionExistsAsync
        [Test]
        public async Task FacilityTypeDescriptionExistsAsync_ReturnsTrue_WhenDescriptionExists()
        {
            var result = await _repository.FacilityTypeDescriptionExistsAsync("Initial description");
            result.Should().BeTrue();
        }

        [Test]
        public async Task FacilityTypeDescriptionExistsAsync_ReturnsFalse_WhenDescriptionDoesNotExist()
        {
            var result = await _repository.FacilityTypeDescriptionExistsAsync("NonExistentDescription");
            result.Should().BeFalse();
        }

        [Test]
        public async Task FacilityTypeDescriptionExistsAsync_IgnoresGivenId_WhenChecking()
        {
            var facilityType = await _context.FacilityTypes.FirstAsync();
            var result = await _repository.FacilityTypeDescriptionExistsAsync("Initial description", facilityType.Id);
            result.Should().BeFalse();
        }

        // Test for GetFacilityTypeAsync
        [Test]
        public async Task GetFacilityTypeAsync_ReturnsCorrectFacilityType_WhenIdIsValid()
        {
            var facilityType = await _context.FacilityTypes.FirstAsync();

            var result = await _repository.GetFacilityTypeAsync(facilityType.Id);
            result.Should().NotBeNull();
            result.Name.Should().Be(facilityType.Name);
            result.Description.Should().Be(facilityType.Description);
        }

        [Test]
        public async Task GetFacilityTypeAsync_ReturnsNull_WhenIdIsInvalid()
        {
            var result = await _repository.GetFacilityTypeAsync(Guid.NewGuid());
            result.Should().BeNull();
        }

        // Test for GetFacilityTypeNameAsync
        [Test]
        public async Task GetFacilityTypeNameAsync_ReturnsName_WhenIdIsValid()
        {
            var facilityType = await _context.FacilityTypes.FirstAsync();
            var result = await _repository.GetFacilityTypeNameAsync(facilityType.Id);
            result.Should().Be(facilityType.Name);
        }

        [Test]
        public async Task GetFacilityTypeNameAsync_ReturnsNull_WhenIdIsInvalid()
        {
            var result = await _repository.GetFacilityTypeNameAsync(Guid.NewGuid());
            result.Should().BeNull();
        }

        // Test for GetFacilityTypeListAsync
        [Test]
        public async Task GetFacilityTypeListAsync_ReturnsAllFacilityTypes()
        {
            var result = await _repository.GetFacilityTypeListAsync();
            result.Should().NotBeNullOrEmpty();
        }

        // Tests for CreateFacilityTypeAsync
        [Test]
        public async Task CreateFacilityTypeAsync_CreatesNewFacilityType_WhenDataIsValid()
        {
            var dto = new FacilityTypeCreateDto { Name = "UniqueName", Description = "Unique Description" };

            var newId = await _repository.CreateFacilityTypeAsync(dto);
            var createdFacilityType = await _context.FacilityTypes.FindAsync(newId);

            createdFacilityType.Should().NotBeNull();
            createdFacilityType.Name.Should().Be("UniqueName");
            createdFacilityType.Description.Should().Be("Unique Description");
        }

        [Test]
        public void CreateFacilityTypeAsync_ThrowsArgumentException_WhenNameAlreadyExists()
        {
            var existingFacilityType = new FacilityType { Id = Guid.NewGuid(), Name = "DuplicateName", Description = "Existing Description" };
            _context.FacilityTypes.Add(existingFacilityType);
            _context.SaveChanges();

            var dto = new FacilityTypeCreateDto { Name = "DuplicateName", Description = "New Description" };

            Func<Task> action = async () => await _repository.CreateFacilityTypeAsync(dto);
            action.Should().ThrowAsync<ArgumentException>().WithMessage("Facility Type 'DuplicateName' already exists.");
        }

        [Test]
        public void CreateFacilityTypeAsync_ThrowsArgumentException_WhenDescriptionAlreadyExists()
        {
            var existingFacilityType = new FacilityType { Id = Guid.NewGuid(), Name = "UniqueName", Description = "Duplicate Description" };
            _context.FacilityTypes.Add(existingFacilityType);
            _context.SaveChanges();

            var dto = new FacilityTypeCreateDto { Name = "New Name", Description = "Duplicate Description" };

            Func<Task> action = async () => await _repository.CreateFacilityTypeAsync(dto);
            action.Should().ThrowAsync<ArgumentException>().WithMessage("Facility Type description 'Duplicate Description' already exists.");
        }

        // Tests for UpdateFacilityTypeAsync
        [Test]
        public async Task UpdateFacilityTypeAsync_UpdatesExistingFacilityType_WhenDataIsValid()
        {
            var existingFacilityType = new FacilityType { Id = Guid.NewGuid(), Name = "OriginalName", Description = "Original Description" };
            _context.FacilityTypes.Add(existingFacilityType);
            await _context.SaveChangesAsync();

            var updateDto = new FacilityTypeEditDto { Name = "UpdatedName", Description = "Updated Description" };
            await _repository.UpdateFacilityTypeAsync(existingFacilityType.Id, updateDto);

            var updatedFacilityType = await _context.FacilityTypes.FindAsync(existingFacilityType.Id);
            updatedFacilityType.Name.Should().Be("UpdatedName");
            updatedFacilityType.Description.Should().Be("Updated Description");
        }

        [Test]
        public void UpdateFacilityTypeAsync_ThrowsArgumentException_WhenIdDoesNotExist()
        {
            var updateDto = new FacilityTypeEditDto { Name = "NonExistent", Description = "NonExistent Description" };

            Func<Task> action = async () => await _repository.UpdateFacilityTypeAsync(Guid.NewGuid(), updateDto);
            action.Should().ThrowAsync<ArgumentException>().WithMessage("Facility Type ID not found.");
        }

        [Test]
        public void UpdateFacilityTypeAsync_ThrowsArgumentException_WhenNameAlreadyExists()
        {
            var facility1 = new FacilityType { Id = Guid.NewGuid(), Name = "Name1", Description = "Description1" };
            var facility2 = new FacilityType { Id = Guid.NewGuid(), Name = "Name2", Description = "Description2" };
            _context.FacilityTypes.AddRange(facility1, facility2);
            _context.SaveChanges();

            var updateDto = new FacilityTypeEditDto { Name = "Name2", Description = "Updated Description" };

            Func<Task> action = async () => await _repository.UpdateFacilityTypeAsync(facility1.Id, updateDto);
            action.Should().ThrowAsync<ArgumentException>().WithMessage("Facility Type 'Name2' already exists.");
        }

        // Tests for UpdateFacilityTypeStatusAsync
        [Test]
        public async Task UpdateFacilityTypeStatusAsync_UpdatesStatusCorrectly()
        {
            var facilityType = new FacilityType { Id = Guid.NewGuid(), Name = "StatusTest", Active = true };
            _context.FacilityTypes.Add(facilityType);
            await _context.SaveChangesAsync();

            await _repository.UpdateFacilityTypeStatusAsync(facilityType.Id, false);

            var updatedFacilityType = await _context.FacilityTypes.FindAsync(facilityType.Id);
            updatedFacilityType.Active.Should().BeFalse();
        }

        [Test]
        public void UpdateFacilityTypeStatusAsync_ThrowsArgumentException_WhenIdDoesNotExist()
        {
            Func<Task> action = async () => await _repository.UpdateFacilityTypeStatusAsync(Guid.NewGuid(), false);
            action.Should().ThrowAsync<ArgumentException>().WithMessage("Facility Type ID not found");
        }

    }
}
