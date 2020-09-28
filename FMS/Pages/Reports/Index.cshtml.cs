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
        public SelectList States => new SelectList(Data.States);
        public SelectList FacilityStatuses { get; private set; }
        public SelectList FacilityTypes { get; private set; }
        public SelectList BudgetCodes { get; private set; }
        public SelectList OrganizationalUnits { get; private set; }
        public SelectList EnvironmentalInterests { get; private set; }
        public SelectList ComplianceOfficers { get; private set; }

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
            return Page();
        }

        public async Task<IActionResult> OnGetExportAsync(FacilitySpec spec)
        {
            FacilityList = await _repository.GetFacilityDetailListAsync(spec);
            await PopulateSelectsAsync();
            await ExportReportAsync();
            return Page();
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

        public string GetCountyName(int? id)
        {
            if(id == null)
            {
                return string.Empty;
            }
            foreach(SelectListItem item in Counties)
            {
                if(item.Value.Equals(id.ToString()))
                {
                    return item.Text;
                }
            }
            return string.Empty;
        }

        public string GetBudgetCodeName(Guid? id)
        {
            if(id == null)
            {
                return string.Empty;
            }
            foreach (SelectListItem item in BudgetCodes)
            {
                if(item.Value.ToString().Equals(id.Value.ToString()))
                {
                    return item.Text;
                }
            }
            return string.Empty;
        }

        public string GetComplianceOfficerName(Guid? id)
        {
            if (id == null)
            {
                return string.Empty;
            }
            foreach (SelectListItem item in ComplianceOfficers)
            {
                if (item.Value.ToString().Equals(id.Value.ToString()))
                {
                    return item.Text;
                }
            }
            return string.Empty;
        }

        public string GetEnvironmentalInterestName(Guid? id)
        {
            if (id == null)
            {
                return string.Empty;
            }
            foreach (SelectListItem item in EnvironmentalInterests)
            {
                if (item.Value.ToString().Equals(id.Value.ToString()))
                {
                    return item.Text;
                }
            }
            return string.Empty;
        }

        public string GetFacilityStatusName(Guid? id)
        {
            if (id == null)
            {
                return string.Empty;
            }
            foreach (SelectListItem item in FacilityStatuses)
            {
                if (item.Value.ToString().Equals(id.Value.ToString()))
                {
                    return item.Text;
                }
            }
            return string.Empty;
        }

        public string GetFacilityTypeName(Guid? id)
        {
            if (id == null)
            {
                return string.Empty;
            }
            foreach (SelectListItem item in FacilityTypes)
            {
                if (item.Value.ToString().Equals(id.Value.ToString()))
                {
                    return item.Text;
                }
            }
            return string.Empty;
        }

        public string GetOrganizationalUnitName(Guid? id)
        {
            if (id == null)
            {
                return string.Empty;
            }
            foreach (SelectListItem item in OrganizationalUnits)
            {
                if (item.Value.ToString().Equals(id.Value.ToString()))
                {
                    return item.Text;
                }
            }
            return string.Empty;
        }

        #endregion

        #region "Reports"
        public async Task<MemoryStream> ExportReportAsync()
        {
            var xm = new DataExportMeta(DateTime.Now);

            using (var ms = new MemoryStream())
            using (StreamWriter writer = new StreamWriter(xm.FilePath)) 
            using (CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Configuration.SanitizeForInjection = false;
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
                Map(m => m.Cabinets).Index(15).Name("Cabinet(s)");
                Map(m => m.RetentionRecords).Index(16).Name("Retention Records");
            }
        }

        #endregion
    }
}
