using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Infrastructure.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly FmsDbContext _context;
        public FileRepository(FmsDbContext context) => _context = context;

        public async Task<bool> FileExistsAsync(Guid id) =>
            await _context.Files.AnyAsync(e => e.Id == id);

        public async Task<FileDetailDto> GetFileAsync(Guid id)
        {
            var file = await _context.Files.AsNoTracking()
                .Include(e => e.Facilities)
                .Include(e => e.CabinetFiles).ThenInclude(c => c.Cabinet)
                .SingleOrDefaultAsync(e => e.Id == id);

            if (file == null)
            {
                return null;
            }

            return new FileDetailDto(file);
        }

        public async Task<FileDetailDto> GetFileAsync(string id)
        {
            var file = await _context.Files.AsNoTracking()
                .Include(e => e.Facilities)
                .Include(e => e.CabinetFiles).ThenInclude(c => c.Cabinet)
                .SingleOrDefaultAsync(e => e.FileLabel == id);

            if (file == null)
            {
                return null;
            }

            return new FileDetailDto(file);
        }

        public async Task<List<FacilitySummaryDto>> GetFacilitiesForFileAsync(Guid id) =>
            await _context.Facilities.AsNoTracking()
            .Where(e => e.FileId == id)
            .Select(e => new FacilitySummaryDto(e))
            .ToListAsync();

        public async Task<bool> FileHasActiveFacilities(Guid id) =>
            await _context.Facilities.AsNoTracking()
            .AnyAsync(e => e.FileId == id && e.Active);

        public async Task<int> CountAsync(FileSpec spec) =>
            await _context.Files.AsNoTracking()
            .Where(e => e.Active || spec.ShowInactive)
            .Where(e => string.IsNullOrWhiteSpace(spec.FileLabel) || e.FileLabel.Contains(spec.FileLabel))
            .Where(e => !spec.CountyId.HasValue || e.FileLabel.StartsWith(File.CountyString(spec.CountyId.Value)))
            .CountAsync();

        public async Task<IReadOnlyList<FileDetailDto>> GetFileListAsync(FileSpec spec) =>
            await _context.Files.AsNoTracking()
            .Include(e => e.Facilities)
            .Include(e => e.CabinetFiles).ThenInclude(c => c.Cabinet)
            .Where(e => e.Active || spec.ShowInactive)
            .Where(e => string.IsNullOrWhiteSpace(spec.FileLabel) || e.FileLabel.Contains(spec.FileLabel))
            .Where(e => !spec.CountyId.HasValue || e.FileLabel.StartsWith(File.CountyString(spec.CountyId.Value)))
            .OrderBy(e => e.FileLabel)
            .Select(e => new FileDetailDto(e))
            .ToListAsync();

        public async Task<int> GetNextSequenceForCountyAsync(int countyNum)
        {
            var countyString = File.CountyString(countyNum);
            var allSequencesForCounty = await _context.Files.AsNoTracking()
                .Where(e => e.FileLabel.StartsWith(countyString))
                .Select(e => int.Parse(e.FileLabel.Substring(4, 4)))
                .ToListAsync();
            return allSequencesForCounty.Count == 0 ? 1 : allSequencesForCounty.Max() + 1;
        }

        public async Task<Guid> CreateFileAsync(int countyNum)
        {
            if (!Data.Counties.Any(e => e.Id == countyNum))
            {
                throw new ArgumentException($"County ID {countyNum} does not exist.", nameof(countyNum));
            }

            var nextSequence = await GetNextSequenceForCountyAsync(countyNum);
            var file = new File(countyNum, nextSequence);

            await _context.Files.AddAsync(file);
            await _context.SaveChangesAsync();

            return file.Id;
        }

        public async Task UpdateFileAsync(Guid id, bool active)
        {
            var file = await _context.Files.FindAsync(id);

            if (file == null)
            {
                throw new ArgumentException("File ID not found.");
            }

            file.Active = active;

            await _context.SaveChangesAsync();
        }

        // TODO #49: Add Cabinets relationship 

        public Task<List<string>> GetCabinetsForFileAsync(Guid fileId)
        {
            throw new NotImplementedException();
        }

        public Task AddCabinetToFileAsync(Guid fileId, Guid cabinetId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveCabinetFromFileAsync(Guid fileId, Guid cabinetId)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support

        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects)
                    _context.Dispose();
                }

                // free unmanaged resources (unmanaged objects) and override finalizer
                // set large fields to null
                _disposedValue = true;
            }
        }

        // override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~FileRepository()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
