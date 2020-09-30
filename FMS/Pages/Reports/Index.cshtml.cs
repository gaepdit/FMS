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
using Microsoft.AspNetCore.Mvc.Rendering;
using FMS.Domain.Data;
using System.Linq;

namespace FMS.Pages.Reports
{
    public class IndexModel : PageModel
    {
        private readonly IFacilityRepository _repository;
        private readonly IItemsListRepository _listRepository;

        // "Spec" is the Facility DTO bound to the HTML Page elements
        [BindProperty(SupportsGet = true)]
        public FacilitySpec Spec { get; set; }

        // Detailed Facility List to go to a report
        public IReadOnlyList<FacilityDetailDto> FacilityList { get; set; }

        // Select List 
        public SelectList Counties => new SelectList(Data.Counties, "Id", "Name");

        // Names from ItemList IDs
        public string CountyName { get; private set; }
        public string FacilityStatusName { get; private set; }
        public string FacilityTypeName { get; private set; }
        public string BudgetCodeName { get; private set; }
        public string OrganizationalUnitName { get; private set; }
        public string EnvironmentalInterestName { get; private set; }
        public string ComplianceOfficerName { get; private set; }

        public IndexModel(
            IFacilityRepository repository,
             IItemsListRepository listRepository)
        {
            _repository = repository;
            _listRepository = listRepository;
        }
        public async Task<IActionResult> OnGetAsync(FacilitySpec spec)
        {
            FacilityList = await _repository.GetFacilityDetailListAsync(spec);
            Spec = spec;
            await SetNamesAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            FacilityList = await _repository.GetFacilityDetailListAsync(Spec);

            return File(await GetCsvByteArrayAsync(), "text/csv", $"FMS_export_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.csv");
        }

        #region "ListItem Get Name Methods"

        private async Task SetNamesAsync()
        {
            SetCountyName();
            if (Spec.BudgetCodeId != null)
            {
                BudgetCodeName = await _listRepository.GetBudgetCodeNameAsync((Guid)Spec.BudgetCodeId);
            }
            if(Spec.ComplianceOfficerId != null)
            {
                ComplianceOfficerName = await _listRepository.GetComplianceOfficerNameAsync((Guid)Spec.ComplianceOfficerId);
            }
            if(Spec.EnvironmentalInterestId != null)
            {
                EnvironmentalInterestName = await _listRepository.GetEnvironmentalInterestNameAsync((Guid)Spec.EnvironmentalInterestId);
            }
            if(Spec.FacilityStatusId != null)
            {
                FacilityStatusName = await _listRepository.GetFacilityStatusNameAsync((Guid)Spec.FacilityStatusId);
            }
            if(Spec.FacilityTypeId != null)
            {
                FacilityTypeName = await _listRepository.GetFacilityTypeNameAsync((Guid)Spec.FacilityTypeId);
            }
            if(Spec.OrganizationalUnitId != null)
            {
                OrganizationalUnitName = await _listRepository.GetOrganizationalUnitNameAsync((Guid)Spec.OrganizationalUnitId);
            }
        }

        private void SetCountyName()
        {
            if(Spec.CountyId == null || Counties == null)
            {
                CountyName = string.Empty;
            }
            else
            {
                CountyName = Data.Counties.SingleOrDefault(e => e.Id == Spec.CountyId.Value)?.Name;
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
