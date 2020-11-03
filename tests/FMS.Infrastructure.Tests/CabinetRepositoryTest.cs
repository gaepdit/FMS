using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Dto;
using TestHelpers.SimpleRepository;
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
            using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
            var result = await repository.CabinetExistsAsync(SimpleRepositoryData.Cabinets[0].Id);
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task CabinetExists_NotExists_ReturnsFalse()
        {
            using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
            var result = await repository.CabinetExistsAsync(Guid.Empty);
            result.ShouldBeFalse();
        }

        // CabinetNameExistsAsync

        [Fact]
        public async Task CabinetNameExists_NotExists_ReturnsFalse()
        {
            using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
            var result = await repository.CabinetNameExistsAsync("New");
            result.ShouldBeFalse();
        }

        [Fact]
        public async Task CabinetNameExists_Exists_ReturnsTrue()
        {
            using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
            var cabinet = SimpleRepositoryData.Cabinets[0];

            var result = await repository.CabinetNameExistsAsync(cabinet.Name);
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task CabinetNameExists_ExistsButIgnored_ReturnsFalse()
        {
            using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
            var cabinet = SimpleRepositoryData.Cabinets[0];

            var result = await repository.CabinetNameExistsAsync(cabinet.Name, cabinet.Id);
            result.ShouldBeFalse();
        }

        [Fact]
        public async Task CabinetNameExists_NotExistsNotIgnored_ReturnsFalse()
        {
            using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
            var result = await repository.CabinetNameExistsAsync("New", Guid.Empty);
            result.ShouldBeFalse();
        }

        [Fact]
        public async Task CabinetNameExists_ExistsNotIgnored_ReturnsTrue()
        {
            using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
            var cabinet = SimpleRepositoryData.Cabinets[0];

            var result = await repository.CabinetNameExistsAsync(cabinet.Name, Guid.Empty);
            result.ShouldBeTrue();
        }

        // GetCabinetListAsync

        [Fact]
        public async Task GetCabinetList_ReturnsAll()
        {
            using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
            var result = await repository.GetCabinetListAsync();
            var expected = SimpleRepositoryData.Cabinets
                .Select(e => new CabinetSummaryDto(e));
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetCabinetList_WithoutInactive_ReturnsAll()
        {
            using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
            var result = await repository.GetCabinetListAsync(false);
            var expected = SimpleRepositoryData.Cabinets
                .Where(e => e.Active)
                .Select(e => new CabinetSummaryDto(e));
            result.Should().BeEquivalentTo(expected);
        }

        // GetCabinetSummaryAsync (by Guid)

        [Fact]
        public async Task GetCabinetSummary_ReturnsCorrectCabinet()
        {
            using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
            var cabinet = SimpleRepositoryData.Cabinets[0];

            var expected = SimpleRepositoryData.GetCabinetSummary(cabinet.Id);
            var result = await repository.GetCabinetSummaryAsync(cabinet.Id);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetCabinetSummary_Nonexistent_ReturnsNull()
        {
            using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
            var result = await repository.GetCabinetSummaryAsync(Guid.Empty);
            result.ShouldBeNull();
        }

        // GetCabinetDetailsAsync (by cabinet number)

        [Fact]
        public async Task GetCabinetByNumber_ReturnsCorrectCabinet()
        {
            using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
            var cabinet = SimpleRepositoryData.Cabinets[0];

            var expected = SimpleRepositoryData.GetCabinetDetail(cabinet.Id);
            var result = await repository.GetCabinetDetailsAsync(cabinet.Name);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetCabinetByNumber_Nonexistent_ReturnsNull()
        {
            using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
            var result = await repository.GetCabinetDetailsAsync("zzz");
            result.ShouldBeNull();
        }

        // CreateCabinetAsync

        [Fact]
        public async Task CreateCabinet_Succeeds()
        {
            var repositoryHelper = new SimpleRepositoryHelper();

            var newCabinet = new CabinetEditDto
            {
                Name = "New Cabinet",
                FirstFileLabel = "000-0000",
            };

            using (var repository = repositoryHelper.GetCabinetRepository())
            {
                await repository.CreateCabinetAsync(newCabinet);
            }

            using (var repository = repositoryHelper.GetCabinetRepository())
            {
                var cabinet = await repository.GetCabinetDetailsAsync(newCabinet.Name);

                cabinet.Active.ShouldBeTrue();
                cabinet.Name.Should().Be(newCabinet.Name);
                cabinet.FirstFileLabel.Should().Be(newCabinet.FirstFileLabel);
            }
        }

        [Fact]
        public async Task CreateCabinet_WithNullName_ThrowsException()
        {
            var newCabinet = new CabinetEditDto
            {
                Name = null,
                FirstFileLabel = "000-0000",
            };

            Func<Task> action = async () =>
            {
                using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
                await repository.CreateCabinetAsync(newCabinet);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("Value cannot be null. (Parameter 'Name')");
        }

        [Fact]
        public async Task CreateCabinet_WithNullLabel_ThrowsException()
        {
            var newCabinet = new CabinetEditDto
            {
                Name = "C",
                FirstFileLabel = null,
            };

            Func<Task> action = async () =>
            {
                using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
                await repository.CreateCabinetAsync(newCabinet);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("Value cannot be null. (Parameter 'FirstFileLabel')");
        }

        [Fact]
        public async Task CreateCabinet_WithExistingName_ThrowsException()
        {
            var cabinet = SimpleRepositoryData.Cabinets[0];

            var newCabinet = new CabinetEditDto
            {
                Name = cabinet.Name,
                FirstFileLabel = "000-0000",
            };

            Func<Task> action = async () =>
            {
                using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
                await repository.CreateCabinetAsync(newCabinet);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage($"Cabinet Name {cabinet.Name} already exists.");
        }

        [Fact]
        public async Task CreateCabinet_WithInvalidLabel_ThrowsException()
        {
            var newCabinet = new CabinetEditDto
            {
                Name = "New",
                FirstFileLabel = "00-00",
            };

            Func<Task> action = async () =>
            {
                using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
                await repository.CreateCabinetAsync(newCabinet);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("The File Label is invalid.");
        }

        // UpdateCabinetAsync

        [Fact]
        public async Task UpdateCabinet_Label_Succeeds()
        {
            var repositoryHelper = new SimpleRepositoryHelper();
            var cabinet = SimpleRepositoryData.Cabinets[0];

            var cabinetEdit = new CabinetEditDto()
            {
                Name = cabinet.Name,
                FirstFileLabel = "222-2222",
            };

            using (var repository = repositoryHelper.GetCabinetRepository())
            {
                await repository.UpdateCabinetAsync(cabinet.Id, cabinetEdit);
            }

            using (var repository = repositoryHelper.GetCabinetRepository())
            {
                var updatedCabinet = await repository.GetCabinetSummaryAsync(cabinet.Id);
                updatedCabinet.Name.Should().Be(cabinetEdit.Name);
                updatedCabinet.FirstFileLabel.Should().Be(cabinetEdit.FirstFileLabel);
                updatedCabinet.Active.Should().Be(cabinet.Active);
            }
        }

        [Fact]
        public async Task UpdateCabinet_Name_Succeeds()
        {
            var repositoryHelper = new SimpleRepositoryHelper();
            var cabinet = SimpleRepositoryData.Cabinets[0];

            var cabinetEdit = new CabinetEditDto()
            {
                Name = "C",
                FirstFileLabel = cabinet.FirstFileLabel,
            };

            using (var repository = repositoryHelper.GetCabinetRepository())
            {
                await repository.UpdateCabinetAsync(cabinet.Id, cabinetEdit);
            }

            using (var repository = repositoryHelper.GetCabinetRepository())
            {
                var updatedCabinet = await repository.GetCabinetSummaryAsync(cabinet.Id);
                updatedCabinet.Name.Should().Be(cabinetEdit.Name);
                updatedCabinet.FirstFileLabel.Should().Be(cabinetEdit.FirstFileLabel);
                updatedCabinet.Active.Should().Be(cabinet.Active);
            }
        }

        [Fact]
        public async Task UpdateCabinet_WithNoChanges_Succeeds()
        {
            var repositoryHelper = new SimpleRepositoryHelper();
            var cabinet = SimpleRepositoryData.Cabinets[0];

            var cabinetEdit = new CabinetEditDto()
            {
                Name = cabinet.Name,
                FirstFileLabel = cabinet.FirstFileLabel,
            };

            using (var repository = repositoryHelper.GetCabinetRepository())
            {
                await repository.UpdateCabinetAsync(cabinet.Id, cabinetEdit);
            }

            using (var repository = repositoryHelper.GetCabinetRepository())
            {
                var updatedCabinet = await repository.GetCabinetDetailsAsync(cabinet.Name);
                updatedCabinet.Name.Should().Be(cabinetEdit.Name);
                updatedCabinet.FirstFileLabel.Should().Be(cabinetEdit.FirstFileLabel);
                updatedCabinet.Active.Should().Be(cabinet.Active);
            }
        }

        [Fact]
        public async Task UpdateCabinet_WithNullName_ThrowsException()
        {
            var cabinet = SimpleRepositoryData.Cabinets[0];
            var cabinetEdit = new CabinetEditDto()
            {
                Name = null,
                FirstFileLabel = "000-0000",
            };

            Func<Task> action = async () =>
            {
                using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
                await repository.UpdateCabinetAsync(cabinet.Id, cabinetEdit);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("Value cannot be null. (Parameter 'Name')");
        }

        [Fact]
        public async Task UpdateCabinet_WithNullFileLabel_ThrowsException()
        {
            var cabinet = SimpleRepositoryData.Cabinets[0];
            var cabinetEdit = new CabinetEditDto()
            {
                Name = "C",
                FirstFileLabel = null,
            };

            Func<Task> action = async () =>
            {
                using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
                await repository.UpdateCabinetAsync(cabinet.Id, cabinetEdit);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("Value cannot be null. (Parameter 'FirstFileLabel')");
        }

        [Fact]
        public async Task UpdateCabinet_WithExistingName_ThrowsException()
        {
            var cabinet = SimpleRepositoryData.Cabinets[0];
            var existingName = SimpleRepositoryData.Cabinets[1].Name;
            var cabinetEdit = new CabinetEditDto()
            {
                Name = existingName,
                FirstFileLabel = cabinet.FirstFileLabel,
            };

            Func<Task> action = async () =>
            {
                using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
                await repository.UpdateCabinetAsync(cabinet.Id, cabinetEdit);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage($"Cabinet Name {existingName} already exists.");
        }

        [Fact]
        public async Task UpdateCabinet_WithInvalidFileLabel_ThrowsException()
        {
            var cabinet = SimpleRepositoryData.Cabinets[0];
            var cabinetEdit = new CabinetEditDto()
            {
                Name = cabinet.Name,
                FirstFileLabel = "00-00",
            };

            Func<Task> action = async () =>
            {
                using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
                await repository.UpdateCabinetAsync(cabinet.Id, cabinetEdit);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("The File Label is invalid.");
        }

        [Fact]
        public async Task UpdateNonexistentCabinet_ThrowsException()
        {
            var cabinetEdit = new CabinetEditDto()
            {
                Name = "C",
                FirstFileLabel = "000-0000",
            };

            Func<Task> action = async () =>
            {
                using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
                await repository.UpdateCabinetAsync(Guid.Empty, cabinetEdit);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("Cabinet ID not found.");
        }
    }
}