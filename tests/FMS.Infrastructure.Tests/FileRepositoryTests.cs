using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHelpers;
using TestSupport.EfHelpers;
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
            using var repository = new RepositoryHelper().GetFileRepository();
            var result = await repository.FileExistsAsync(DataHelpers.Files[0].Id);
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task FileExists_NotExists_ReturnsFalse()
        {
            using var repository = new RepositoryHelper().GetFileRepository();
            var result = await repository.FileExistsAsync(default);
            result.ShouldBeFalse();
        }

        // GetFileAsync

        [Fact]
        public async Task GetFile_ById_ReturnsCorrectFile()
        {
            using var repository = new RepositoryHelper().GetFileRepository();
            var file = DataHelpers.Files[0];

            var result = await repository.GetFileAsync(file.Id);

            var expected = new FileDetailDto(file);
            expected.Facilities = DataHelpers.Facilities
                .Where(e => e.Active)
                .Where(e => e.FileId == expected.Id)
                .Select(e => new FacilitySummaryDto(e)).ToList();

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetNonexistentFile_ById_ReturnsNull()
        {
            using var repository = new RepositoryHelper().GetFileRepository();
            var result = await repository.GetFileAsync((Guid)default);
            result.ShouldBeNull();
        }

        [Fact]
        public async Task GetFile_ByName_ReturnsCorrectFile()
        {
            using var repository = new RepositoryHelper().GetFileRepository();
            var file = DataHelpers.Files[0];

            var result = await repository.GetFileAsync(file.FileLabel);

            var expected = new FileDetailDto(file);
            expected.Facilities = DataHelpers.Facilities
                .Where(e => e.Active)
                .Where(e => e.FileId == expected.Id)
                .Select(e => new FacilitySummaryDto(e)).ToList();
            
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetNonexistentFile_ByName_ReturnsNull()
        {
            using var repository = new RepositoryHelper().GetFileRepository();
            var result = await repository.GetFileAsync(string.Empty);
            result.ShouldBeNull();
        }

        // GetFacilitiesForFileAsync

        [Fact]
        public async Task GetFacilies_ReturnsCorrectList()
        {
            using var repository = new RepositoryHelper().GetFileRepository();
            Guid fileId = DataHelpers.Files[1].Id;

            var result = await repository.GetFacilitiesForFileAsync(fileId);

            var expected = DataHelpers.Facilities
                .Where(e => e.FileId == fileId)
                .Select(e => DataHelpers.GetFacilitySummary(e.Id))
                .ToList();
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetFacilies_UnusedFile_ReturnsEmptyList()
        {
            using var repository = SimpleFileRepository();
            var fileId = (await repository.GetFileAsync("099-0001")).Id;

            var result = await repository.GetFacilitiesForFileAsync(fileId);

            var expected = new List<FacilitySummaryDto>();
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetFacilies_NonexistentFile_ReturnsEmptyList()
        {
            using var repository = new RepositoryHelper().GetFileRepository();

            var result = await repository.GetFacilitiesForFileAsync(default);

            var expected = new List<FacilitySummaryDto>();
            result.Should().BeEquivalentTo(expected);
        }

        // FileHasActiveFacilities

        [Fact]
        public async Task FileHasActiveFacilities_HasActive_ReturnsTrue()
        {
            using var repository = new RepositoryHelper().GetFileRepository();
            var id = new Guid("5019EBBC-8F99-469A-BCDC-256823EDD9A2");

            var result = await repository.FileHasActiveFacilities(id);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task FileHasActiveFacilities_HasNoActive_ReturnsFalse()
        {
            using var repository = new RepositoryHelper().GetFileRepository();
            var id = new Guid("790B04E8-F5F5-412E-95E2-B785E630A2A7");

            var result = await repository.FileHasActiveFacilities(id);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task FileHasActiveFacilities_NonExistentId_ReturnsFalse()
        {
            using var repository = new RepositoryHelper().GetFileRepository();
            var id = Guid.NewGuid();

            var result = await repository.FileHasActiveFacilities(id);

            result.Should().BeFalse();
        }

        // CountAsync

        [Fact]
        public async Task FileCount_DefaultSpec_ReturnsCountOfActiveFiles()
        {
            using var repository = new RepositoryHelper().GetFileRepository();
            var spec = new FileSpec();

            var result = await repository.CountAsync(spec);

            var expected = DataHelpers.Files.Count(e => e.Active);
            result.Should().Be(expected);
        }

        [Fact]
        public async Task FileCount_WithInactive_ReturnsCountOfAll()
        {
            using var repository = new RepositoryHelper().GetFileRepository();
            var spec = new FileSpec() { ShowInactive = true };

            var result = await repository.CountAsync(spec);

            var expected = DataHelpers.Files.Count;
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(243)]
        [InlineData(180)]
        public async Task FileCount_ByCounty_ReturnsCorrectCount(int countyId)
        {
            using var repository = new RepositoryHelper().GetFileRepository();
            var spec = new FileSpec() { CountyId = countyId };
            var countyString = countyId.ToString().PadLeft(3, '0');

            var result = await repository.CountAsync(spec);

            var expected = DataHelpers.Files.Count(e => e.FileLabel.StartsWith(countyString) && e.Active);
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("00")]
        [InlineData("0001")]
        [InlineData("0-0")]
        public async Task FileCount_ByFileLabel_ReturnsCorrectCount(string fileLabel)
        {
            using var repository = new RepositoryHelper().GetFileRepository();
            var spec = new FileSpec() { FileLabel = fileLabel };

            var result = await repository.CountAsync(spec);

            var expected = DataHelpers.Files.Count(e => e.FileLabel.Contains(fileLabel) && e.Active);
            result.Should().Be(expected);
        }

        // GetFileListAsync

        [Fact]
        public async Task FileSearch_Default_ReturnsActiveFiles()
        {
            using var repository = new RepositoryHelper().GetFileRepository();

            var result = await repository.GetFileListAsync(new FileSpec());
            var expected = DataHelpers.Files
                .Where(e => e.Active)
                .Select(e => new FileDetailDto(e)).ToList();

            foreach (var file in expected)
            {
                file.Facilities = DataHelpers.Facilities
                    .Where(e => e.Active)
                    .Where(e => e.FileId == file.Id)
                    .Select(e => DataHelpers.GetFacilitySummary(e.Id))
                    .ToList();

                file.Cabinets = DataHelpers.GetCabinetsForFile(file.Id);
            }

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task FileSearch_WithInactive_ReturnsAllFiles()
        {
            using var repository = new RepositoryHelper().GetFileRepository();
            var spec = new FileSpec() { ShowInactive = true };

            var result = await repository.GetFileListAsync(spec);
            var expected = DataHelpers.Files
                .Select(e => new FileDetailDto(e)).ToList();
            foreach (var file in expected)
            {
                file.Facilities = DataHelpers.Facilities
                    .Where(e => e.Active)
                    .Where(e => e.FileId == file.Id)
                    .Select(e => DataHelpers.GetFacilitySummary(e.Id))
                    .ToList();
                file.Cabinets = DataHelpers.GetCabinetsForFile(file.Id);
            }

            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(243)]
        [InlineData(180)]
        public async Task FileSearch_ByCounty_ReturnsCorrectList(int countyId)
        {
            using var repository = new RepositoryHelper().GetFileRepository();
            var spec = new FileSpec() { CountyId = countyId };
            var countyString = countyId.ToString().PadLeft(3, '0');

            var result = await repository.GetFileListAsync(spec);
            var expected = DataHelpers.Files
                .Where(e => e.FileLabel.StartsWith(countyString) && e.Active)
                .Select(e => new FileDetailDto(e)).ToList();
            foreach (var file in expected)
            {
                file.Facilities = DataHelpers.Facilities
                    .Where(e => e.Active)
                    .Where(e => e.FileId == file.Id)
                    .Select(e => DataHelpers.GetFacilitySummary(e.Id))
                    .ToList();
                file.Cabinets = DataHelpers.GetCabinetsForFile(file.Id);
            }

            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("00")]
        [InlineData("0001")]
        [InlineData("0-0")]
        public async Task FileSearch_ByFileNumber_ReturnsCorrectList(string fileLabel)
        {
            using var repository = new RepositoryHelper().GetFileRepository();
            var spec = new FileSpec() { FileLabel = fileLabel };

            var result = await repository.GetFileListAsync(spec);
            var expected = DataHelpers.Files
                .Where(e => e.FileLabel.Contains(fileLabel) && e.Active)
                .Select(e => DataHelpers.GetFileDetail(e.Id)).ToList();

            result.Should().BeEquivalentTo(expected);
        }

        // GetNextSequenceForCounty

        /// <summary>
        /// This Simplified File repository is used by 
        /// the GetNextSequenceForCounty unit tests.
        /// </summary>
        /// <returns>A FileRepository with a simplified list of Files.</returns>
        private FileRepository SimpleFileRepository()
        {
            var simpleFileList = new List<File>
            {
                new File { Id = Guid.NewGuid(), FileLabel = "099-0001" },
                new File { Id = Guid.NewGuid(), FileLabel = "111-0001" },
                new File { Id = Guid.NewGuid(), FileLabel = "102-0001" },
                new File { Id = Guid.NewGuid(), FileLabel = "102-0003" },
                new File { Id = Guid.NewGuid(), FileLabel = "103-0001" },
                new File { Id = Guid.NewGuid(), FileLabel = "103-0002", Active = false }
            };
            var context = new FmsDbContext(SqliteInMemory.CreateOptions<FmsDbContext>());
            context.Database.EnsureCreated();
            context.Files.AddRange(simpleFileList);
            context.SaveChanges();
            return new FileRepository(context);
        }

        [Fact]
        public async Task GetNextSequenceForCounty_Succeeds()
        {
            int countyNum = 111;
            using var repository = SimpleFileRepository();
            var result = await repository.GetNextSequenceForCountyAsync(countyNum);
            result.Should().Be(2);
        }


        [Fact]
        public async Task GetNextSequenceForCounty_TwoDigit_Succeeds()
        {
            int countyNum = 99;
            using var repository = SimpleFileRepository();
            var result = await repository.GetNextSequenceForCountyAsync(countyNum);
            result.Should().Be(2);
        }

        [Fact]
        public async Task GetNextSequenceForCounty_NoCurrentLabel_ReturnsOne()
        {
            int countyNum = 101;
            using var repository = SimpleFileRepository();
            var result = await repository.GetNextSequenceForCountyAsync(countyNum);
            result.Should().Be(1);
        }

        [Fact]
        public async Task GetNextSequenceForCounty_CurrentLabelSkipsNumber_Succeeds()
        {
            int countyNum = 102;
            using var repository = SimpleFileRepository();
            var result = await repository.GetNextSequenceForCountyAsync(countyNum);
            result.Should().Be(4);
        }

        [Fact]
        public async Task GetNextSequenceForCounty_LatestLabelInactive_Succeeds()
        {
            int countyNum = 103;
            using var repository = SimpleFileRepository();
            var result = await repository.GetNextSequenceForCountyAsync(countyNum);
            result.Should().Be(3);
        }

        [Fact]
        public async Task GetNextSequenceForCounty_NoSuchCounty_ReturnsOne()
        {
            int countyNum = 999;
            using var repository = SimpleFileRepository();

            Func<Task> action = async () =>
            {
                await repository.GetNextSequenceForCountyAsync(countyNum);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage($"County ID {countyNum} does not exist. (Parameter 'countyNum')");
        }

        // CreateFileAsync

        [Fact]
        public async Task CreateFile_Succeeds()
        {
            var repositoryHelper = new RepositoryHelper();
            Guid newFileId;
            int countyNum = 180;
            var expectedFileLabel = "180-0004";

            using (var repository = repositoryHelper.GetFileRepository())
            {
                newFileId = await repository.CreateFileAsync(countyNum);
            }

            using (var repository = repositoryHelper.GetFileRepository())
            {
                var createdFile = await repository.GetFileAsync(expectedFileLabel);

                createdFile.Active.Should().BeTrue();
                createdFile.FileLabel.Should().Be(expectedFileLabel);
                createdFile.Id.Should().Be(newFileId);
                createdFile.Facilities.Should().BeEquivalentTo(new List<FacilitySummaryDto>());
                createdFile.Cabinets.Should().BeEquivalentTo(new List<string>());
            }
        }

        [Fact]
        public async Task CreateFile_WithInvalidCounty_ThrowsException()
        {
            using var repository = new RepositoryHelper().GetFileRepository();
            int countyNum = 999;

            Func<Task> action = async () =>
            {
                var result = await repository.CreateFileAsync(countyNum);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage($"County ID {countyNum} does not exist. (Parameter 'countyNum')");
        }

        // UpdateFileAsync

        [Fact]
        public async Task UpdateFile_Succeeds()
        {
            var repositoryHelper = new RepositoryHelper();
            var file = DataHelpers.Files[0];

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
            using var repository = new RepositoryHelper().GetFileRepository();

            Func<Task> action = async () =>
            {
                await repository.UpdateFileAsync(default, default);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("File ID not found.");
        }
    }
}
