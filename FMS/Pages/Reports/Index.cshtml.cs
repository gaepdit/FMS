using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.IO;
using System.Globalization;
using FMS.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using FMS.Domain.Data;
using System.Linq;

namespace FMS.Pages.Reports
{
    public class IndexModel : PageModel
    {
        private readonly IFacilityRepository _repository;
        private readonly ISelectListHelper _listHelper;

        // "Spec" is the Facility DTO bound to the HTML Page elements
        [BindProperty(SupportsGet = true)]
        public FacilitySpec Spec { get; set; }

        public IReadOnlyList<FacilityDetailDto> FacilityList { get; set; }

        // Select Lists
        public SelectList Counties => new SelectList(Data.Counties, "Id", "Name");
        public string CountyName { get; private set; }
        public SelectList States => new SelectList(Data.States);
        public SelectList FacilityStatuses { get; private set; }
        public string FacilityStatusName { get; private set; }
        public SelectList FacilityTypes { get; private set; }
        public string FacilityTypeName { get; private set; }
        public SelectList BudgetCodes { get; private set; }
        public string BudgetCodeName { get; private set; }
        public SelectList OrganizationalUnits { get; private set; }
        public string OrganizationalUnitName { get; private set; }
        public SelectList EnvironmentalInterests { get; private set; }
        public string EnvironmentalInterestName { get; private set; }
        public SelectList ComplianceOfficers { get; private set; }
        public string ComplianceOfficerName { get; private set; }

        public IndexModel(
            IFacilityRepository repository,
             ISelectListHelper listHelper)
        {
            _repository = repository;
            _listHelper = listHelper;
        }
        public async Task<IActionResult> OnGetAsync(FacilitySpec spec)
        {
            FacilityList = await _repository.GetFacilityDetailListAsync(spec);
            Spec = spec;
            await PopulateSelectsAsync();
            //SetNames();
            return Page();
        }

        public async Task<IActionResult> OnGetExportAsync(FacilitySpec spec)
        {
            FacilityList = await _repository.GetFacilityDetailListAsync(spec);
            Spec = spec;
            await PopulateSelectsAsync();
            //SetNames();

            return File(await GetCsvByteArrayAsync(), "text/csv", $"FMS_export_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.csv");
        }

        #region "Methods"

        private async Task PopulateSelectsAsync()
        {
            BudgetCodes = await _listHelper.BudgetCodesSelectListAsync();
            ComplianceOfficers = await _listHelper.ComplianceOfficersSelectListAsync();
            EnvironmentalInterests = await _listHelper.EnvironmentalInterestsSelectListAsync();
            FacilityStatuses = await _listHelper.FacilityStatusesSelectListAsync();
            FacilityTypes = await _listHelper.FacilityTypesSelectListAsync();
            OrganizationalUnits = await _listHelper.OrganizationalUnitsSelectListAsync();
        }

        private void SetNames()
        {
            SetCountyName();
            SetBudgetCodeName();
            SetComplianceOfficerName();
            SetEnvironmentalInterestName();
            SetFacilityStatusName();
            SetFacilityTypeName();
            SetOrganizationalUnitName();
        }

        private void SetCountyName()
        {
            if(Spec.CountyId == null || Counties == null)
            {
                CountyName = string.Empty;
            }
            else
            {
                ListItem item = (ListItem)Counties.Where(m => m.Value.Equals(Spec.CountyId));
                CountyName = item.Name;
            }
        }

        private void SetBudgetCodeName()
        {
            if(Spec.BudgetCodeId == null || BudgetCodes == null)
            {
                BudgetCodeName = string.Empty;
            }
            else
            {
                ListItem item = (ListItem)BudgetCodes.Where(m => m.Value.Equals(Spec.BudgetCodeId));
                BudgetCodeName = item.Name;
            }
        }

        private void SetComplianceOfficerName()
        {
            if (Spec.ComplianceOfficerId == null || ComplianceOfficers == null)
            {
                ComplianceOfficerName = string.Empty;
            }
            else
            {
                ListItem item = (ListItem)ComplianceOfficers.Where(m => m.Value.Equals(Spec.ComplianceOfficerId));
                ComplianceOfficerName = item.Name;
            }
        }

        private void SetEnvironmentalInterestName()
        {
            if (Spec.EnvironmentalInterestId == null || EnvironmentalInterests == null)
            {
                EnvironmentalInterestName = string.Empty;
            }
            else
            {
                ListItem item = (ListItem)EnvironmentalInterests.Where(m => m.Value.Equals(Spec.EnvironmentalInterestId));
                EnvironmentalInterestName = item.Name;
            }
        }

        private void SetFacilityStatusName()
        {
            if (Spec.FacilityStatusId == null || FacilityStatuses == null)
            {
                FacilityStatusName = string.Empty;
            }
            else
            {
                ListItem item = (ListItem)FacilityStatuses.Where(m => m.Value.Equals(Spec.FacilityStatusId));
                FacilityStatusName = item.Name;
            }
        }

        private void SetFacilityTypeName()
        {
            if (Spec.FacilityTypeId == null || FacilityTypes == null)
            {
                FacilityTypeName =  string.Empty;
            }
            else
            {
                ListItem item = (ListItem)FacilityTypes.Where(m => m.Value.Equals(Spec.FacilityTypeId));
                FacilityTypeName = item.Name;
            }
        }

        private void SetOrganizationalUnitName()
        {
            if (Spec.OrganizationalUnitId == null || OrganizationalUnits == null)
            {
                OrganizationalUnitName = string.Empty;
            }
            else
            {
                ListItem item = (ListItem)OrganizationalUnits.Where(m => m.Value.Equals(Spec.OrganizationalUnitId));
                OrganizationalUnitName = item.Name;
            }
        }

        #endregion

        #region "Reports"

        public async Task<byte[]> GetCsvByteArrayAsync()
        {
            return (await GetCsvMemoryStreamAsync()).ToArray();
        }

        public async Task<MemoryStream> GetCsvMemoryStreamAsync()
        {
            using (var ms = new MemoryStream())
            using (StreamWriter writer = new StreamWriter(ms)) 
            using (CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Configuration.SanitizeForInjection = true;
                csv.Configuration.RegisterClassMap<FacilityReportMap>();
                csv.WriteRecords(FacilityList);
                
                await csv.FlushAsync();
                await writer.FlushAsync();
                await ms.FlushAsync();

                return ms;
            }
        }

        public class FacilityReportMap : ClassMap<FacilityDetailDto>
        {
            public FacilityReportMap()
            {
                Map(m => m.FacilityNumber).Index(0).Name("Facility Number");
                Map(m => m.FileLabel).Index(1).Name("File Label");
                Map(m => m.Name).Index(2).Name("Facility Name");
                Map(m => m.Address).Index(3).Name("Street");
                Map(m => m.City).Index(4).Name("City");
                Map(m => m.County.Name).Index(5).Name("County");
                Map(m => m.State).Index(6).Name("State");
                Map(m => m.PostalCode).Index(7).Name("ZIP Code");
                Map(m => m.Location).Index(8).Name("Location Description");
                Map(m => m.FacilityType.Name).Index(9).Name("Facility Type");
                Map(m => m.ComplianceOfficer.Name).Index(10).Name("Compliance Officer");
                Map(m => m.OrganizationalUnit.Name).Index(11).Name("Organizational Unit");
                Map(m => m.BudgetCode.Name).Index(12).Name("Budget Code");
                Map(m => m.EnvironmentalInterest.Name).Index(13).Name("Environmental Interest");
                Map(m => m.FacilityStatus.Name).Index(14).Name("Facility Status");
                Map(m => m.CabinetsToString).Index(15).Name("Cabinet(s)");
            }
        }

        #endregion
    }
}
