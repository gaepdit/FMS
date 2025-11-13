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
        #region "Get All List Items Lists"
        /// <summary>
        /// Get All List Items Lists
        ///
        Task<IEnumerable<ListItem>> GetBudgetCodesItemListAsync(bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetComplianceOfficersItemListAsync(bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetFacilityStatusesItemListAsync(bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetFacilityTypesItemListAsync(bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetOrganizationalUnitsItemListAsync(bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetCabinetsItemListAsync(bool includeInactive = false);

        // Phase III updates
        Task<IEnumerable<ListItem>> GetActionsTakenListAsync(bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetAbandonedInactiveListAsync(bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetAllowedActionsTakenListAsync(Guid? id, bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetChemicalListAsync(bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetChemicalsFromSubstanceAsync(Guid facilityId, bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetContactTypesListAsync(bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetEventTypesListAsync(bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetEventContractorsListAsync(bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetFundingSourceListAsync(bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetGroundwaterStatusesListAsync(bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetLocationClassesListAsync(bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetOverallStatusesListAsync(bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetParcelTypesListAsync(bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetSoilStatusesListAsync(bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetSourceStatusesListAsync(bool includeInactive = false);
        Task<IEnumerable<ListItem>> GetGapsAssessmentListAsync(bool includeInactive = false);

        #endregion

        #region "Get Single List Item Names"
        Task<string> GetBudgetCodeNameAsync(Guid? id);
        Task<string> GetComplianceOfficerNameAsync(Guid? id);
        Task<string> GetFacilityStatusNameAsync(Guid? id);
        Task<string> GetFacilityTypeNameAsync(Guid? id);
        Task<string> GetOrganizationalUnitNameAsync(Guid? id);

        //Phase III updates
        Task<string> GetActionTakenNameAsync(Guid? id);
        Task<string> GetAbandonedInactiveNameAsync(Guid? id);
        Task<string> GetChemicalNameAsync(Guid? id);
        Task<string> GetContactTypeNameAsync(Guid? id);
        Task<string> GetEventTypeNameAsync(Guid? id);
        Task<string> GetEventContractorNameAsync(Guid? id);
        Task<string> GetFundingSourceNameAsync(Guid? id);
        Task<string> GetGroundwaterStatusNameAsync(Guid? id);
        Task<string> GetLocationClassNameAsync(Guid? id);
        Task<string> GetOverallStatusNameAsync(Guid? id);
        Task<string> GetParcelTypeNameAsync(Guid? id);
        Task<string> GetSoilStatusNameAsync(Guid? id);
        Task<string> GetSourceStatusNameAsync(Guid? id);
        Task<string> GetGAPSAssessmentNameAsync(Guid? id);
        #endregion
    }

    public class ListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
