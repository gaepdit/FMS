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

        // Phase III updates
        Task<SelectList> ActionTakenSelectListAsync(bool includeInactive = false);
        Task<SelectList> ChemicalsSelectListAsync(bool includeInactive = false);
        Task<SelectList> ContactTitlesSelectListAsync(bool includeInactive = false);
        Task<SelectList> ContactTypesSelectListAsync(bool includeInactive = false);
        Task<SelectList> EventTypesSelectListAsync(bool includeInactive = false);
        Task<SelectList> FundingSourceSelectListAsync(bool includeInactive = false);
        Task<SelectList> GroundwaterStatusesSelectListAsync(bool includeInactive = false);
        Task<SelectList> OverallStatusesSelectListAsync(bool includeInactive = false);
        Task<SelectList> ParcelTypesSelectListAsync(bool includeInactive = false);
        Task<SelectList> SoilStatusesSelectListAsync(bool includeInactive = false);
        Task<SelectList> SourceStatusesSelectListAsync(bool includeInactive = false);
        
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

        // Phase III updates
        public async Task<SelectList> ActionTakenSelectListAsync(bool includeInactive = false) =>
            (await _listRepository.GetActionsTakenListAsync(includeInactive)).ToSelectList();
        public async Task<SelectList> ChemicalsSelectListAsync(bool includeInactive = false) =>
            (await _listRepository.GetChemicalListAsync(includeInactive)).ToSelectList();
        public async Task<SelectList> ContactTitlesSelectListAsync(bool includeInactive = false) =>
            (await _listRepository.GetContactTitlesListAsync(includeInactive)).ToSelectList();
        public async Task<SelectList> ContactTypesSelectListAsync(bool includeInactive = false) =>
            (await _listRepository.GetContactTypesListAsync(includeInactive)).ToSelectList();
        public async Task<SelectList> EventTypesSelectListAsync(bool includeInactive = false)=> (await _listRepository.GetEventTypesListAsync(includeInactive)).ToSelectList();
        public async Task<SelectList> FundingSourceSelectListAsync(bool includeInactive = false)=> (await _listRepository.GetFundingSourceListAsync(includeInactive)).ToSelectList();
        public async Task<SelectList> GroundwaterStatusesSelectListAsync(bool includeInactive = false) => (await _listRepository.GetGroundwaterStatusesListAsync(includeInactive)).ToSelectList();
        public async Task<SelectList> OverallStatusesSelectListAsync(bool includeInactive = false)=> (await _listRepository.GetOverallStatusesListAsync(includeInactive)).ToSelectList();
        public async Task<SelectList> ParcelTypesSelectListAsync(bool includeInactive = false)=> (await _listRepository.GetParcelTypesListAsync(includeInactive)).ToSelectList();
        public async Task<SelectList> SoilStatusesSelectListAsync(bool includeInactive = false)=> (await _listRepository.GetSoilStatusesListAsync(includeInactive)).ToSelectList();
        public async Task<SelectList> SourceStatusesSelectListAsync(bool includeInactive = false)=> (await _listRepository.GetSourceStatusesListAsync(includeInactive)).ToSelectList();
    }

    /// <summary>
    /// Extension for converting item lists to the data structure used by SELECT elements.
    /// </summary>
    public static class SelectListExtensions
    {
        public static SelectList ToSelectList(this IEnumerable<ListItem> listItems) =>
            new(listItems, nameof(ListItem.Id), nameof(ListItem.Name));
    }
}
