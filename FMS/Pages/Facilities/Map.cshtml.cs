using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IReadOnlyList<FacilityMapSummaryDto> FacilityList { get; set; }

        // true to show the <div> for Results(after post)
        public bool ShowResults { get; set; }

        //true to show the map (after post)
        public bool ShowMap { get; set; }

        public SelectList States => new SelectList(Data.States);

        // Shows if there are no results in result set
        public bool ShowNone { get; set; }

        public string distance { get; set; }

        public MapModel(IFacilityRepository repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet()
        {
            Spec = new FacilityMapSpec();
            return Page();
        }

        public async Task<IActionResult> OnGetSearchAsync(FacilityMapSpec spec)
        {

            if (!string.IsNullOrEmpty(spec.GeocodeLat) || !string.IsNullOrEmpty(spec.GeocodeLng))
            {
                if (spec.GeocodeLat.Length > 0 && spec.GeocodeLng.Length > 0)
                {
                    if (float.Parse(spec.GeocodeLat) > 0 && float.Parse(spec.GeocodeLng) < 0)
                    {
                        spec.Latitude = decimal.Parse(spec.GeocodeLat);
                        spec.Longitude = decimal.Parse(spec.GeocodeLng);
                    }
                }
            }

            if (spec.Latitude > 0 && spec.Longitude < 0)
            {
                FacilityList = await _repository.GetFacilityListAsync(spec);

                if (FacilityList != null && FacilityList.Count() > 0)
                {
                    if (spec.Output == "1")
                    {
                        ShowMap = true;
                        ShowResults = false;
                    }
                    if (spec.Output == "2")
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

            return Page();
        }
    }
}