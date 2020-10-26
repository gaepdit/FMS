using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS
{
    public interface ISelectListHelper
    {
        Task<SelectList> BudgetCodesSelectListAsync(bool includeInactive = false);
        Task<SelectList> ComplianceOfficersSelectListAsync(bool includeInactive = false);
        Task<SelectList> FacilityStatusesSelectListAsync(bool includeInactive = false);
        Task<SelectList> FacilityTypesSelectListAsync(bool includeInactive = false);
        Task<SelectList> OrganizationalUnitsSelectListAsync(bool includeInactive = false);
        Task<SelectList> CabinetsSelectListAsync(bool includeInactive = false);
    }

    public class SelectListHelper : ISelectListHelper
    {
        private readonly IItemsListRepository _listRepository;
        public SelectListHelper(IItemsListRepository listRepository) =>
            _listRepository = listRepository;

        public async Task<SelectList> BudgetCodesSelectListAsync(bool includeInactive = false) =>
            (await _listRepository.GetBudgetCodesItemListAsync(includeInactive)).ToSelectList();
        public async Task<SelectList> ComplianceOfficersSelectListAsync(bool includeInactive = false) =>
            (await _listRepository.GetComplianceOfficersItemListAsync(includeInactive)).ToSelectList();
        public async Task<SelectList> FacilityStatusesSelectListAsync(bool includeInactive = false) =>
            (await _listRepository.GetFacilityStatusesItemListAsync(includeInactive)).ToSelectList();
        public async Task<SelectList> FacilityTypesSelectListAsync(bool includeInactive = false) =>
            (await _listRepository.GetFacilityTypesItemListAsync(includeInactive)).ToSelectList();
        public async Task<SelectList> OrganizationalUnitsSelectListAsync(bool includeInactive = false) =>
            (await _listRepository.GetOrganizationalUnitsItemListAsync(includeInactive)).ToSelectList();
        public async Task<SelectList> CabinetsSelectListAsync(bool includeInactive = false) =>
            (await _listRepository.GetCabinetsItemListAsync(includeInactive)).ToSelectList();
    }

    /// <summary>
    /// Extension for converting item lists to the data structure used by SELECT elements.
    /// </summary>
    public static class SelectListExtensions
    {
        public static SelectList ToSelectList(this IEnumerable<ListItem> listItems) =>
            new SelectList(listItems, nameof(ListItem.Id), nameof(ListItem.Name));
    }
}
