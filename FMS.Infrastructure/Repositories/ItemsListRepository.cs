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

        #region "Get All List Items Lists"
        /// <summary>
        /// Generic method for retrieving a list of the given Entity as key/value pairs of the <see cref="ListItem"/> type.
        /// </summary>
        /// <typeparam name="TEntity">The Entity type.</typeparam>
        /// <param name="includeInactive">Whether to include records that have been marked as inactive.</param>
        /// <returns>A List of Entity key/value pairs of the ListItem type.</returns>
        private Task<List<ListItem>> GetItemListAsync<TEntity>(bool includeInactive)
            where TEntity : BaseActiveModel, INamedModel
        {
            return _context.Set<TEntity>().AsNoTracking()
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

        // Phase III updates
        public async Task<IEnumerable<ListItem>> GetActionsTakenListAsync(bool includeInactive = false) =>
            await GetItemListAsync<ActionTaken>(includeInactive);

        public async Task<IEnumerable<ListItem>> GetAbandonedInactiveListAsync(bool includeInactive = false) =>
            await GetItemListAsync<AbandonedInactive>(includeInactive);

        public async Task<IEnumerable<ListItem>> GetAllowedActionsTakenListAsync(Guid? id, bool includeInactive = false) => await _context.AllowedActionsTaken.AsNoTracking()
            .Where(e => (e.EventTypeId == id) && (e.Active || includeInactive))
            .Include(e => e.ActionTaken)
            .OrderBy(e => e.ActionTaken.Name)
            .Select(e => new ListItem() { Id = e.ActionTaken.Id, Name = e.ActionTaken.Name })
            .ToListAsync();

        public async Task<IEnumerable<ListItem>> GetChemicalListAsync(bool includeInactive = false) => 
            await _context.Chemicals.AsNoTracking()
            .Where(e => e.Active || includeInactive)
            .OrderBy(e => e.CasNo)
            .Select(e => new ListItem() { Id = e.Id, Name = e.DisplayName })
            .ToListAsync();

        public async Task<IEnumerable<ListItem>> GetContactTitlesListAsync(bool includeInactive = false) => 
            await GetItemListAsync<ContactTitle>(includeInactive);

        public async Task<IEnumerable<ListItem>> GetContactTypesListAsync(bool includeInactive = false) =>
            await GetItemListAsync<ContactType>(includeInactive);

        public async Task<IEnumerable<ListItem>> GetEventTypesListAsync(bool includeInactive = false) =>
            await GetItemListAsync<EventType>(includeInactive);

        public async Task<IEnumerable<ListItem>> GetFundingSourceListAsync(bool includeInactive = false) =>
            await _context.FundingSources.AsNoTracking()
                .Where(e => e.Active || includeInactive)
                .OrderBy(e => e.Name)
                .Select(e => new ListItem() { Id = e.Id, Name = e.DisplayName })
                .ToListAsync();

        public async Task<IEnumerable<ListItem>> GetGroundwaterStatusesListAsync(bool includeInactive = false) =>
            await _context.GroundwaterStatuses.AsNoTracking()
                .Where(e => e.Active || includeInactive)
                .OrderBy(e => e.Name)
                .Select(e => new ListItem() { Id = e.Id, Name = e.DisplayName })
                .ToListAsync();

        public async Task<IEnumerable<ListItem>> GetOverallStatusesListAsync(bool includeInactive = false) =>
            await _context.OverallStatuses.AsNoTracking()
                .Where(e => e.Active || includeInactive)
                .OrderBy(e => e.Name)
                .Select(e => new ListItem() { Id = e.Id, Name = e.DisplayName })
                .ToListAsync();

        public async Task<IEnumerable<ListItem>> GetParcelTypesListAsync(bool includeInactive = false) =>
            await GetItemListAsync<ParcelType>(includeInactive);

        public async Task<IEnumerable<ListItem>> GetSoilStatusesListAsync(bool includeInactive = false) =>
            await _context.SoilStatuses.AsNoTracking()
                .Where(e => e.Active || includeInactive)
                .OrderBy(e => e.Name)
                .Select(e => new ListItem() { Id = e.Id, Name = e.DisplayName })
                .ToListAsync();

        public async Task<IEnumerable<ListItem>> GetSourceStatusesListAsync(bool includeInactive = false) =>
            await _context.SourceStatuses.AsNoTracking()
                .Where(e => e.Active || includeInactive)
                .OrderBy(e => e.Name)
                .Select(e => new ListItem() { Id = e.Id, Name = e.DisplayName })
                .ToListAsync();

        public async Task<IEnumerable<ListItem>> GetGapsAssessmentListAsync(bool includeInactive = false) =>
            await _context.GapsAssessments.AsNoTracking()
                .Where(e => e.Active || includeInactive)
                .OrderBy(e => e.Name)
                .Select(e => new ListItem() { Id = e.Id, Name = e.DisplayName })
                .ToListAsync();

        #endregion

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

        // Phase III updates
        public async Task<string> GetActionTakenNameAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var item = await _context.ActionsTaken.AsNoTracking()
                    .SingleOrDefaultAsync(e => e.Id == id);
                return item?.Name;
            }
            return null;
        }
        public async Task<string> GetAbandonedInactiveNameAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var item = await _context.AbandonedInactives.AsNoTracking()
                    .SingleOrDefaultAsync(e => e.Id == id);
                return item?.Name;
            }
            return null;
        }
        public async Task<string> GetChemicalNameAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var item = await _context.Chemicals.AsNoTracking()
                    .SingleOrDefaultAsync(e => e.Id == id);
                return item?.Name;
            }
            return null;
        }

        public async Task<string> GetContactTitleNameAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var item = await _context.ContactTitles.AsNoTracking()
                    .SingleOrDefaultAsync(e => e.Id == id);
                return item?.Name;
            }
            return null;
        }

        public async Task<string> GetContactTypeNameAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var item = await _context.ContactTypes.AsNoTracking()
                    .SingleOrDefaultAsync(e => e.Id == id);
                return item?.Name;
            }
            return null;
        }

        public async Task<string> GetEventTypeNameAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var item = await _context.EventTypes.AsNoTracking()
                    .SingleOrDefaultAsync(e => e.Id == id);
                return item?.Name;
            }
            return null;
        }

        public async Task<string> GetFundingSourceNameAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var item = await _context.FundingSources.AsNoTracking()
                    .SingleOrDefaultAsync(e => e.Id == id);
                return item?.Name;
            }
            return null;
        }

        public async Task<string> GetGroundwaterStatusNameAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var item = await _context.GroundwaterStatuses.AsNoTracking()
                    .SingleOrDefaultAsync(e => e.Id == id);
                return item?.Name;
            }
            return null;
        }

        public async Task<string> GetOverallStatusNameAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var item = await _context.OverallStatuses.AsNoTracking()
                    .SingleOrDefaultAsync(e => e.Id == id);
                return item?.Name;
            }
            return null;
        }

        public async Task<string> GetParcelTypeNameAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var item = await _context.ParcelTypes.AsNoTracking()
                    .SingleOrDefaultAsync(e => e.Id == id);
                return item?.Name;
            }
            return null;
        }

        public async Task<string> GetSoilStatusNameAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var item = await _context.SoilStatuses.AsNoTracking()
                    .SingleOrDefaultAsync(e => e.Id == id);
                return item?.Name;
            }
            return null;
        }

        public async Task<string> GetSourceStatusNameAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var item = await _context.SourceStatuses.AsNoTracking()
                    .SingleOrDefaultAsync(e => e.Id == id);
                return item?.Name;
            }
            return null;
        }

        public async Task<string> GetGAPSAssessmentNameAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var item = await _context.GapsAssessments.AsNoTracking()
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