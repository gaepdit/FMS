using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using Microsoft.Win32;
using System.IO;
using System.Globalization;
using FMS.Helpers;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FMS.Pages.Reports
{
    public class IndexModel : PageModel
    {
        private readonly IFacilityRepository _repository;

        // "Spec" is the Facility DTO bound to the HTML Page elements
        [BindProperty(SupportsGet = true)]
        public FacilitySpec Spec { get; set; }

        public IReadOnlyList<FacilityDetailDto> FacilityList { get; set; }

        public IndexModel(
            IFacilityRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> OnGetAsync(FacilitySpec spec)
        {
            FacilityList = await _repository.GetFacilityDetailListAsync(spec);
            Spec = spec;
            return Page();
        }

        public async Task<IActionResult> OnGetExportAsync(FacilitySpec spec)
        {
            FacilityList = await _repository.GetFacilityDetailListAsync(spec);

            await ExportReportAsync();

            return Page();
        }

        public async Task<StreamWriter> ExportReportAsync()
        {
            var xm = new DataExportMeta(DateTime.Now);

            using (StreamWriter writer = new StreamWriter(xm.FilePath))
            using (CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Configuration.SanitizeForInjection = false;
                csv.Configuration.RegisterClassMap<FacilityReportMap>();
                csv.WriteRecords(FacilityList);
                
                await csv.FlushAsync();
                await writer.FlushAsync();

                return writer;
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
    }
}
