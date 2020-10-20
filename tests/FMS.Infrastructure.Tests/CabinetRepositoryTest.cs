using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Dto;
using TestHelpers;
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
            var result = await repository.CabinetExistsAsync(default);
            result.ShouldBeFalse();
        }

        // GetCabinetListAsync

        [Fact]
        public async Task GetCabinetList_ReturnsAllActive()
        {
            using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
            var result = await repository.GetCabinetListAsync();
            var expected = SimpleRepositoryData.Cabinets
                .Where(e => e.Active)
                .Select(e => new CabinetSummaryDto(e));
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetCabinetList_WithInactive_ReturnsAll()
        {
            using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
            var result = await repository.GetCabinetListAsync(true);
            var expected = SimpleRepositoryData.Cabinets
                .Select(e => new CabinetSummaryDto(e));
            result.Should().BeEquivalentTo(expected);
        }

        // GetCabinetSummaryAsync

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

        // GetCabinetDetailsAsync (by Guid)

        [Fact]
        public async Task GetCabinetDetails_ReturnsCorrectCabinet()
        {
            using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
            var cabinet = SimpleRepositoryData.Cabinets[0];

            var expected = SimpleRepositoryData.GetCabinetDetail(cabinet.Id);
            var result = await repository.GetCabinetDetailsAsync(cabinet.Id);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetCabinetDetails_Nonexistent_ReturnsNull()
        {
            using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
            var result = await repository.GetCabinetDetailsAsync(Guid.Empty);
            result.ShouldBeNull();
        }

        // GetCabinetDetailsAsync (by cabinet number)

        [Fact]
        public async Task GetCabinetByNumber_ReturnsCorrectCabinet()
        {
            using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
            var cabinet = SimpleRepositoryData.Cabinets[0];

            var expected = SimpleRepositoryData.GetCabinetDetail(cabinet.Id);
            var result = await repository.GetCabinetDetailsAsync(cabinet.CabinetNumber);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetCabinetByNumber_Nonexistent_ReturnsNull()
        {
            using var repository = new SimpleRepositoryHelper().GetCabinetRepository();
            var result = await repository.GetCabinetDetailsAsync(0);
            result.ShouldBeNull();
        }

        // CreateCabinetAsync

        [Fact]
        public async Task CreateCabinet_Succeeds()
        {
            var repositoryHelper = new SimpleRepositoryHelper();
            int newCabinetNumber;
            var expectedCabinetNumber = SimpleRepositoryData.Cabinets.Max(e => e.CabinetNumber) + 1;
            var expectedName = string.Concat("C", expectedCabinetNumber.ToString().PadLeft(3, '0'));

            using (var repository = repositoryHelper.GetCabinetRepository())
            {
                newCabinetNumber = await repository.CreateCabinetAsync();
            }

            using (var repository = repositoryHelper.GetCabinetRepository())
            {
                var cabinet = await repository.GetCabinetDetailsAsync(newCabinetNumber);

                newCabinetNumber.ShouldEqual(expectedCabinetNumber);
                cabinet.CabinetNumber.Should().Be(newCabinetNumber);
                cabinet.Name.Should().Be(expectedName);
                cabinet.Active.ShouldBeTrue();
            }
        }

        // UpdateCabinetAsync

        [Fact]
        public async Task UpdateCabinet_Succeeds()
        {
            var repositoryHelper = new SimpleRepositoryHelper();
            var cabinet = SimpleRepositoryData.Cabinets[0];

            var cabinetEdit = new CabinetEditDto() {FirstFileLabel = "222-2222"};

            using (var repository = repositoryHelper.GetCabinetRepository())
            {
                await repository.UpdateCabinetAsync(cabinet.Id, cabinetEdit);
            }

            using (var repository = repositoryHelper.GetCabinetRepository())
            {
                var updatedCabinet = await repository.GetCabinetSummaryAsync(cabinet.Id);
                updatedCabinet.Name.Should().Be(cabinet.Name);
                updatedCabinet.FirstFileLabel.Should().Be(cabinetEdit.FirstFileLabel);
                updatedCabinet.Active.Should().Be(!cabinet.Active);
            }
        }

        [Fact]
        public async Task UpdateCabinet_WithNoChanges_Succeeds()
        {
            var repositoryHelper = new SimpleRepositoryHelper();
            var cabinet = SimpleRepositoryData.Cabinets[0];

            var cabinetEdit = new CabinetEditDto() {FirstFileLabel = cabinet.FirstFileLabel};

            using (var repository = repositoryHelper.GetCabinetRepository())
            {
                await repository.UpdateCabinetAsync(cabinet.Id, cabinetEdit);
            }

            using (var repository = repositoryHelper.GetCabinetRepository())
            {
                var updatedCabinet = await repository.GetCabinetDetailsAsync(cabinet.Id);
                updatedCabinet.Name.Should().Be(cabinet.Name);
                updatedCabinet.FirstFileLabel.Should().Be(cabinetEdit.FirstFileLabel);
                updatedCabinet.Active.Should().Be(cabinet.Active);
            }
        }

        [Fact]
        public async Task UpdateCabinet_WithEmptyFileLabel_ThrowsException()
        {
            var cabinet = DataHelpers.Cabinets[0];
            var cabinetEdit = new CabinetEditDto() {FirstFileLabel = ""};

            Func<Task> action = async () =>
            {
                using var repository = new RepositoryHelper().GetCabinetRepository();
                await repository.UpdateCabinetAsync(cabinet.Id, cabinetEdit);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("File Label can not be null or empty.");
        }

        [Fact]
        public async Task UpdateCabinet_WithInvalidFileLabel_ThrowsException()
        {
            var cabinet = DataHelpers.Cabinets[0];
            var cabinetEdit = new CabinetEditDto() {FirstFileLabel = "00-00"};

            Func<Task> action = async () =>
            {
                using var repository = new RepositoryHelper().GetCabinetRepository();
                await repository.UpdateCabinetAsync(cabinet.Id, cabinetEdit);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("File Label is invalid.");
        }

        [Fact]
        public async Task UpdateNonexistentCabinet_ThrowsException()
        {
            var cabinetEdit = new CabinetEditDto() {FirstFileLabel = "000-0000"};

            Func<Task> action = async () =>
            {
                using var repository = new RepositoryHelper().GetCabinetRepository();
                await repository.UpdateCabinetAsync(default, cabinetEdit);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("Cabinet ID not found.");
        }

        // GetNextCabinetName

        [Fact]
        public async Task GetNextCabinetName_Succeeds()
        {
            var repository = new SimpleRepositoryHelper().GetCabinetRepository();
            var nextCabinetNumber = SimpleRepositoryData.Cabinets.Max(e => e.CabinetNumber) + 1;
            var expected = string.Concat("C", nextCabinetNumber.ToString().PadLeft(3, '0'));

            var result = await repository.GetNextCabinetName();

            result.Should().Be(expected);
        }
    }
}