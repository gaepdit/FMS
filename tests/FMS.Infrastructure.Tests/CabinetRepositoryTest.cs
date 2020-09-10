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
    public class CabinetRepositoryTest
    {
        // CabinetExistsAsync

        [Fact]
        public async Task CabinetExists_Exists_ReturnsTrue()
        {
            using var repository = new RepositoryHelper().GetCabinetRepository();
            var result = await repository.CabinetExistsAsync(DataHelpers.Cabinets[0].Id);
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task CabinetExists_NotExists_ReturnsFalse()
        {
            using var repository = new RepositoryHelper().GetCabinetRepository();
            var result = await repository.CabinetExistsAsync(default);
            result.ShouldBeFalse();
        }

        // CabinetNameExistsAsync

        [Fact]
        public async Task CabinetNameExists_Exists_ReturnsTrue()
        {
            using var repository = new RepositoryHelper().GetCabinetRepository();
            var result = await repository.CabinetNameExistsAsync(DataHelpers.Cabinets[0].Name);
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task CabinetNameExists_NotExists_ReturnsFalse()
        {
            using var repository = new RepositoryHelper().GetCabinetRepository();
            var result = await repository.CabinetNameExistsAsync("0");
            result.ShouldBeFalse();
        }

        // GetCabinetListAsync

        [Fact]
        public async Task GetCabinetList_ReturnsAllActive()
        {
            using var repository = new RepositoryHelper().GetCabinetRepository();
            var result = await repository.GetCabinetListAsync(false);
            var expected = DataHelpers.Cabinets.Where(e => e.Active)
                .Select(e => new CabinetSummaryDto(e));
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetCabinetList_WithInactive_ReturnsAll()
        {
            using var repository = new RepositoryHelper().GetCabinetRepository();
            var result = await repository.GetCabinetListAsync(true);
            var expected = DataHelpers.Cabinets
                .Select(e => new CabinetSummaryDto(e));
            result.Should().BeEquivalentTo(expected);
        }

        // GetCabinetAsync

        [Fact]
        public async Task GetCabinet_ReturnsCorrectCabinet()
        {
            using var repository = new RepositoryHelper().GetCabinetRepository();
            Guid CabinetId = DataHelpers.Cabinets[0].Id;

            var expected = DataHelpers.GetCabinet(CabinetId);
            var result = await repository.GetCabinetAsync(CabinetId);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetCabinet_Nonexistant_ReturnsNull()
        {
            using var repository = new RepositoryHelper().GetCabinetRepository();
            var result = await repository.GetCabinetAsync(default);
            result.ShouldBeNull();
        }

        // GetCabinetByNameAsync

        [Fact]
        public async Task GetCabinetByName_ReturnsCorrectCabinet()
        {
            using var repository = new RepositoryHelper().GetCabinetRepository();
            var cabinet = DataHelpers.Cabinets[0];

            var expected = DataHelpers.GetCabinet(cabinet.Id);
            var result = await repository.GetCabinetByNameAsync(cabinet.Name);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetCabinetByName_Nonexistant_ReturnsNull()
        {
            using var repository = new RepositoryHelper().GetCabinetRepository();
            var result = await repository.GetCabinetByNameAsync(null);
            result.ShouldBeNull();
        }

        // CreateCabinetAsync

        [Fact]
        public async Task CreateCabinet_Succeeds()
        {
            var repositoryHelper = new RepositoryHelper();
            Guid newId;
            var newName = "C000";
            var cabinetCreate = new CabinetCreateDto() { Name = newName };

            using (var repository = repositoryHelper.GetCabinetRepository())
            {
                newId = await repository.CreateCabinetAsync(cabinetCreate);
            }

            using (var repository = repositoryHelper.GetCabinetRepository())
            {
                var cabinet = await repository.GetCabinetAsync(newId);

                cabinet.Id.Should().Be(newId);
                cabinet.Name.Should().Be(newName);
                cabinet.Active.ShouldBeTrue();
            }
        }

        [Fact]
        public async Task CreateCabinet_WithEmptyName_ThrowsException()
        {
            using var repository = new RepositoryHelper().GetCabinetRepository();
            var cabinetCreate = new CabinetCreateDto();

            Func<Task> action = async () =>
            {
                var result = await repository.CreateCabinetAsync(cabinetCreate);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("Cabinet Name can not be null or empty.");
        }

        [Fact]
        public async Task CreateCabinet_WithExistingName_ThrowsException()
        {
            using var repository = new RepositoryHelper().GetCabinetRepository();
            var existingName = DataHelpers.Cabinets[0].Name;
            var cabinetCreate = new CabinetCreateDto() { Name = existingName };

            Func<Task> action = async () =>
            {
                var result = await repository.CreateCabinetAsync(cabinetCreate);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage($"Cabinet Name '{cabinetCreate.Name}' already exists.");
        }

        // UpdateCabinetAsync

        [Fact]
        public async Task UpdateCabinet_Name_Succeeds()
        {
            var repositoryHelper = new RepositoryHelper();

            var cabinet = DataHelpers.Cabinets[0];
            var newName = "C000";
            var cabinetEdit = new CabinetEditDto()
            {
                Active = cabinet.Active,
                Name = newName
            };

            using (var repository = repositoryHelper.GetCabinetRepository())
            {
                await repository.UpdateCabinetAsync(cabinet.Id, cabinetEdit);
            }

            using (var repository = repositoryHelper.GetCabinetRepository())
            {
                var updatedCabinet = await repository.GetCabinetAsync(cabinet.Id);
                updatedCabinet.Name.Should().Be(newName);
                updatedCabinet.Active.ShouldBeTrue();
            }
        }

        [Fact]
        public async Task UpdateCabinet_Active_Succeeds()
        {
            var repositoryHelper = new RepositoryHelper();

            var cabinet = DataHelpers.Cabinets[0];
            var cabinetEdit = new CabinetEditDto()
            {
                Active = !cabinet.Active,
                Name = cabinet.Name
            };

            using (var repository = repositoryHelper.GetCabinetRepository())
            {
                await repository.UpdateCabinetAsync(cabinet.Id, cabinetEdit);
            }

            using (var repository = repositoryHelper.GetCabinetRepository())
            {
                var updatedCabinet = await repository.GetCabinetAsync(cabinet.Id);
                updatedCabinet.Name.Should().Be(cabinet.Name);
                updatedCabinet.Active.Should().Be(!cabinet.Active);
            }
        }

        [Fact]
        public async Task UpdateCabinet_WithNoChanges_Succeeds()
        {
            var repositoryHelper = new RepositoryHelper();

            var cabinet = DataHelpers.Cabinets[0];
            var cabinetEdit = new CabinetEditDto()
            {
                Active = cabinet.Active,
                Name = cabinet.Name
            };

            using (var repository = repositoryHelper.GetCabinetRepository())
            {
                await repository.UpdateCabinetAsync(cabinet.Id, cabinetEdit);
            }

            using (var repository = repositoryHelper.GetCabinetRepository())
            {
                var updatedCabinet = await repository.GetCabinetAsync(cabinet.Id);
                updatedCabinet.Name.Should().Be(cabinet.Name);
                updatedCabinet.Active.Should().Be(cabinet.Active);
            }
        }

        [Fact]
        public async Task UpdateCabinet_WithEmptyName_ThrowsException()
        {
            using var repository = new RepositoryHelper().GetCabinetRepository();

            var cabinet = DataHelpers.Cabinets[0];
            var cabinetEdit = new CabinetEditDto()
            {
                Active = cabinet.Active,
                Name = ""
            };

            Func<Task> action = async () =>
            {
                await repository.UpdateCabinetAsync(cabinet.Id, cabinetEdit);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("Cabinet Name can not be null or empty.");
        }

        [Fact]
        public async Task UpdateCabinet_WithExistingName_ThrowsException()
        {
            using var repository = new RepositoryHelper().GetCabinetRepository();
            var existingName = DataHelpers.Cabinets[1].Name;

            var cabinet = DataHelpers.Cabinets[0];
            var cabinetEdit = new CabinetEditDto()
            {
                Active = cabinet.Active,
                Name = existingName
            };

            Func<Task> action = async () =>
            {
                await repository.UpdateCabinetAsync(cabinet.Id, cabinetEdit);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage($"Cabinet Name '{cabinetEdit.Name}' already exists.");
        }

        [Fact]
        public async Task UpdateNonexistantCabinet_ThrowsException()
        {
            using var repository = new RepositoryHelper().GetCabinetRepository();
            var cabinetEdit = new CabinetEditDto() { Name = "C000" };

            Func<Task> action = async () =>
            {
                await repository.UpdateCabinetAsync(default, cabinetEdit);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("Cabinet ID not found.");
        }
    }
}
