using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Dto;
using TestHelpers;
using Xunit;
using Xunit.Extensions.AssertExtensions;
using static TestHelpers.RepositoryHelper;

namespace FMS.Infrastructure.Tests
{
    public class FileRepositoryTests
    {
        // FileExistsAsync

        [Fact]
        public async Task FileExists_Exists_ReturnsTrue()
        {
            using var repository = CreateRepositoryHelper().GetFileRepository();
            var result = await repository.FileExistsAsync(RepositoryData.Files[0].Id);
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task FileExists_NotExists_ReturnsFalse()
        {
            using var repository = CreateRepositoryHelper().GetFileRepository();
            var result = await repository.FileExistsAsync(Guid.Empty);
            result.ShouldBeFalse();
        }

        // GetFileAsync

        [Fact]
        public async Task GetFileById_ReturnsCorrectFile()
        {
            using var repository = CreateRepositoryHelper().GetFileRepository();
            var file = RepositoryData.Files[0];

            var result = await repository.GetFileAsync(file.Id);

            result.Id.Should().Be(file.Id);
            result.FileLabel.Should().Be(file.FileLabel);
        }

        [Fact]
        public async Task GetFileById_Nonexistent_ReturnsNull()
        {
            using var repository = CreateRepositoryHelper().GetFileRepository();
            var result = await repository.GetFileAsync(Guid.Empty);
            result.ShouldBeNull();
        }

        [Fact]
        public async Task GetFileByName_ReturnsCorrectFile()
        {
            using var repository = CreateRepositoryHelper().GetFileRepository();
            var file = RepositoryData.Files[0];
            var result = await repository.GetFileAsync(file.FileLabel);
            result.Id.Should().Be(file.Id);
        }

        [Fact]
        public async Task GetFileByName_Nonexistent_ReturnsNull()
        {
            using var repository = CreateRepositoryHelper().GetFileRepository();
            var result = await repository.GetFileAsync(string.Empty);
            result.ShouldBeNull();
        }

        // FileHasActiveFacilities

        [Fact]
        public async Task FileHasActiveFacilities_HasActive_ReturnsTrue()
        {
            using var repository = CreateRepositoryHelper().GetFileRepository();
            var result = await repository.FileHasActiveFacilities(RepositoryData.Files[0].Id);
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task FileHasActiveFacilities_HasNoActive_ReturnsFalse()
        {
            using var repository = CreateRepositoryHelper().GetFileRepository();
            var result = await repository.FileHasActiveFacilities(RepositoryData.Files[1].Id);
            result.ShouldBeFalse();
        }

        [Fact]
        public async Task FileHasActiveFacilities_NonExistentId_ReturnsFalse()
        {
            using var repository = CreateRepositoryHelper().GetFileRepository();
            var result = await repository.FileHasActiveFacilities(Guid.Empty);
            result.ShouldBeFalse();
        }

        // CountAsync

        [Fact]
        public async Task FileCount_DefaultSpec_ReturnsCountOfActiveFiles()
        {
            using var repository = CreateRepositoryHelper().GetFileRepository();
            var spec = new FileSpec();

            var result = await repository.CountAsync(spec);

            var expected = RepositoryData.Files.Count(e => e.Active);
            result.Should().Be(expected);
        }

        [Fact]
        public async Task FileCount_WithInactive_ReturnsCountOfAll()
        {
            using var repository = CreateRepositoryHelper().GetFileRepository();
            var spec = new FileSpec() {ShowInactive = true};

            var result = await repository.CountAsync(spec);

            var expected = RepositoryData.Files.Count;
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(99)]
        [InlineData(102)]
        public async Task FileCount_ByCounty_ReturnsCorrectCount(int countyId)
        {
            using var repository = CreateRepositoryHelper().GetFileRepository();
            var spec = new FileSpec() {CountyId = countyId};
            var countyString = countyId.ToString().PadLeft(3, '0');

            var result = await repository.CountAsync(spec);

            var expected = RepositoryData.Files.Count(e => e.FileLabel.StartsWith(countyString) && e.Active);
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("00")]
        [InlineData("0001")]
        [InlineData("2-0")]
        public async Task FileCount_ByFileLabel_ReturnsCorrectCount(string fileLabel)
        {
            using var repository = CreateRepositoryHelper().GetFileRepository();
            var spec = new FileSpec() {FileLabel = fileLabel};

            var result = await repository.CountAsync(spec);

            var expected = RepositoryData.Files.Count(e => e.FileLabel.Contains(fileLabel) && e.Active);
            result.Should().Be(expected);
        }

        // GetFileListAsync

        [Fact]
        public async Task FileSearch_Default_ReturnsActiveFiles()
        {
            using var repository = CreateRepositoryHelper().GetFileRepository();

            var result = await repository.GetFileListAsync(new FileSpec(), 1, 999);
            var expectedCount = RepositoryData.Files.Count(e => e.Active);

            result.TotalCount.Should().Be(expectedCount);
            result.Items.Count.Should().Be(expectedCount);
            result.PageNumber.ShouldEqual(1);
            result.TotalPages.ShouldEqual(1);
        }

        [Fact]
        public async Task FileSearch_WithInactive_ReturnsAllFiles()
        {
            using var repository = CreateRepositoryHelper().GetFileRepository();
            var spec = new FileSpec() {ShowInactive = true};

            var result = await repository.GetFileListAsync(spec, 1, 999);
            var expectedCount = RepositoryData.Files.Count;

            result.TotalCount.Should().Be(expectedCount);
            result.Items.Count.Should().Be(expectedCount);
        }

        [Theory]
        [InlineData(99)]
        [InlineData(102)]
        public async Task FileSearch_ByCounty_ReturnsCorrectList(int countyId)
        {
            using var repository = CreateRepositoryHelper().GetFileRepository();
            var spec = new FileSpec() {CountyId = countyId};
            var countyString = countyId.ToString().PadLeft(3, '0');

            var result = await repository.GetFileListAsync(spec, 1, 999);
            var expectedCount = RepositoryData.Files.Count(e => e.FileLabel.StartsWith(countyString) && e.Active);

            result.TotalCount.Should().Be(expectedCount);
            result.Items.Count.Should().Be(expectedCount);
        }

        [Theory]
        [InlineData("00")]
        [InlineData("0001")]
        [InlineData("2-0")]
        public async Task FileSearch_ByFileNumber_ReturnsCorrectList(string fileLabelSpec)
        {
            using var repository = CreateRepositoryHelper().GetFileRepository();
            var spec = new FileSpec() {FileLabel = fileLabelSpec};

            var result = await repository.GetFileListAsync(spec, 1, 999);
            var expectedCount = RepositoryData.Files.Count(e => e.FileLabel.Contains(fileLabelSpec) && e.Active);

            result.TotalCount.Should().Be(expectedCount);
            result.Items.Count.Should().Be(expectedCount);
        }

        // UpdateFileAsync

        [Fact]
        public async Task UpdateFile_Succeeds()
        {
            var file = RepositoryData.Files[0];

            using var repositoryHelper = CreateRepositoryHelper();
            using var repository = repositoryHelper.GetFileRepository();
            await repository.UpdateFileAsync(file.Id, !file.Active);
            repositoryHelper.ClearChangeTracker();

            var updatedFile = await repository.GetFileAsync(file.FileLabel);

            updatedFile.Active.Should().Be(!file.Active);
            updatedFile.FileLabel.Should().Be(file.FileLabel);
        }

        [Fact]
        public async Task UpdateNonexistentFile_ThrowsException()
        {
            Func<Task> action = async () =>
            {
                using var repository = CreateRepositoryHelper().GetFileRepository();
                await repository.UpdateFileAsync(Guid.Empty, default);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("File ID not found.");
        }
    }
}