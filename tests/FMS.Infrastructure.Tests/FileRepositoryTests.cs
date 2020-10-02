using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.Repositories;
using TestHelpers;
using TestSupport.EfHelpers;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace FMS.Infrastructure.Tests
{
    public class FileRepositoryTests
    {
        /// <summary>
        /// Simple File list used by SimpleFileRepository
        /// </summary>
        private static readonly List<File> SimpleFileList = new List<File>
        {
            new File {Id = Guid.NewGuid(), FileLabel = "099-0001"},
            new File {Id = Guid.NewGuid(), FileLabel = "111-0001"},
            new File {Id = Guid.NewGuid(), FileLabel = "102-0001"},
            new File {Id = Guid.NewGuid(), FileLabel = "102-0003"},
            new File {Id = Guid.NewGuid(), FileLabel = "103-0001"},
            new File {Id = Guid.NewGuid(), FileLabel = "103-0002", Active = false}
        };

        /// <summary>
        /// Simple Facility list used by SimpleFileRepository
        /// </summary>
        private static readonly List<Facility> SimpleFacilityList = new List<Facility>
        {
            new Facility
            {
                Id = Guid.NewGuid(),
                FacilityNumber = "ABC",
                FileId = SimpleFileList[0].Id,
                CountyId = 131
            },
            new Facility
            {
                Id = Guid.NewGuid(),
                FacilityNumber = "DEF",
                FileId = SimpleFileList[1].Id,
                CountyId = 131,
                Active = false
            },
        };

        /// <summary>
        /// Simplified File repository used by some unit tests.
        /// </summary>
        /// <returns>A FileRepository with a simplified list of Files and Facilities.</returns>
        private static FileRepository SimpleFileRepository()
        {
            var context = new FmsDbContext(SqliteInMemory.CreateOptions<FmsDbContext>());
            context.Database.EnsureCreated();
            context.Files.AddRange(SimpleFileList);
            context.Facilities.AddRange(SimpleFacilityList);
            context.SaveChanges();
            return new FileRepository(context);
        }

        // FileExistsAsync

        [Fact]
        public async Task FileExists_Exists_ReturnsTrue()
        {
            using var repository = SimpleFileRepository();
            var result = await repository.FileExistsAsync(SimpleFileList[0].Id);
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task FileExists_NotExists_ReturnsFalse()
        {
            using var repository = SimpleFileRepository();
            var result = await repository.FileExistsAsync(Guid.Empty);
            result.ShouldBeFalse();
        }

        // GetFileAsync

        [Fact]
        public async Task GetFile_ById_ReturnsCorrectFile()
        {
            using var repository = SimpleFileRepository();
            var file = SimpleFileList[0];

            var result = await repository.GetFileAsync(file.Id);

            result.Id.Should().Be(file.Id);
            result.FileLabel.Should().Be(file.FileLabel);
        }

        [Fact]
        public async Task GetNonexistentFile_ById_ReturnsNull()
        {
            using var repository = SimpleFileRepository();
            var result = await repository.GetFileAsync(Guid.Empty);
            result.ShouldBeNull();
        }

        [Fact]
        public async Task GetFile_ByName_ReturnsCorrectFile()
        {
            using var repository = SimpleFileRepository();
            var file = SimpleFileList[0];
            var result = await repository.GetFileAsync(file.FileLabel);
            result.Id.Should().Be(file.Id);
        }

        [Fact]
        public async Task GetNonexistentFile_ByName_ReturnsNull()
        {
            using var repository = SimpleFileRepository();
            var result = await repository.GetFileAsync(string.Empty);
            result.ShouldBeNull();
        }

        // FileHasActiveFacilities

        [Fact]
        public async Task FileHasActiveFacilities_HasActive_ReturnsTrue()
        {
            using var repository = SimpleFileRepository();
            var result = await repository.FileHasActiveFacilities(SimpleFileList[0].Id);
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task FileHasActiveFacilities_HasNoActive_ReturnsFalse()
        {
            using var repository = SimpleFileRepository();
            var result = await repository.FileHasActiveFacilities(SimpleFileList[1].Id);
            result.ShouldBeFalse();
        }

        [Fact]
        public async Task FileHasActiveFacilities_NonExistentId_ReturnsFalse()
        {
            using var repository = SimpleFileRepository();
            var result = await repository.FileHasActiveFacilities(Guid.Empty);
            result.ShouldBeFalse();
        }

        // CountAsync

        [Fact]
        public async Task FileCount_DefaultSpec_ReturnsCountOfActiveFiles()
        {
            using var repository = SimpleFileRepository();
            var spec = new FileSpec();

            var result = await repository.CountAsync(spec);

            var expected = SimpleFileList.Count(e => e.Active);
            result.Should().Be(expected);
        }

        [Fact]
        public async Task FileCount_WithInactive_ReturnsCountOfAll()
        {
            using var repository = SimpleFileRepository();
            var spec = new FileSpec() {ShowInactive = true};

            var result = await repository.CountAsync(spec);

            var expected = SimpleFileList.Count;
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(99)]
        [InlineData(102)]
        public async Task FileCount_ByCounty_ReturnsCorrectCount(int countyId)
        {
            using var repository = SimpleFileRepository();
            var spec = new FileSpec() {CountyId = countyId};
            var countyString = countyId.ToString().PadLeft(3, '0');

            var result = await repository.CountAsync(spec);

            var expected = SimpleFileList.Count(e => e.FileLabel.StartsWith(countyString) && e.Active);
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("00")]
        [InlineData("0001")]
        [InlineData("2-0")]
        public async Task FileCount_ByFileLabel_ReturnsCorrectCount(string fileLabel)
        {
            using var repository = SimpleFileRepository();
            var spec = new FileSpec() {FileLabel = fileLabel};

            var result = await repository.CountAsync(spec);

            var expected = SimpleFileList.Count(e => e.FileLabel.Contains(fileLabel) && e.Active);
            result.Should().Be(expected);
        }

        // GetFileListAsync

        [Fact]
        public async Task FileSearch_Default_ReturnsActiveFiles()
        {
            using var repository = SimpleFileRepository();
            var result = await repository.GetFileListAsync(new FileSpec());
            result.Count.Should().Be(SimpleFileList.Count(e => e.Active));
        }

        [Fact]
        public async Task FileSearch_WithInactive_ReturnsAllFiles()
        {
            using var repository = SimpleFileRepository();
            var spec = new FileSpec() {ShowInactive = true};
            var result = await repository.GetFileListAsync(spec);
            result.Count.Should().Be(SimpleFileList.Count);
        }

        [Theory]
        [InlineData(99)]
        [InlineData(102)]
        public async Task FileSearch_ByCounty_ReturnsCorrectList(int countyId)
        {
            using var repository = SimpleFileRepository();
            var spec = new FileSpec() {CountyId = countyId};
            var countyString = countyId.ToString().PadLeft(3, '0');

            var result = await repository.GetFileListAsync(spec);

            result.Count.Should().Be(SimpleFileList.Count(e => e.FileLabel.StartsWith(countyString) && e.Active));
        }

        [Theory]
        [InlineData("00")]
        [InlineData("0001")]
        [InlineData("2-0")]
        public async Task FileSearch_ByFileNumber_ReturnsCorrectList(string fileLabelSpec)
        {
            using var repository = SimpleFileRepository();
            var spec = new FileSpec() {FileLabel = fileLabelSpec};

            var result = await repository.GetFileListAsync(spec);

            result.Count.Should().Be(SimpleFileList.Count(e => e.FileLabel.Contains(fileLabelSpec) && e.Active));
        }

        // GetNextSequenceForCounty

        [Fact]
        public async Task GetNextSequenceForCounty_Succeeds()
        {
            const int countyNum = 111;
            using var repository = SimpleFileRepository();
            var result = await repository.GetNextSequenceForCountyAsync(countyNum);
            result.Should().Be(2);
        }


        [Fact]
        public async Task GetNextSequenceForCounty_TwoDigit_Succeeds()
        {
            const int countyNum = 99;
            using var repository = SimpleFileRepository();
            var result = await repository.GetNextSequenceForCountyAsync(countyNum);
            result.Should().Be(2);
        }

        [Fact]
        public async Task GetNextSequenceForCounty_NoCurrentLabel_ReturnsOne()
        {
            const int countyNum = 101;
            using var repository = SimpleFileRepository();
            var result = await repository.GetNextSequenceForCountyAsync(countyNum);
            result.Should().Be(1);
        }

        [Fact]
        public async Task GetNextSequenceForCounty_CurrentLabelSkipsNumber_Succeeds()
        {
            const int countyNum = 102;
            using var repository = SimpleFileRepository();
            var result = await repository.GetNextSequenceForCountyAsync(countyNum);
            result.Should().Be(4);
        }

        [Fact]
        public async Task GetNextSequenceForCounty_LatestLabelInactive_Succeeds()
        {
            const int countyNum = 103;
            using var repository = SimpleFileRepository();
            var result = await repository.GetNextSequenceForCountyAsync(countyNum);
            result.Should().Be(3);
        }

        [Fact]
        public async Task GetNextSequenceForCounty_NoSuchCounty_ReturnsOne()
        {
            const int countyNum = 999;

            Func<Task> action = async () =>
            {
                using var repository = SimpleFileRepository();
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
            const int countyNum = 170;
            const string expectedFileLabel = "170-0002";

            using (var repository = repositoryHelper.GetFileRepository())
            {
                newFileId = await repository.CreateFileAsync(countyNum);
            }

            using (var repository = repositoryHelper.GetFileRepository())
            {
                var createdFile = await repository.GetFileAsync(expectedFileLabel);

                createdFile.Active.ShouldBeTrue();
                createdFile.FileLabel.Should().Be(expectedFileLabel);
                createdFile.Id.Should().Be(newFileId);
                createdFile.Facilities.Should().BeEquivalentTo(new List<FacilitySummaryDto>());
                createdFile.Cabinets.Should().BeEquivalentTo(new List<string>());
            }
        }

        [Fact]
        public async Task CreateFile_WithInvalidCounty_ThrowsException()
        {
            const int countyNum = 999;

            Func<Task> action = async () =>
            {
                using var repository = SimpleFileRepository();
                await repository.CreateFileAsync(countyNum);
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
            Func<Task> action = async () =>
            {
                using var repository = SimpleFileRepository();
                await repository.UpdateFileAsync(default, default);
            };

            (await action.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false))
                .WithMessage("File ID not found.");
        }

        // GetCabinetsForFileAsync

        [Fact]
        public async Task GetCabinetsForFileAsync()
        {
            using var repository = new RepositoryHelper().GetFileRepository();
            var file = DataHelpers.Files.FirstOrDefault(e => e.Name == "180-0001");

            var result = await repository.GetCabinetsForFileAsync(file.Id);

            result.Should().BeEquivalentTo(DataHelpers.GetCabinetSummariesForFile(file.Id));
        }

        // GetCabinetsNotAssociatedWithFileAsync

        [Fact]
        public async Task GetCabinetsNotAssociatedWithFileAsync()
        {
            using var repository = new RepositoryHelper().GetFileRepository();
            var file = DataHelpers.Files.FirstOrDefault(e => e.Name == "180-0001");
            var cabs = DataHelpers.GetCabinetSummariesForFile(file.Id);

            var result = await repository.GetCabinetsAvailableForFileAsync(file.Id);

            result.Should().NotContain(cabs);
        }

        // AddCabinetToFileAsync

        [Fact]
        public async Task AddCabinetFile_Succeeds()
        {
            using var repository = new RepositoryHelper().GetFileRepository();
            var cabinet = DataHelpers.Cabinets.FirstOrDefault(e => e.Name == "C006");
            var fileId = DataHelpers.Files.FirstOrDefault().Id;

            await repository.AddCabinetToFileAsync(cabinet.Id, fileId);

            var file = await repository.GetFileAsync(fileId);
            file.Cabinets.Should().BeEquivalentTo(new List<CabinetSummaryDto> {new CabinetSummaryDto(cabinet)});
        }

        // RemoveCabinetFromFileAsync

        [Fact]
        public async Task RemoveCabinetFile_Succeeds()
        {
            using var repository = new RepositoryHelper().GetFileRepository();
            var cf = DataHelpers.CabinetFiles[0];
            var cabinetName = DataHelpers.GetCabinetSummary(cf.CabinetId).Name;

            await repository.RemoveCabinetFromFileAsync(cf.CabinetId, cf.FileId);

            var file = await repository.GetFileAsync(cf.FileId);
            file.Cabinets.Should().NotContain(cabinetName);
        }
    }
}