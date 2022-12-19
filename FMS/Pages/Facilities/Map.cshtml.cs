using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Dto.Facility;
using FMS.Domain.Repositories;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FMS.Domain.Data;
using FMS.Domain.Dto.PaginatedList;
using DocumentFormat.OpenXml.Office2016.Excel;

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

        public void OnGet()
        {
            // Method intentionally left empty.
        }

        public async Task<IActionResult> OnGetSearchAsync(FacilityMapSpec spec)
        {
            if (!string.IsNullOrEmpty(spec.GeocodeLat) && !string.IsNullOrEmpty(spec.GeocodeLng)
                && decimal.Parse(spec.GeocodeLat) > 0 && decimal.Parse(spec.GeocodeLng) < 0)
            {
                spec.Latitude = decimal.Parse(spec.GeocodeLat);
                spec.Longitude = decimal.Parse(spec.GeocodeLng);
            }

            // Radius is limited to values in Select list.
            if (!new[] {0.25m, 0.5m, 1m, 3m, 5m}.Contains(spec.Radius)) spec.Radius = 0.25m;

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

                if (spec.Output == "2" && FacilityList is {Count: > 0})
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
            var fileName = $"FMS_Map_export{DateTime.Now:yyyy-MM-dd.HH-mm-ss.FFF}.xlsx";
            IReadOnlyList<FacilityMapSummaryDto> facilityMapSummaries = await _repository.GetFacilityListAsync(ExportSpec);
            var facilityMapDetail = from p in facilityMapSummaries select new FacilityMapSummaryDtoScalar(p);
            return File(facilityMapDetail.ExportExcelAsByteArray(), "application/vnd.ms.excel", fileName);

            
        }
    }
}