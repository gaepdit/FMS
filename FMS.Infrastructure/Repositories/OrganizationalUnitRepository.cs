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
    public class OrganizationalUnitRepository : IOrganizationalUnitRepository
    {
        private readonly FmsDbContext _context;

        public OrganizationalUnitRepository(FmsDbContext context) => _context = context;

        public async Task<int> CountAsync(OrganizationalUnitSpec spec)
        {
            return await _context.OrganizationalUnits.AsNoTracking().CountAsync();
        }

        public async Task<bool> OrganizationalUnitExistsAsync(Guid id) =>
            await _context.OrganizationalUnits.AnyAsync(e => e.Id == id);

        public async Task<bool> OrganizationalUnitExistsAsync(string name) =>
            await _context.OrganizationalUnits.AnyAsync(e => e.Name == name);

        public async Task<bool> OrganizationalUnitNameExistsAsync(string organizationalUnitName, Guid? ignoreId = null) => await _context.OrganizationalUnits.AnyAsync(e => e.Name == organizationalUnitName && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public async Task<OrganizationalUnitEditDto> GetOrganizationalUnitAsync(Guid id)
        {
            var organizationalUnit = await _context.OrganizationalUnits.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

            if (organizationalUnit == null)
            {
                return null;
            }

            return new OrganizationalUnitEditDto(organizationalUnit);
        }

        public async Task<IReadOnlyList<OrganizationalUnitSummaryDto>> GetOrganizationalUnitListAsync()
        {
            return await _context.OrganizationalUnits.AsNoTracking()
                .OrderBy(e => e.Name)
                .Select(e => new OrganizationalUnitSummaryDto(e))
                .ToListAsync();
        }

        public async Task<Guid> CreateOrganizationalUnitAsync(OrganizationalUnitCreateDto organizationalUnit)
        {
            if (organizationalUnit == null)
            {
                throw new ArgumentException("Values required for new Organizational Unit.");
            }

            if (string.IsNullOrWhiteSpace(organizationalUnit.Name))
            {
                throw new ArgumentException("New Name for Budget Code is required.");
            }

            return await CreateOrganizationalUnitInternalAsync(organizationalUnit);
        }

        public async Task<Guid> CreateOrganizationalUnitInternalAsync(OrganizationalUnitCreateDto organizationalUnit)
        {
            if (await OrganizationalUnitExistsAsync(organizationalUnit.Name))
            {
                throw new ArgumentException($"Organizational Unit {organizationalUnit.Name} Already Exists.");
            }

            var newOU = new OrganizationalUnit(organizationalUnit);

            await _context.OrganizationalUnits.AddAsync(newOU);
            await _context.SaveChangesAsync();

            return newOU.Id;
        }


        public Task UpdateOrganizationalUnitAsync(Guid id, OrganizationalUnitEditDto organizationalUnitUpdates)
        {
            if (string.IsNullOrWhiteSpace(organizationalUnitUpdates.Name))
            {
                throw new ArgumentException("Organizational Unit Name is required.");
            }
            return UpdateOrganizationalUnitUpdatesInternalAsync(id, organizationalUnitUpdates);
        }

        public async Task UpdateOrganizationalUnitUpdatesInternalAsync(Guid id, OrganizationalUnitEditDto organizationalUnitUpdates)
        {
            var organizationalUnit = await _context.OrganizationalUnits.FindAsync(id);

            if (organizationalUnit == null)
            {
                throw new ArgumentException("Organizational Unit ID not found.", nameof(id));
            }

            if (await OrganizationalUnitNameExistsAsync(organizationalUnit.Name, id))
            {
                throw new ArgumentException($"Organizational Unit Name '{organizationalUnit.Name}' already exists.");
            }

            organizationalUnit.Name = organizationalUnitUpdates.Name;
            organizationalUnit.Active = true;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrganizationalUnitStatusAsync(Guid id, bool active)
        {
            var organizationalUnit = await _context.OrganizationalUnits.FindAsync(id);

            if (organizationalUnit == null)
            {
                throw new ArgumentException("Organizational Unit ID not found");
            }

            organizationalUnit.Active = active;

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
        ~OrganizationalUnitRepository()
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
