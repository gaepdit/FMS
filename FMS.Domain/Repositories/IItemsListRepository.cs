using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    /// <summary>
    /// Provides an interface for retrieving lists of entities as key, value pairs
    /// </summary>
    public interface IItemsListRepository : IDisposable
    {
        Task<IEnumerable<ListItem>> GetBudgetCodesItemListAsync(bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetComplianceOfficersItemListAsync(bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetEnvironmentalInterestsItemListAsync(bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetFacilityStatusesItemListAsync(bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetFacilityTypesItemListAsync(bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetFilesItemListAsync(bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetOrganizationalUnitsItemListAsync(bool includeInactive = false);
    }

    public class ListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
