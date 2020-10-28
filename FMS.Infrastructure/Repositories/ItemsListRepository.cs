using FMS.Domain.Entities;
using FMS.Domain.Entities.Base;
using FMS.Domain.Repositories;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Infrastructure.Repositories
{
    /// <summary>
    /// Provides an class for retrieving lists of entities as key, value pairs
    /// </summary>
    public class ItemsListRepository : IItemsListRepository

    {
        private readonly FmsDbContext _context;
        public ItemsListRepository(FmsDbContext context) => _context = context;

        /// <summary>
        /// Generic method for retrieving a list of the given Entity as key/value pairs of the <see cref="ListItem"/> type.
        /// </summary>
        /// <typeparam name="TEntity">The Entity type.</typeparam>
        /// <param name="includeInactive">Whether to include records that have been marked as inactive.</param>
        /// <returns>A List of Entity key/value pairs of the ListItem type.</returns>
        private async Task<List<ListItem>> GetItemListAsync<TEntity>(bool includeInactive)
            where TEntity : BaseActiveModel, INamedModel
        {
            return await _context.Set<TEntity>().AsNoTracking()
                .Where(e => e.Active || includeInactive)
                .OrderBy(e => e.Name)
                .Select(e => new ListItem {Id = e.Id, Name = e.Name})
                .ToListAsync();
        }

        public async Task<IEnumerable<ListItem>> GetBudgetCodesItemListAsync(bool includeInactive = false) =>
            await GetItemListAsync<BudgetCode>(includeInactive);

        public async Task<IEnumerable<ListItem>> GetComplianceOfficersItemListAsync(bool includeInactive = false) =>
            await _context.ComplianceOfficers.AsNoTracking()
                .Where(e => e.Active || includeInactive)
                .OrderBy(e => e.FamilyName)
                .ThenBy(e => e.GivenName)
                .Select(e => new ListItem() {Id = e.Id, Name = e.FamilyName + ", " + e.GivenName})
                .ToListAsync();

        public async Task<IEnumerable<ListItem>> GetFacilityStatusesItemListAsync(bool includeInactive = false) =>
            await _context.FacilityStatuses.AsNoTracking()
                .Where(e => e.Active || includeInactive)
                .OrderBy(e => e.Status)
                .Select(e => new ListItem() {Id = e.Id, Name = e.Status})
                .ToListAsync();

        public async Task<IEnumerable<ListItem>> GetFacilityTypesItemListAsync(bool includeInactive = false) =>
            await _context.FacilityTypes.AsNoTracking()
                .Where(e => e.Active || includeInactive)
                .OrderBy(e => e.Name)
                .Select(e => new ListItem() {Id = e.Id, Name = e.DisplayName})
                .ToListAsync();

        public async Task<IEnumerable<ListItem>> GetOrganizationalUnitsItemListAsync(bool includeInactive = false) =>
            await GetItemListAsync<OrganizationalUnit>(includeInactive);

        public async Task<IEnumerable<ListItem>> GetCabinetsItemListAsync(bool includeInactive = false) =>
            await GetItemListAsync<Cabinet>(includeInactive);


        #region "Get single ListItem names"

        // && !id.Equals(Guid.Empty)
        //Used to get name value from a specific ListItem
        public async Task<string> GetBudgetCodeNameAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var item = await _context.BudgetCodes.AsNoTracking()
                    .SingleOrDefaultAsync(e => e.Id == id);
                return item?.Name;
            }

            return null;
        }

        public async Task<string> GetComplianceOfficerNameAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var item = await _context.ComplianceOfficers.AsNoTracking()
                    .SingleOrDefaultAsync(e => e.Id == id);
                if (item == null)
                {
                    return null;
                }

                return item.GivenName + " " + item.FamilyName;
            }

            return null;
        }

        public async Task<string> GetFacilityStatusNameAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var item = await _context.FacilityStatuses.AsNoTracking()
                    .SingleOrDefaultAsync(e => e.Id == id);
                return item?.Status;
            }

            return null;
        }

        public async Task<string> GetFacilityTypeNameAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var item = await _context.FacilityTypes.AsNoTracking()
                    .SingleOrDefaultAsync(e => e.Id == id);
                return item?.Name;
            }

            return null;
        }

        public async Task<string> GetOrganizationalUnitNameAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var item = await _context.OrganizationalUnits.AsNoTracking()
                    .SingleOrDefaultAsync(e => e.Id == id);
                return item?.Name;
            }

            return null;
        }

        #endregion

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
        ~ItemsListRepository()
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