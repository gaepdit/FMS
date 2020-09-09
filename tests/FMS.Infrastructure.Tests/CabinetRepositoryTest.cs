using FluentAssertions;
using FMS.Domain.Dto;
using System;
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
        public async Task ExistingCabinet_Exists_ReturnsTrue()
        {
            using var repository = new RepositoryHelper().GetCabinetRepository();
            var result = await repository.CabinetExistsAsync(DataHelpers.Cabinets[0].Id);
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task NonexistantCabinet_Exists_ReturnsFalse()
        {
            using var repository = new RepositoryHelper().GetCabinetRepository();
            var result = await repository.CabinetExistsAsync(default);
            result.ShouldBeFalse();
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
        public async Task GetNonexistantCabinet_ReturnsNull()
        {
            using var repository = new RepositoryHelper().GetCabinetRepository();
            var result = await repository.GetCabinetAsync(default);
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
            var cabinetEdit = new CabinetEditDto(cabinet) { Name = newName };

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
            var cabinetEdit = new CabinetEditDto(cabinet) { Active = !cabinet.Active };

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
            var cabinetEdit = new CabinetEditDto(cabinet);

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
            var cabinetEdit = new CabinetEditDto(cabinet) { Name = "" };

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
            var cabinetEdit = new CabinetEditDto(cabinet) { Name = existingName };

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
