﻿using System;
using System.Linq;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Dto.PaginatedList;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly FmsDbContext _context;

        public FileRepository(FmsDbContext context) => _context = context;

        public Task<bool> FileExistsAsync(Guid id) =>
            _context.Files.AnyAsync(e => e.Id == id);

        public async Task<FileDetailDto> GetFileAsync(Guid id)
        {
            var file = await _context.Files.AsNoTracking()
                .Include(e => e.Facilities)
                .SingleOrDefaultAsync(e => e.Id == id);

            if (file == null) return null;

            var fileDetail = new FileDetailDto(file);
            fileDetail.Cabinets = (await _context.GetCabinetListAsync(false))
                .GetCabinetsForFile(fileDetail.FileLabel);

            return fileDetail;
        }

        public async Task<FileDetailDto> GetFileAsync(string fileLabel)
        {
            var file = await _context.Files.AsNoTracking()
                .Include(e => e.Facilities)
                .SingleOrDefaultAsync(e => e.FileLabel == fileLabel);

            if (file == null) return null;

            var fileDetail = new FileDetailDto(file);
            fileDetail.Cabinets = (await _context.GetCabinetListAsync(false))
                .GetCabinetsForFile(fileDetail.FileLabel);

            return fileDetail;
        }

        public Task<bool> FileHasActiveFacilities(Guid id) =>
            _context.Facilities.AsNoTracking()
                .AnyAsync(e => e.FileId == id && e.Active);

        public Task<int> CountAsync(FileSpec spec) =>
            _context.Files.AsNoTracking()
                .Where(e => spec.ShowInactive || e.Active)
                .Where(e => string.IsNullOrWhiteSpace(spec.FileLabel) || e.FileLabel.Contains(spec.FileLabel))
                .Where(e => !spec.CountyId.HasValue || e.FileLabel.StartsWith(File.CountyString(spec.CountyId.Value)))
                .CountAsync();

        public async Task<PaginatedList<FileDetailDto>> GetFileListAsync(
            FileSpec spec, int pageNumber, int pageSize)
        {
            Prevent.NegativeOrZero(pageNumber, nameof(pageNumber));
            Prevent.NegativeOrZero(pageSize, nameof(pageSize));

            var items = await _context.Files.AsNoTracking()
                .Include(e => e.Facilities)
                .Where(e => spec.ShowInactive || e.Active)
                .Where(e => string.IsNullOrWhiteSpace(spec.FileLabel) || e.FileLabel.Contains(spec.FileLabel))
                .Where(e => !spec.CountyId.HasValue || e.FileLabel.StartsWith(File.CountyString(spec.CountyId.Value)))
                .OrderBy(e => e.FileLabel)
                .Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .Select(e => new FileDetailDto(e))
                .ToListAsync();

            var cabinets = await _context.GetCabinetListAsync(false);
            foreach (var item in items)
            {
                item.Cabinets = cabinets.GetCabinetsForFile(item.FileLabel);
            }

            var totalCount = await CountAsync(spec);
            return new PaginatedList<FileDetailDto>(items, totalCount, pageNumber, pageSize);
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

        #region IDisposable Support

        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;

            if (disposing)
            {
                // dispose managed state (managed objects)
                _context.Dispose();
            }

            // free unmanaged resources (unmanaged objects) and override finalizer
            // set large fields to null
            _disposedValue = true;
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