using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.Repositories
{
    public class CabinetRepository : ICabinetRepository
    {
        private readonly FmsDbContext _context;
        public CabinetRepository(FmsDbContext context) => _context = context;

        public async Task<bool> CabinetExistsAsync(Guid id) =>
            await _context.Cabinets.AnyAsync(e => e.Id == id);

        public async Task<bool> CabinetNameExistsAsync(string name, Guid? ignoreId = null) =>
            await _context.Cabinets.AnyAsync(e =>
                e.Name == name && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public async Task<IReadOnlyList<CabinetSummaryDto>> GetCabinetListAsync(bool includeInactive = true) =>
            await _context.Cabinets.AsNoTracking()
                .Where(e => e.Active || includeInactive)
                .OrderBy(e => e.FirstFileLabel)
                .ThenBy(e => e.Name)
                .Select(e => new CabinetSummaryDto(e))
                .ToListAsync();

        public async Task<CabinetSummaryDto> GetCabinetSummaryAsync(Guid id)
        {
            var cabinet = await _context.Cabinets.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

            return cabinet == null ? null : new CabinetSummaryDto(cabinet);
        }
        
        public async Task<CabinetDetailDto> GetCabinetDetailsAsync(string name)
        {
            var cabinet = await _context.Cabinets.AsNoTracking()
                .Include(e => e.CabinetFiles).ThenInclude(c => c.File)
                .SingleOrDefaultAsync(e => e.Name == name);

            if (cabinet == null) return null;

            cabinet.CabinetFiles = cabinet.CabinetFiles
                .OrderBy(e => e.File.Name).ToList();

            return new CabinetDetailDto(cabinet);
        }

        public Task CreateCabinetAsync(CabinetEditDto cabinet)
        {
            Prevent.Null(cabinet, nameof(cabinet));
            Prevent.NullOrEmpty(cabinet.Name, nameof(cabinet.Name));
            Prevent.NullOrEmpty(cabinet.FirstFileLabel, nameof(cabinet.FirstFileLabel));

            if (!File.IsValidFileLabelFormat(cabinet.FirstFileLabel))
            {
                throw new ArgumentException("The File Label is invalid.");
            }

            return CreateCabinetInternalAsync(cabinet);
        }

        private async Task CreateCabinetInternalAsync(CabinetEditDto cabinet)
        {
            if (await CabinetNameExistsAsync(cabinet.Name))
            {
                throw new ArgumentException($"Cabinet Name {cabinet.Name} already exists.");
            }

            var newCabinet = new Cabinet()
            {
                Name = cabinet.Name,
                FirstFileLabel = cabinet.FirstFileLabel,
            };

            await _context.Cabinets.AddAsync(newCabinet);
            await _context.SaveChangesAsync();
        }

        public Task UpdateCabinetAsync(Guid id, CabinetEditDto cabinetEdit)
        {
            Prevent.Null(cabinetEdit, nameof(cabinetEdit));
            Prevent.NullOrEmpty(cabinetEdit.Name, nameof(cabinetEdit.Name));
            Prevent.NullOrEmpty(cabinetEdit.FirstFileLabel, nameof(cabinetEdit.FirstFileLabel));

            if (!File.IsValidFileLabelFormat(cabinetEdit.FirstFileLabel))
            {
                throw new ArgumentException("The File Label is invalid.");
            }

            return UpdateCabinetInternalAsync(id, cabinetEdit);
        }

        private async Task UpdateCabinetInternalAsync(Guid id, CabinetEditDto cabinetEdit)
        {
            if (await CabinetNameExistsAsync(cabinetEdit.Name, id))
            {
                throw new ArgumentException($"Cabinet Name {cabinetEdit.Name} already exists.");
            }

            var cabinet = await _context.Cabinets.FindAsync(id);

            if (cabinet == null)
            {
                throw new ArgumentException("Cabinet ID not found.");
            }

            cabinet.Name = cabinetEdit.Name;
            cabinet.FirstFileLabel = cabinetEdit.FirstFileLabel;
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
        ~CabinetRepository()
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