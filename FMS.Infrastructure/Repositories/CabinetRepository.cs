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

        public async Task<IReadOnlyList<CabinetSummaryDto>> GetCabinetListAsync(bool includeInactive = true) =>
            await _context.Cabinets.AsNoTracking()
                .Where(e => e.Active || includeInactive)
                .OrderBy(e => e.CabinetNumber)
                .Select(e => new CabinetSummaryDto(e))
                .ToListAsync();

        public async Task<CabinetSummaryDto> GetCabinetSummaryAsync(Guid id)
        {
            var cabinet = await _context.Cabinets.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

            return cabinet == null ? null : new CabinetSummaryDto(cabinet);
        }

        public async Task<CabinetSummaryDto> GetCabinetSummaryAsync(string name)
        {
            var cabinet = await _context.Cabinets.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Name == name);

            return cabinet == null ? null : new CabinetSummaryDto(cabinet);
        }

        public async Task<CabinetDetailDto> GetCabinetDetailsAsync(Guid id)
        {
            var cabinet = await _context.Cabinets.AsNoTracking()
                .Include(e => e.CabinetFiles).ThenInclude(c => c.File)
                .SingleOrDefaultAsync(e => e.Id == id);

            return cabinet == null ? null : new CabinetDetailDto(cabinet);
        }

        public async Task<CabinetDetailDto> GetCabinetDetailsAsync(int cabinetNumber)
        {
            var cabinet = await _context.Cabinets.AsNoTracking()
                .Include(e => e.CabinetFiles).ThenInclude(c => c.File)
                .SingleOrDefaultAsync(e => e.CabinetNumber == cabinetNumber);

            if (cabinet == null) return null;

            cabinet.CabinetFiles = cabinet.CabinetFiles
                .OrderBy(e => e.File.Name).ToList();

            return new CabinetDetailDto(cabinet);
        }

        private async Task<int> GetNextCabinetNumber() =>
            1 + (await _context.Cabinets.MaxAsync(e => (int?) e.CabinetNumber) ?? 0);

        public async Task<string> GetNextCabinetName() =>
            Cabinet.CabinetNumberAsName(await GetNextCabinetNumber());

        public async Task<Guid> CreateCabinetAsync()
        {
            var cabinet = new Cabinet()
            {
                CabinetNumber = await GetNextCabinetNumber(),
                FirstFileLabel = "999-9999",
            };

            await _context.Cabinets.AddAsync(cabinet);
            await _context.SaveChangesAsync();

            return cabinet.Id;
        }

        public Task UpdateCabinetAsync(Guid id, CabinetEditDto cabinetEdit)
        {
            Prevent.NullOrEmpty(cabinetEdit.FirstFileLabel, nameof(cabinetEdit.FirstFileLabel));

            if (!File.IsValidFileLabelFormat(cabinetEdit.FirstFileLabel))
            {
                throw new ArgumentException("The File Label is invalid.");
            }

            return UpdateCabinetInternalAsync(id, cabinetEdit);
        }

        private async Task UpdateCabinetInternalAsync(Guid id, CabinetEditDto cabinetEdit)
        {
            var cabinet = await _context.Cabinets.FindAsync(id);

            if (cabinet == null)
            {
                throw new ArgumentException("Cabinet ID not found.");
            }

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