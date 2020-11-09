using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Reports
{
    public class IndexModel : PageModel
    {
        private readonly IFacilityRepository _repository;
        private readonly IItemsListRepository _listRepository;

        // "Spec" is the Facility DTO bound to the HTML Page elements
        [BindProperty]
        public FacilitySpec Spec { get; set; }

        // Detailed Facility List to go to a report
        public IReadOnlyList<FacilityDetailDto> FacilityList { get; private set; }

        // Names from ItemList IDs
        public string CountyName { get; private set; }
        public string FacilityStatusName { get; private set; }
        public string FacilityTypeName { get; private set; }
        public string BudgetCodeName { get; private set; }
        public string OrganizationalUnitName { get; private set; }
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
            return File(await FacilityList.GetCsvByteArrayAsync<FacilityReportMap>(), "text/csv",
                $"FMS_export_{DateTime.Now:yyyy-MM-dd.HH-mm-ss.FFF}.csv");
        }

        private async Task SetNamesAsync()
        {
            CountyName = Data.Counties.Find(e => e.Id == Spec.CountyId)?.Name;
            BudgetCodeName = await _listRepository.GetBudgetCodeNameAsync(Spec.BudgetCodeId);
            ComplianceOfficerName = await _listRepository.GetComplianceOfficerNameAsync(Spec.ComplianceOfficerId);
            FacilityStatusName = await _listRepository.GetFacilityStatusNameAsync(Spec.FacilityStatusId);
            FacilityTypeName = await _listRepository.GetFacilityTypeNameAsync(Spec.FacilityTypeId);
            OrganizationalUnitName = await _listRepository.GetOrganizationalUnitNameAsync(Spec.OrganizationalUnitId);
        }

        private sealed class FacilityReportMap : ClassMap<FacilityDetailDto>
        {
            public FacilityReportMap()
            {
                Map(m => m.FacilityNumber).Index(0).Name("Facility Number");
                Map(m => m.FileLabel).Index(1).Name("File Label");
                Map(m => m.Name).Index(2).Name("Facility Name");
                Map(m => m.Address).Index(3).Name("Street Address");
                Map(m => m.City).Index(4).Name("City");
                Map(m => m.County.Name).Index(5).Name("County");
                Map(m => m.State).Index(6).Name("State");
                Map(m => m.PostalCode).Index(7).Name("ZIP Code");
                Map(m => m.Location).Index(8).Name("Location Description");
                Map(m => m.FacilityType.Name).Index(9).Name("Type/Environmental Interest");
                Map(m => m.ComplianceOfficer.Name).Index(10).Name("Compliance Officer");
                Map(m => m.OrganizationalUnit.Name).Index(11).Name("Organizational Unit");
                Map(m => m.BudgetCode.Name).Index(12).Name("Budget Code");
                Map(m => m.FacilityStatus.Name).Index(13).Name("Facility Status");
                Map(m => m.CabinetsToString).Index(14).Name("Cabinets");
                Map(m => m.RetentionRecordsToString).Index(15).Name("Retention Records");
            }
        }
    }
}