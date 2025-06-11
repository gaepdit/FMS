using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Dto;
using TestHelpers;
using NUnit.Framework;
using static TestHelpers.RepositoryHelper;
using FMS.TestData.SeedData;

namespace FMS.Infrastructure.Tests
{
    public class FileRepositoryTests
    {
        // FileExistsAsync

        [Test]
        public async Task FileExists_Exists_ReturnsTrue()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFileRepository();
            var result = await repository.FileExistsAsync(SeedData.GetFiles()[0].Id);
            result.Should().BeTrue();
        }

        [Test]
        public async Task FileExists_NotExists_ReturnsFalse()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFileRepository();
            var result = await repository.FileExistsAsync(Guid.Empty);
            result.Should().BeFalse();
        }

        // GetFileAsync

        [Test]
        public async Task GetFileById_ReturnsCorrectFile()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFileRepository();
            var file = SeedData.GetFiles()[0];

            var result = await repository.GetFileAsync(file.Id);

            result.Id.Should().Be(file.Id);
            result.FileLabel.Should().Be(file.FileLabel);
        }

        [Test]
        public async Task GetFileById_Nonexistent_ReturnsNull()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFileRepository();
            var result = await repository.GetFileAsync(Guid.Empty);
            result.Should().BeNull();
        }

        [Test]
        public async Task GetFileByName_ReturnsCorrectFile()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFileRepository();
            var file = SeedData.GetFiles()[0];
            var result = await repository.GetFileAsync(file.FileLabel);
            result.Id.Should().Be(file.Id);
        }

        [Test]
        public async Task GetFileByName_Nonexistent_ReturnsNull()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFileRepository();
            var result = await repository.GetFileAsync(string.Empty);
            result.Should().BeNull();
        }

        // FileHasActiveFacilities

        [Test]
        public async Task FileHasActiveFacilities_HasActive_ReturnsTrue()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFileRepository();
            var result = await repository.FileHasActiveFacilities(SeedData.GetFiles()[8].Id);
            result.Should().BeTrue();
        }

        [Test]
        public async Task FileHasActiveFacilities_HasNoActive_ReturnsFalse()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFileRepository();
            var result = await repository.FileHasActiveFacilities(SeedData.GetFiles()[1].Id);
            result.Should().BeFalse();
        }

        [Test]
        public async Task FileHasActiveFacilities_NonExistentId_ReturnsFalse()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFileRepository();
            var result = await repository.FileHasActiveFacilities(Guid.Empty);
            result.Should().BeFalse();
        }

        // CountAsync

        [Test]
        public async Task FileCount_DefaultSpec_ReturnsCountOfActiveFiles()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFileRepository();
            var spec = new FileSpec();

            var result = await repository.CountAsync(spec);

            var expected = SeedData.GetFiles().Count(e => e.Active);
            result.Should().Be(expected);
        }

        [Test]
        public async Task FileCount_WithInactive_ReturnsCountOfAll()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFileRepository();
            var spec = new FileSpec { ShowInactive = true };

            var result = await repository.CountAsync(spec);

            var expected = SeedData.GetFiles().Count;
            result.Should().Be(expected);
        }

        [TestCase(99)]
        [TestCase(102)]
        public async Task FileCount_ByCounty_ReturnsCorrectCount(int countyId)
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFileRepository();
            var spec = new FileSpec { CountyId = countyId };
            var countyString = countyId.ToString().PadLeft(3, '0');

            var result = await repository.CountAsync(spec);

            var expected = SeedData.GetFiles().Count(e => e.FileLabel.StartsWith(countyString) && e.Active);
            result.Should().Be(expected);
        }

        [TestCase("00")]
        [TestCase("0001")]
        [TestCase("2-0")]
        public async Task FileCount_ByFileLabel_ReturnsCorrectCount(string fileLabel)
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFileRepository();
            var spec = new FileSpec { FileLabel = fileLabel };

            var result = await repository.CountAsync(spec);

            var expected = SeedData.GetFiles().Count(e => e.FileLabel.Contains(fileLabel) && e.Active);
            result.Should().Be(expected);
        }

        // GetFileListAsync

        [Test]
        public async Task FileSearch_Default_ReturnsActiveFiles()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFileRepository();

            var result = await repository.GetFileListAsync(new FileSpec(), 1, 999);
            var expectedCount = SeedData.GetFiles().Count(e => e.Active);

            result.TotalCount.Should().Be(expectedCount);
            result.Items.Count.Should().Be(expectedCount);
            result.PageNumber.Should().Be(1);
            result.TotalPages.Should().Be(1);
        }

        [Test]
        public async Task FileSearch_WithInactive_ReturnsAllFiles()
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFileRepository();
            var spec = new FileSpec { ShowInactive = true };

            var result = await repository.GetFileListAsync(spec, 1, 999);
            var expectedCount = SeedData.GetFiles().Count;

            result.TotalCount.Should().Be(expectedCount);
            result.Items.Count.Should().Be(expectedCount);
        }

        [TestCase(99)]
        [TestCase(102)]
        public async Task FileSearch_ByCounty_ReturnsCorrectList(int countyId)
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFileRepository();
            var spec = new FileSpec { CountyId = countyId };
            var countyString = countyId.ToString().PadLeft(3, '0');

            var result = await repository.GetFileListAsync(spec, 1, 999);
            var expectedCount = SeedData.GetFiles().Count(e => e.FileLabel.StartsWith(countyString) && e.Active);

            result.TotalCount.Should().Be(expectedCount);
            result.Items.Count.Should().Be(expectedCount);
        }

        [TestCase("00")]
        [TestCase("0001")]
        [TestCase("2-0")]
        public async Task FileSearch_ByFileNumber_ReturnsCorrectList(string fileLabelSpec)
        {
            using var repository = (await CreateRepositoryHelperAsync()).GetFileRepository();
            var spec = new FileSpec { FileLabel = fileLabelSpec };

            var result = await repository.GetFileListAsync(spec, 1, 999);
            var expectedCount = SeedData.GetFiles().Count(e => e.FileLabel.Contains(fileLabelSpec) && e.Active);

            result.TotalCount.Should().Be(expectedCount);
            result.Items.Count.Should().Be(expectedCount);
        }

        // UpdateFileAsync

        [Test]
        public async Task UpdateFile_Succeeds()
        {
            var file = SeedData.GetFiles()[0];

            using var repositoryHelper = (await CreateRepositoryHelperAsync());
            using var repository = repositoryHelper.GetFileRepository();
            await repository.UpdateFileAsync(file.Id, !file.Active);
            repositoryHelper.ClearChangeTracker();

            var updatedFile = await repository.GetFileAsync(file.FileLabel);

            updatedFile.Active.Should().Be(!file.Active);
            updatedFile.FileLabel.Should().Be(file.FileLabel);
        }

        [Test]
        public async Task UpdateNonexistentFile_ThrowsException()
        {
            Func<Task> action = async () =>
            {
                using var repository = (await CreateRepositoryHelperAsync()).GetFileRepository();
                await repository.UpdateFileAsync(Guid.Empty, default);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("File ID not found.");
        }
    }
}