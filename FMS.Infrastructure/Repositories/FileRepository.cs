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

        public async Task<bool> FileExistsAsync(Guid id)
        {
            return await _context.Files.AnyAsync(e => e.Id == id);
        }

        public async Task<FileDetailDto> GetFileAsync(Guid id)
        {
            var file = await _context.Files.AsNoTracking()
                //.Include(e => e.FileCabinets)
                //.Include(e => e.Facilities)
                .SingleOrDefaultAsync(e => e.Id == id);

            if (file == null)
            {
                return null;
            }

            return new FileDetailDto(file);
        }

        public Task<int> CountAsync(FileSpec spec)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<FileSummaryDto>> GetFileListAsync(int? Cnty)
        {
            return await _context.Files.AsNoTracking()
                .Where(e => !Cnty.HasValue || e.FileLabel.StartsWith(Cnty.ToString()))
                .OrderBy(e => e.FileLabel)
                .Select(e => new FileSummaryDto(e))
                .ToListAsync();
        }

        public async Task<Guid> CreateFileAsync(FileCreateDto file)
        {
            File newFile = new File(file);
            _context.Files.Add(newFile);
            _context.SaveChanges();

            return await Task.FromResult(newFile.Id);
        }

        public async Task UpdateFileAsync(Guid id, FileEditDto fileUpdates)
        {
            var file = await _context.Files.FindAsync(id);
            if (file == null)
            {
                throw new ArgumentException("Facility ID not found", nameof(id));
            }

            file.Active = fileUpdates.Active;
            file.FileLabel = fileUpdates.FileLabel;
            // TODO: Add FileCabinets and Facilities
            await _context.SaveChangesAsync();
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
