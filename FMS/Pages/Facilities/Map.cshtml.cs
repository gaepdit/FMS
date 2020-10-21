using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Facilities
{
    public class MapModel : PageModel
    {
        private readonly IFacilityRepository _repository;

        // "Spec" is the Facility DTO bound to the HTML Page elements
        public FacilityMapSpec Spec { get; set; }

        // List of facilities returned from the search
        public IReadOnlyList<FacilityMapSummaryDto> FacilityList { get; private set; }

        // Shows results section after searching
        public bool ShowResults { get; private set; }

        // true to show the table for results
        public bool ShowTable { get; private set; }

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
            if ((!string.IsNullOrEmpty(spec.GeocodeLat) && !string.IsNullOrEmpty(spec.GeocodeLng))
                && decimal.Parse(spec.GeocodeLat) > 0 && decimal.Parse(spec.GeocodeLng) < 0)
            {
                spec.Latitude = decimal.Parse(spec.GeocodeLat);
                spec.Longitude = decimal.Parse(spec.GeocodeLng);
            }

            if (spec.Latitude > 0 && spec.Longitude < 0)
            {
                FacilityList = await _repository.GetFacilityListAsync(spec);

                if (FacilityList == null || FacilityList.Count == 0)
                {
                    ShowNone = true;
                }

                if (spec.Output == "1")
                {
                    ShowMap = true;
                }

                if (spec.Output == "2" && FacilityList != null && FacilityList.Count > 0)
                {
                    ShowTable = true;
                }
            }
            else
            {
                ShowNone = true;
            }

            ExportSpec = new FacilityMapSpec()
            {
                Latitude = spec.Latitude,
                Longitude = spec.Longitude,
                ShowDeleted = spec.ShowDeleted,
                Radius = spec.Radius
            };

            ShowResults = true;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            FacilityList = await _repository.GetFacilityListAsync(ExportSpec);
            return File(await FacilityList.GetCsvByteArrayAsync<FacilityMapReportMap>(), "text/csv",
                $"FMS_export_{DateTime.Now:yyyy-MM-dd.HH-mm-ss.FFF}.csv");
        }

        private sealed class FacilityMapReportMap : ClassMap<FacilityMapSummaryDto>
        {
            public FacilityMapReportMap()
            {
                Map(m => m.FacilityNumber).Index(0).Name("Facility Number");
                Map(m => m.FileLabel).Index(1).Name("File Label");
                Map(m => m.Name).Index(2).Name("Facility Name");
                Map(m => m.Address).Index(3).Name("Street Address");
                Map(m => m.City).Index(4).Name("City");
                Map(m => m.State).Index(5).Name("State");
                Map(m => m.PostalCode).Index(6).Name("ZIP Code");
                Map(m => m.FacilityType).Index(7).Name("Facility Type");
                Map(m => m.Latitude).Index(8).Name("Latitude");
                Map(m => m.Longitude).Index(9).Name("Longitude");
                Map(m => m.FacilityStatus).Index(10).Name("Facility Status");
                Map(m => m.Distance).Index(11).Name("Distance (miles)");
            }
        }
    }
}