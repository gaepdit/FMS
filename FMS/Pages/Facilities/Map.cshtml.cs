using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.IO;
using System.Globalization;


namespace FMS.Pages.Facilities
{
    public class MapModel : PageModel
    {
        private readonly IFacilityRepository _repository;

        // "Spec" is the Facility DTO bound to the HTML Page elements
        public FacilityMapSpec Spec { get; set; }

        // List of facilities returned from the search
        public IReadOnlyList<FacilityMapSummaryDto> FacilityList { get; private set; }

        // true to show the table for results
        public bool ShowResults { get; private set; }

        // true to show the map for results
        public bool ShowMap { get; private set; }

        // Shows if there are no results in result set
        public bool ShowNone { get; private set; }

        [BindProperty]
        public FacilityMapSpec ExportSpec { get; set; }

        public MapModel(IFacilityRepository repository) => _repository = repository;

        public void OnGet() { }

        public async Task<IActionResult> OnGetSearchAsync(FacilityMapSpec spec)
        {
            if ((!string.IsNullOrEmpty(spec.GeocodeLat) || !string.IsNullOrEmpty(spec.GeocodeLng))
                && decimal.Parse(spec.GeocodeLat) > 0 && decimal.Parse(spec.GeocodeLng) < 0)
            {
                spec.Latitude = decimal.Parse(spec.GeocodeLat);
                spec.Longitude = decimal.Parse(spec.GeocodeLng);
            }

            if (spec.Latitude > 0 && spec.Longitude < 0)
            {
                FacilityList = await _repository.GetFacilityListAsync(spec);
                if (FacilityList != null && FacilityList.Count > 0)
                {
                    if (spec.Output == "1")
                    {
                        ShowMap = true;
                        ShowResults = false;
                    }
                    else if (spec.Output == "2")
                    {
                        ShowMap = false;
                        ShowResults = true;
                    }
                }
                else
                {
                    ShowNone = true;
                }
            }
            else
            {
                ShowNone = true;
            }

            ExportSpec = new FacilityMapSpec()
            {
                Latitude=spec.Latitude,
                Longitude=spec.Longitude,
                ActiveOnly=spec.ActiveOnly,
                Radius=spec.Radius
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            FacilityList = await _repository.GetFacilityListAsync(ExportSpec);
            return File(await GetCsvByteArrayAsync(), "text/csv",
                $"FMS_export_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.csv");
        }

        #region "Reports"

        private async Task<byte[]> GetCsvByteArrayAsync()
        {
            MemoryStream ms = null;
            StreamWriter writer = null;
            CsvWriter csv = null;

            try
            {
                ms = new MemoryStream();
                writer = new StreamWriter(ms);
                csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

                csv.Configuration.SanitizeForInjection = true;
                csv.Configuration.RegisterClassMap<FacilityMapReportMap>();
                await csv.WriteRecordsAsync(FacilityList);

                await csv.FlushAsync();
                await writer.FlushAsync();
                await ms.FlushAsync();

                return ms.ToArray();
            }
            finally
            {
                if (csv != null) await csv.DisposeAsync();
                if (writer != null) await writer.DisposeAsync();
                if (ms != null) await ms.DisposeAsync();
            }
        }

        private class FacilityMapReportMap : ClassMap<FacilityMapSummaryDto>
        {
            public FacilityMapReportMap()
            {
                Map(m => m.FacilityNumber).Index(0).Name("Facility Number");
                Map(m => m.FileLabel).Index(1).Name("File Label");
                Map(m => m.Name).Index(2).Name("Facility Name");
                Map(m => m.Address).Index(3).Name("Street Address");
                Map(m => m.City).Index(4).Name("City");
                Map(m => m.State).Index(6).Name("State");
                Map(m => m.PostalCode).Index(7).Name("ZIP Code");
                Map(m => m.FacilityType).Index(9).Name("Facility Type");
                Map(m => m.FacilityStatus).Index(14).Name("Facility Status");
                Map(m => m.Latitude).Index(12).Name("Latitude");
                Map(m => m.Longitude).Index(13).Name("Longitude");
                Map(m => m.Distance).Index(15).Name("Distance");
            }
        }

        #endregion
    }
}