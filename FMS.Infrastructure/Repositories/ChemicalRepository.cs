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
    public class ChemicalRepository : IChemicalRepository
    {
        private readonly FmsDbContext _context;

        public ChemicalRepository(FmsDbContext context) => _context = context;

        public Task<bool> ChemicalExistsAsync(Guid id) =>
            _context.Chemicals.AnyAsync(e => e.Id == id);

        public Task<bool> ChemicalCasNoExistsAsync(string casNo, Guid? ignoreId = null) =>
            _context.Chemicals.AnyAsync(e =>
                e.CasNo == casNo && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public Task<bool> ChemicalChemicalNameExistsAsync(string chemicalName, Guid? ignoreId = null) =>
            _context.Chemicals.AnyAsync(e =>
                e.ChemicalName == chemicalName && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public Task<bool> ChemicalCommonNameExistsAsync(string commonName, Guid? ignoreId = null) =>
            _context.Chemicals.AnyAsync(e =>
                e.CommonName == commonName && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public async Task<ChemicalEditDto> GetChemicalByIdAsync(Guid id)
        {
            var chemical = await _context.Chemicals.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

            return chemical == null ? null : new ChemicalEditDto(chemical);
        }

        public async Task<Chemical> GetChemicalByNameAsync(string name)
        {
            var chemical = await _context.Chemicals.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Name == name);

            return chemical;
        }

        public async Task<IReadOnlyList<ChemicalSummaryDto>> GetChemicalListAsync()
        {
            return await _context.Chemicals.AsNoTracking()
                .OrderByDescending(e => e.Active)
                .ThenBy(e => e.ChemicalName)
                .Select(e => new ChemicalSummaryDto(e))
                .ToListAsync();
        }

        public Task<Guid> CreateChemicalAsync(ChemicalCreateDto chemical)
        {
            Prevent.Null(chemical, nameof(chemical));
            Prevent.NullOrEmpty(chemical.ChemicalName, nameof(chemical.ChemicalName));
            Prevent.NullOrEmpty(chemical.CasNo, nameof(chemical.CasNo));

            return CreateChemicalInternalAsync(chemical);
        }

        private async Task<Guid> CreateChemicalInternalAsync(ChemicalCreateDto chemical)
        {
            if (await ChemicalCasNoExistsAsync(chemical.CasNo))
            {
                throw new ArgumentException($"Chemical with CasNo: '{chemical.CasNo}' already exists.");
            }

            var newChemical = new Chemical
            {
                Id = Guid.NewGuid(),
                Active = true,
                ChemicalName = chemical.ChemicalName,
                CasNo = chemical.CasNo,
                ToxValue = chemical.ToxValue,
                CommonName = chemical.CommonName,
                MCLs = chemical.MCLs
            };
            await _context.Chemicals.AddAsync(newChemical);
            await _context.SaveChangesAsync();
            return newChemical.Id;
        }

        public Task UpdateChemicalAsync(Guid id, ChemicalEditDto chemicalUpdates)
        {
            Prevent.Null(chemicalUpdates, nameof(chemicalUpdates));
            return UpdateChemicalInternalAsync(id, chemicalUpdates);
        }

        private async Task UpdateChemicalInternalAsync(Guid id, ChemicalEditDto chemicalUpdates)
        {
            var chemical = await _context.Chemicals.FindAsync(id) ?? throw new ArgumentException("Chemical ID not found.", nameof(id));

            if (await ChemicalCasNoExistsAsync(chemicalUpdates.CasNo, id))
            {
                throw new ArgumentException($"Chemical CasNo '{chemicalUpdates.CasNo}' already exists.");
            }

            chemical.CasNo = chemicalUpdates.CasNo;
            chemical.ChemicalName = chemicalUpdates.ChemicalName;
            chemical.CommonName = chemicalUpdates.CommonName;
            chemical.ToxValue = chemicalUpdates.ToxValue;
            chemical.MCLs = chemicalUpdates.MCLs;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateChemicalStatusAsync(Guid id, bool active)
        {
            var chemical = await _context.Chemicals.FindAsync(id) ?? throw new ArgumentException("Chemical ID not found");

            chemical.Active = active;

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
        ~ChemicalRepository()
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
