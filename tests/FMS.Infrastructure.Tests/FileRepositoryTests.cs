using System;
using System.Collections.Generic;
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
    public class FileRepositoryTests
    {
        // FileExistsAsync

        [Fact]
        public async Task FileExists_Exists_ReturnsTrue()
        {
            using var repository = new SimpleRepositoryHelper().GetFileRepository();
            var result = await repository.FileExistsAsync(SimpleRepositoryData.Files[0].Id);
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task FileExists_NotExists_ReturnsFalse()
        {
            using var repository = new SimpleRepositoryHelper().GetFileRepository();
            var result = await repository.FileExistsAsync(Guid.Empty);
            result.ShouldBeFalse();
        }

        // GetFileAsync

        [Fact]
        public async Task GetFileById_ReturnsCorrectFile()
        {
            using var repository = new SimpleRepositoryHelper().GetFileRepository();
            var file = SimpleRepositoryData.Files[0];

            var result = await repository.GetFileAsync(file.Id);

            result.Id.Should().Be(file.Id);
            result.FileLabel.Should().Be(file.FileLabel);
        }

        [Fact]
        public async Task GetFileById_Nonexistent_ReturnsNull()
        {
            using var repository = new SimpleRepositoryHelper().GetFileRepository();
            var result = await repository.GetFileAsync(Guid.Empty);
            result.ShouldBeNull();
        }

        [Fact]
        public async Task GetFileByName_ReturnsCorrectFile()
        {
            using var repository = new SimpleRepositoryHelper().GetFileRepository();
            var file = SimpleRepositoryData.Files[0];
            var result = await repository.GetFileAsync(file.FileLabel);
            result.Id.Should().Be(file.Id);
        }

        [Fact]
        public async Task GetFileByName_Nonexistent_ReturnsNull()
        {
            using var repository = new SimpleRepositoryHelper().GetFileRepository();
            var result = await repository.GetFileAsync(string.Empty);
            result.ShouldBeNull();
        }

        // FileHasActiveFacilities

        [Fact]
        public async Task FileHasActiveFacilities_HasActive_ReturnsTrue()
        {
            using var repository = new SimpleRepositoryHelper().GetFileRepository();
            var result = await repository.FileHasActiveFacilities(SimpleRepositoryData.Files[0].Id);
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task FileHasActiveFacilities_HasNoActive_ReturnsFalse()
        {
            using var repository = new SimpleRepositoryHelper().GetFileRepository();
            var result = await repository.FileHasActiveFacilities(SimpleRepositoryData.Files[1].Id);
            result.ShouldBeFalse();
        }

        [Fact]
        public async Task FileHasActiveFacilities_NonExistentId_ReturnsFalse()
        {
            using var repository = new SimpleRepositoryHelper().GetFileRepository();
            var result = await repository.FileHasActiveFacilities(Guid.Empty);
            result.ShouldBeFalse();
        }

        // CountAsync

        [Fact]
        public async Task FileCount_DefaultSpec_ReturnsCountOfActiveFiles()
        {
            using var repository = new SimpleRepositoryHelper().GetFileRepository();
            var spec = new FileSpec();

            var result = await repository.CountAsync(spec);

            var expected = SimpleRepositoryData.Files.Count(e => e.Active);
            result.Should().Be(expected);
        }

        [Fact]
        public async Task FileCount_WithInactive_ReturnsCountOfAll()
        {
            using var repository = new SimpleRepositoryHelper().GetFileRepository();
            var spec = new FileSpec() {ShowInactive = true};

            var result = await repository.CountAsync(spec);

            var expected = SimpleRepositoryData.Files.Count;
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(99)]
        [InlineData(102)]
        public async Task FileCount_ByCounty_ReturnsCorrectCount(int countyId)
        {
            using var repository = new SimpleRepositoryHelper().GetFileRepository();
            var spec = new FileSpec() {CountyId = countyId};
            var countyString = countyId.ToString().PadLeft(3, '0');

            var result = await repository.CountAsync(spec);

            var expected = SimpleRepositoryData.Files.Count(e => e.FileLabel.StartsWith(countyString) && e.Active);
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("00")]
        [InlineData("0001")]
        [InlineData("2-0")]
        public async Task FileCount_ByFileLabel_ReturnsCorrectCount(string fileLabel)
        {
            using var repository = new SimpleRepositoryHelper().GetFileRepository();
            var spec = new FileSpec() {FileLabel = fileLabel};

            var result = await repository.CountAsync(spec);

            var expected = SimpleRepositoryData.Files.Count(e => e.FileLabel.Contains(fileLabel) && e.Active);
            result.Should().Be(expected);
        }

        // GetFileListAsync

        [Fact]
        public async Task FileSearch_Default_ReturnsActiveFiles()
        {
            using var repository = new SimpleRepositoryHelper().GetFileRepository();

            var result = await repository.GetFileListAsync(new FileSpec(), 1, 999);
            var expectedCount = SimpleRepositoryData.Files.Count(e => e.Active);

            result.TotalCount.Should().Be(expectedCount);
            result.Items.Count.Should().Be(expectedCount);
            result.PageNumber.ShouldEqual(1);
            result.TotalPages.ShouldEqual(1);
        }

        [Fact]
        public async Task FileSearch_WithInactive_ReturnsAllFiles()
        {
            using var repository = new SimpleRepositoryHelper().GetFileRepository();
            var spec = new FileSpec() {ShowInactive = true};

            var result = await repository.GetFileListAsync(spec, 1, 999);
            var expectedCount = SimpleRepositoryData.Files.Count;

            result.TotalCount.Should().Be(expectedCount);
            result.Items.Count.Should().Be(expectedCount);
        }

        [Theory]
        [InlineData(99)]
        [InlineData(102)]
        public async Task FileSearch_ByCounty_ReturnsCorrectList(int countyId)
        {
            using var repository = new SimpleRepositoryHelper().GetFileRepository();
            var spec = new FileSpec() {CountyId = countyId};
            var countyString = countyId.ToString().PadLeft(3, '0');

            var result = await repository.GetFileListAsync(spec, 1, 999);
            var expectedCount = SimpleRepositoryData.Files.Count(e => e.FileLabel.StartsWith(countyString) && e.Active);

            result.TotalCount.Should().Be(expectedCount);
            result.Items.Count.Should().Be(expectedCount);
        }

        [Theory]
        [InlineData("00")]
        [InlineData("0001")]
        [InlineData("2-0")]
        public async Task FileSearch_ByFileNumber_ReturnsCorrectList(string fileLabelSpec)
        {
            using var repository = new SimpleRepositoryHelper().GetFileRepository();
            var spec = new FileSpec() {FileLabel = fileLabelSpec};

            var result = await repository.GetFileListAsync(spec, 1, 999);
            var expectedCount = SimpleRepositoryData.Files.Count(e => e.FileLabel.Contains(fileLabelSpec) && e.Active);

            result.TotalCount.Should().Be(expectedCount);
            result.Items.Count.Should().Be(expectedCount);
        }

        // GetNextSequenceForCounty

        [Fact]
        public async Task GetNextSequenceForCounty_Succeeds()
        {
            const int countyNum = 111;
            using var repository = new SimpleRepositoryHelper().GetFileRepository();
            var result = await repository.GetNextSequenceForCountyAsync(countyNum);
            result.Should().Be(2);
        }


        [Fact]
        public async Task GetNextSequenceForCounty_TwoDigit_Succeeds()
        {
            const int countyNum = 99;
            using var repository = new SimpleRepositoryHelper().GetFileRepository();
            var result = await repository.GetNextSequenceForCountyAsync(countyNum);
            result.Should().Be(2);
        }

        [Fact]
        public async Task GetNextSequenceForCounty_NoCurrentLabel_ReturnsOne()
        {
            const int countyNum = 101;
            using var repository = new SimpleRepositoryHelper().GetFileRepository();
            var result = await repository.GetNextSequenceForCountyAsync(countyNum);
            result.Should().Be(1);
        }

        [Fact]
        public async Task GetNextSequenceForCounty_CurrentLabelSkipsNumber_Succeeds()
        {
            const int countyNum = 102;
            using var repository = new SimpleRepositoryHelper().GetFileRepository();
            var result = await repository.GetNextSequenceForCountyAsync(countyNum);
            result.Should().Be(4);
        }

        [Fact]
        public async Task GetNextSequenceForCounty_LatestLabelInactive_Succeeds()
        {
            const int countyNum = 103;
            using var repository = new SimpleRepositoryHelper().GetFileRepository();
            var result = await repository.GetNextSequenceForCountyAsync(countyNum);
            result.Should().Be(3);
        }

        [Fact]
        public async Task GetNextSequenceForCounty_NoSuchCounty_ThrowsException()
        {
            const int countyNum = 999;

            Func<Task> action = async () =>
            {
                using var repository = new SimpleRepositoryHelper().GetFileRepository();
                await repository.GetNextSequenceForCountyAsync(countyNum);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage($"County ID {countyNum} does not exist. (Parameter 'countyNum')");
        }

        // UpdateFileAsync

        [Fact]
        public async Task UpdateFile_Succeeds()
        {
            var repositoryHelper = new SimpleRepositoryHelper();
            var file = SimpleRepositoryData.Files[0];

            using (var repository = repositoryHelper.GetFileRepository())
            {
                await repository.UpdateFileAsync(file.Id, !file.Active);
            }

            using (var repository = repositoryHelper.GetFileRepository())
            {
                var updatedFile = await repository.GetFileAsync(file.FileLabel);
                updatedFile.Active.Should().Be(!file.Active);
                updatedFile.FileLabel.Should().Be(file.FileLabel);
            }
        }

        [Fact]
        public async Task UpdateNonexistentFile_ThrowsException()
        {
            Func<Task> action = async () =>
            {
                using var repository = new SimpleRepositoryHelper().GetFileRepository();
                await repository.UpdateFileAsync(Guid.Empty, default);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("File ID not found.");
        }
    }
}