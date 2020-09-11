using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Data;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace FMS.Pages.Facilities
{
    public class IndexMapModel : PageModel
    {
        private readonly IFacilityRepository _repository;        

        // This the FacilityObject used for the search function
        // It's properties are bound to the HTML elements
        [BindProperty]
        public FacilityMapSpec Spec { get; set; }

        // Select Lists
        //public SelectList States => new SelectList(Data.States);

        // this is the list of facilities returned from the search
        public IReadOnlyList<FacilityMapSummaryDto> FacilityList { get; set; }

        // true when checkbox is checked to show only active sites
        [BindProperty]
        public bool ActiveOnly { get; set; }

        // true to show the <div> for Results(after post)
        [BindProperty]
        public bool ShowResults { get; set; }

        //true to show the map (after post)
        public bool ShowMap { get; set; }

        // Shows if there are no results in result set
        [BindProperty]
        public bool ShowNone { get; set; }

        [BindProperty]
        public string Output { get; set; }

        [BindProperty]
        public string LocalLat { get; set; }

        [BindProperty]
        public string LocalLng { get; set; }


        // search radius for map, bound to radius drop-down
        //public string SearchRadius { get; set; }

        public IndexMapModel(IFacilityRepository repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!String.IsNullOrEmpty(LocalLat) || !String.IsNullOrEmpty(LocalLng))
            {
                if (LocalLat.Length > 0 && LocalLng.Length > 0)
                {
                    if (float.Parse(LocalLat) > 0 && float.Parse(LocalLng) < 0)
                    {
                        Spec.Latitude = decimal.Parse(LocalLat);
                        Spec.Longitude = decimal.Parse(LocalLng);                       
                    }
                }
            }
            if (Spec.Latitude > 0 && Spec.Longitude < 0)
            {
                FacilityList = await _repository.GetFacilityListAsync(Spec);
            }
            else 
            {
                FacilityList = await _repository.GetFacilityListAsync(Spec);
            
            }

            // logic to show different "divs" depending if search finds results
            if (FacilityList != null && FacilityList.Count() > 0)
            {
               if (Output == "1")
                {
                    ShowMap = true;
                    ShowNone = false;
                    ShowResults = false;
                }
                if (Output == "2")
                {
                    ShowMap = false;
                    ShowNone = false;
                    ShowResults = true;
                }
            }
            else
            {
                ShowResults = false;
                ShowMap = false;
                ShowNone = true;
            }
            
            return Page();
        }      
        
    }
}