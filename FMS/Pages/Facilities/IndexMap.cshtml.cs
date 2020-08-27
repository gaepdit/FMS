using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Facilities
{
    public class IndexMapModel : PageModel
    {
        private readonly IFacilityRepository _repository;

        // This the FacilityObject used for the search function
        // It's properties are bound to the HTML elements
        [BindProperty]
        public FacilityMapSpec Spec { get; set; }

        // this is the list of facilities returned from the search
        public IReadOnlyList<FacilitySummaryDto> FacilityList { get; set; }

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
            // this is the entity search
            // Spec is the FacilitySummaryDto that is bound to this search page
            FacilityList = await _repository.GetFacilityListAsync(Spec);

            // logic to show different "divs" depending if search finds results
            if (FacilityList.Count() > 0)
            {
                ShowMap = true;
                ShowNone = false;
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