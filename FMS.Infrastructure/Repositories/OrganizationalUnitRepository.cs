using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
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

        public async Task<bool> OrganizationalUnitExistsAsync(Guid id) =>
            await _context.OrganizationalUnits.AnyAsync(e => e.Id == id);

        public async Task<bool> OrganizationalUnitNameExistsAsync(string name, Guid? ignoreId = null) =>
            await _context.OrganizationalUnits.AnyAsync(e =>
                e.Name == name && (!ignoreId.HasValue || e.Id != ignoreId.Value));

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

        public async Task<IReadOnlyList<OrganizationalUnitSummaryDto>> GetOrganizationalUnitListAsync() =>
            await _context.OrganizationalUnits.AsNoTracking()
                .OrderBy(e => e.Name)
                .Select(e => new OrganizationalUnitSummaryDto(e))
                .ToListAsync();

        public Task<Guid> CreateOrganizationalUnitAsync(OrganizationalUnitCreateDto organizationalUnit)
        {
            Prevent.Null(organizationalUnit, nameof(organizationalUnit));

            if (string.IsNullOrWhiteSpace(organizationalUnit.Name))
            {
                throw new ArgumentException("New Name for Budget Code is required.");
            }

            return CreateOrganizationalUnitInternalAsync(organizationalUnit);
        }

        private async Task<Guid> CreateOrganizationalUnitInternalAsync(OrganizationalUnitCreateDto organizationalUnit)
        {
            if (await OrganizationalUnitNameExistsAsync(organizationalUnit.Name))
            {
                throw new ArgumentException($"Organizational Unit {organizationalUnit.Name} already exists.");
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

            return UpdateOrganizationalUnitInternalAsync(id, organizationalUnitUpdates);
        }

        private async Task UpdateOrganizationalUnitInternalAsync(Guid id, OrganizationalUnitEditDto organizationalUnitUpdates)
        {
            var organizationalUnit = await _context.OrganizationalUnits.FindAsync(id);

            if (organizationalUnit == null)
            {
                throw new ArgumentException("Organizational Unit ID not found.", nameof(id));
            }

            if (await OrganizationalUnitNameExistsAsync(organizationalUnitUpdates.Name, id))
            {
                throw new ArgumentException($"Organizational Unit Name '{organizationalUnitUpdates.Name}' already exists.");
            }

            organizationalUnit.Name = organizationalUnitUpdates.Name;

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