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
                .Where(e => e.Active == true || includeInactive)
                .OrderBy(e => e.Name)
                .Select(e => new ListItem { Id = e.Id, Name = e.Name })
                .ToListAsync();
        }

        public async Task<IEnumerable<ListItem>> GetBudgetCodesItemListAsync(bool includeInactive = false) =>
            await GetItemListAsync<BudgetCode>(includeInactive);

        public async Task<IEnumerable<ListItem>> GetComplianceOfficersItemListAsync(bool includeInactive = false) =>
            await _context.ComplianceOfficers.AsNoTracking()
                .Where(e => e.Active == true || includeInactive)
                .OrderBy(e => e.FamilyName)
                .ThenBy(e => e.GivenName)
                .Select(e => new ListItem() { Id = e.Id, Name = e.FamilyName + ", " + e.GivenName })
                .ToListAsync();

        public async Task<IEnumerable<ListItem>> GetEnvironmentalInterestsItemListAsync(bool includeInactive = false) =>
            await GetItemListAsync<EnvironmentalInterest>(includeInactive);

        public async Task<IEnumerable<ListItem>> GetFacilityStatusesItemListAsync(bool includeInactive = false) =>
            await _context.FacilityStatuses.AsNoTracking()
                .Where(e => e.Active == true || includeInactive)
                .OrderBy(e => e.Status)
                .Select(e => new ListItem() { Id = e.Id, Name = e.Status })
                .ToListAsync();

        public async Task<IEnumerable<ListItem>> GetFacilityTypesItemListAsync(bool includeInactive = false) =>
            await GetItemListAsync<FacilityType>(includeInactive);

        public async Task<IEnumerable<ListItem>> GetFilesItemListAsync(bool includeInactive = false) =>
            await _context.Files.AsNoTracking()
                .Where(e => e.Active == true || includeInactive)
                .OrderBy(e => e.FileLabel)
                .Select(e => new ListItem() { Id = e.Id, Name = e.FileLabel })
                .ToListAsync();

        public async Task<IEnumerable<ListItem>> GetOrganizationalUnitsItemListAsync(bool includeInactive = false) =>
            await GetItemListAsync<OrganizationalUnit>(includeInactive);

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

                _disposedValue = true;
            }
        }

        //  override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
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