using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.Tests
{
    [TestFixture]
    public class LocationRepositoryTests
    {
        private FmsDbContext _context;
        private LocationRepository _repository;
        private bool _disposed = false;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<FmsDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;
            var httpContextAccessor = Substitute.For<HttpContextAccessor>();
            _context = new FmsDbContext(options, httpContextAccessor);
            _repository = new LocationRepository(_context);

            _context.Locations.Add(new Location
            {
                Id = Guid.NewGuid(),
                FacilityId = Guid.NewGuid(),
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
                _context.Database.EnsureCreated();
                _context.Dispose();
                _repository.Dispose();
                _disposed = true;
            }
        }

        // LocationExistAsync
        [Test]
        public async Task LocationExistAsync_ReturnTrue_LocationExist()
        {
            var existingLocation = await _context.Locations.Select(ft => ft.Id).FirstAsync();
            var results = await _repository.LocationExistsAsync(existingLocation);
            results.Should().BeTrue();
        }
        [Test]
        public async Task LocationExistAsync_ReturnFalse_LocationDoesNotExist()
        {
            var nonExistingLocation = Guid.NewGuid();
            var results = await _repository.LocationExistsAsync(nonExistingLocation);
            results.Should().BeFalse();
        }

        // GetLocationByIdAsync
        [Test]
        public async Task GetLocationByIdAsync_WhenIdExist()
        {
            var existingLocation = await _context.Locations.FirstAsync();
            var result = await _repository.GetLocationByIdAsync(existingLocation.Id);

            result.Should().NotBeNull();
            result.Should().BeOfType<LocationEditDto>();
            result.Id.Should().Be(existingLocation.Id);
        }
        [Test]
        public async Task GetLocationByIdAsync_WhenIdDoesNotExist_ReturnsNull()
        {
            var nonExistingId = Guid.NewGuid();
            var result = await _repository.GetLocationByIdAsync(nonExistingId);

            result.Should().BeNull();
        }

        // GetLocationByFacilityIdAsync
        [Test]
        public async Task GetLocationByFacilityIdAsync_WhenFacilityIdExist()
        {
            var existingLocation = await _context.Locations.FirstAsync();
            var result = await _repository.GetLocationByFacilityIdAsync(existingLocation.FacilityId);

            result.Should().NotBeNull();
            result.Should().BeOfType<LocationEditDto>();
            result.Id.Should().Be(existingLocation.Id);
        }
        [Test]
        public async Task GetLocationByFacilityIdAsync_WhenFacilityIdDoesNotExist_ReturnsNull()
        {
            var nonExistingFacilityId = Guid.NewGuid();
            var result = await _repository.GetLocationByFacilityIdAsync(nonExistingFacilityId);

            result.Should().BeNull();
        }

        // CreateLocationAsync
        [Test]
        public async Task CreateLocationAsync_CreateNewLocation_WhenDataIsValid()
        {
            var dto = new LocationCreateDto { FacilityId = Guid.NewGuid(), LocationClassId = Guid.NewGuid() };

            var newId = await _repository.CreateLocationAsync(dto);
            var createdLocation = await _context.Locations.FindAsync(newId);

            createdLocation.Should().NotBeNull();
            createdLocation.LocationClassId.Should().Be(dto.LocationClassId);
        }

        // UpdateLocationAsync
        [Test]
        public async Task UpdateLocationAsync_UpdatesExistingLocation_WhenDataIsValid()
        {
            var existingLocation = new Location { Id = Guid.NewGuid(), FacilityId = Guid.NewGuid(), LocationClassId = Guid.NewGuid()};
            _context.Locations.Add(existingLocation);
            await _context.SaveChangesAsync();

            var updateDto = new LocationEditDto { LocationClassId = Guid.NewGuid() };
            await _repository.UpdateLocationAsync(existingLocation.FacilityId, updateDto);

            var updatedLocation = await _context.Locations.FindAsync(existingLocation.Id);
            updatedLocation.LocationClassId.Should().Be(updateDto.LocationClassId);
        }
        [Test]
        public async Task UpdateLocationAsync_ThrowsArgumentException_WhenIdDoesNotExist()
        {
            var invalidId = Guid.NewGuid();
            var updateDto = new LocationEditDto { FacilityId = Guid.NewGuid(), LocationClassId = Guid.NewGuid() };

            Func<Task> action = async () => await _repository.UpdateLocationAsync(invalidId, updateDto);
            await action.Should().ThrowAsync<NullReferenceException>();
        }

        // UpdateLocationStatusAsync
        [Test]
        public async Task UpdateLocationStatusAsync_UpdatesStatusCorrectly()
        {
            var newLocation = new Location { Id = Guid.NewGuid(), LocationClassId = Guid.NewGuid(), Active = true };
            _context.Locations.Add(newLocation);
            await _context.SaveChangesAsync();

            await _repository.UpdateLocationStatusAsync(newLocation.Id, false);

            var updatedLocation = await _context.Locations.FindAsync(newLocation.Id);
            updatedLocation.Active.Should().BeFalse();
        }

        [Test]
        public async Task UpdateLocationStatusAsync_ThrowsArgumentException_WhenIdDoesNotExist()
        {
            Func<Task> action = async () => await _repository.UpdateLocationStatusAsync(Guid.NewGuid(), false);
            await action.Should().ThrowAsync<KeyNotFoundException>();
        }

    }
}
