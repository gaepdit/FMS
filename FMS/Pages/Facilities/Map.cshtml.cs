using FMS.Domain.Dto;
using FMS.Domain.Dto.Facility;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            // If Latitude is 0.00 then Longitude must also be 0.00
            if (!GeoCoordHelper.BothZeroOrBothNonzero(spec.Latitude, spec.Longitude))
            {
                ModelState.AddModelError("Spec.Latitude", "Latitude and Longitude must both be zero (0) or both valid coordinates.");
            }
            else
            {
                // If Latitude and/or Longitude fall outside State of Georgia, then alert user
                if (!GeoCoordHelper.ValidLat(spec.Latitude))
                {
                    ModelState.AddModelError("Spec.Latitude",
                        $"Latitude entered is outside State of Georgia. Must be between {GeoCoordHelper.UpperLat} and {GeoCoordHelper.LowerLat} North Latitude or zero if unknown.");
                }

                if (!GeoCoordHelper.ValidLong(spec.Longitude))
                {
                    ModelState.AddModelError("Spec.Longitude",
                        $"Longitude entered is outside State of Georgia. Must be between {GeoCoordHelper.EasternLong} and {GeoCoordHelper.WesternLong} West Longitude or zero if unknown.");
                }
            }

            //Radius is limited to values in Select list.
            if (!new[] { 0.25m, 0.5m, 1m, 3m, 5m }.Contains(spec.Radius)) spec.Radius = 0.25m; 

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(spec.GeocodeLat) && !string.IsNullOrEmpty(spec.GeocodeLng))
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

                    if (spec.Output == "2" && FacilityList is { Count: > 0 })
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

            }
            else
            {
                ShowNone = true;
            }

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