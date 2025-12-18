using FMS.Domain.Dto;
using FMS.Domain.Dto.Facility;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Pages.Facilities
{
    public class MapModel : PageModel
    {
        private readonly IFacilityRepository _repository;
        private readonly ISelectListHelper _listHelper;

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

        public SelectList FacilityTypes { get; private set; }

        [BindProperty]
        public FacilityMapSpec ExportSpec { get; set; }

        public MapModel(
            IFacilityRepository repository,
            ISelectListHelper listHelper)
        {
            _repository = repository;
            _listHelper = listHelper;
        }

        public async Task<IActionResult> OnGet(Guid? id)
        {
            if (id != null)
            {
                var map = await _repository.GetFacilityAsync(id.Value);
                Spec = new FacilityMapSpec(map);
                Spec.Radius = 3m;
                Spec.Output = "1";               
            }
           
            await PopulateSelectsAsync();
            return Page();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of methods should not be too high", Justification = "<Pending>")]
        public async Task<IActionResult> OnGetSearchAsync(FacilityMapSpec spec)
        {
            if (!ModelState.IsValid)
            {
                await PopulateSelectsAsync();
                return Page();
            }
            // Make sure GeoCoordinates are withing the State of Georgia or both Zero
            GeoCoordHelper.CoordinateValidation EnumVal = GeoCoordHelper.ValidateCoordinatesMap(spec.Latitude, spec.Longitude);
            string ValidationString = GeoCoordHelper.GetDescription(EnumVal);

            if (EnumVal != GeoCoordHelper.CoordinateValidation.Valid)
            {
                if (EnumVal == GeoCoordHelper.CoordinateValidation.LongNotInGeorgia)
                {
                    ModelState.AddModelError("spec.Longitude", ValidationString);
                }
                else
                {
                    ModelState.AddModelError("spec.Latitude", ValidationString);
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
                    Radius = spec.Radius,
                    FacilityTypeId = spec.FacilityTypeId
                };

            }
            else
            {
                ShowNone = true;
            }

            ShowResults = true;
            await PopulateSelectsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()

        {
            var fileName = $"FMS_Map_export{DateTime.Now:yyyy-MM-dd.HH-mm-ss.FFF}.xlsx";
            IReadOnlyList<FacilityMapSummaryDto> facilityMapSummaries = await _repository.GetFacilityListAsync(ExportSpec);
            var facilityMapDetail = from p in facilityMapSummaries select new FacilityMapSummaryDtoScalar(p);
            return File(facilityMapDetail.ExportExcelAsByteArray(ExportHelper.ReportType.Map), "application/vnd.ms.excel", fileName);
        }

        private async Task PopulateSelectsAsync()
        {
            FacilityTypes = await _listHelper.FacilityTypesSelectListAsync();
        }
    }
}