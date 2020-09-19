using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FMS.Helpers;

namespace FMS.Pages.Facilities
{
    public class IndexMapModel : PageModel
    {
        private readonly IFacilityRepository _repository;

        // "Spec" is the Facility DTO bound to the HTML Page elements
        public FacilityMapSpec Spec { get; set; }      

        // List of facilities returned from the search
        public IReadOnlyList<FacilityMapSummaryDto> FacilityList { get; set; }     

        // true to show the <div> for Results(after post)
        [BindProperty]
        public bool ShowResults { get; set; }

        //true to show the map (after post)
        public bool ShowMap { get; set; }

        // Shows if there are no results in result set
        [BindProperty]
        public bool ShowNone { get; set; }
       
             public IndexMapModel(IFacilityRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Spec = new FacilityMapSpec();           
            return Page();
        }

        public async Task<IActionResult> OnGetSearchAsync(FacilityMapSpec spec)
        {

            if (!String.IsNullOrEmpty(spec.GeocodeLat) || !String.IsNullOrEmpty(spec.GeocodeLng))
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