using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Domain.Entities;
using FMS.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FMS.Pages.Facilities
{
    public class IndexMapModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly FmsDbContext _context;

        private readonly Guid _nullguid = new Guid("00000000-0000-0000-0000-000000000000");

        // true when checkbox is checked to show only active sites
        [BindProperty]
        public bool ActiveOnly { get; set; }

        // true to show the <div> for Results(after post)
        [BindProperty]
        public bool ShowResults { get; set; }

        //true to show the map (after post)
        public bool ShowMap { get; set; }

        //Guid for Test facility for development
        public Guid TestGuid { get; set; }

        //Facility Object
        public Facility Facility { get; set; }

        public IEnumerable<Facility> Facilities { get; set; }

        public IndexMapModel(
            ILogger<IndexModel> logger,
            FmsDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        //public void OnGet(Guid guid, bool Active = false, bool Result = false, bool Map = false)
        //{
        //    if (guid != _nullguid)
        //    {
        //        Facilities = new Facility[] { };
        //        Facilities = _context.Facilities;
        //    }
        //    //if (guid != _nullguid)
        //    //{
        //    //    Facility = await _context.Facilities.FirstOrDefaultAsync(m => m.Id == guid);
        //    //    Facilities = new Facility[] { Facility };
        //    //}

        //    ActiveOnly = Active;
        //    ShowResults = Result;
        //    ShowMap = Map;
        //    TestGuid = guid;
        //    //return Page();
        //}

        public async Task<IActionResult> OnGetAsync(Guid guid, bool Active = false, bool Result = false, bool Map = false)
        {
            if (guid != _nullguid)
            {
                Facilities = _context.Facilities;
            }
            //if (guid != _nullguid)
            //{
            //    Facility = await _context.Facilities.FirstOrDefaultAsync(m => m.Id == guid);
            //    Facilities = new Facility[] { Facility };
            //}

            ActiveOnly = Active;
            ShowResults = Result;
            ShowMap = Map;
            TestGuid = guid;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // static data from SeedData for building and testing
            TestGuid = new Guid("3FF8B38C-B2A0-4A32-B703-BEAB9138B7F0");
            // this is where the entity search will go

            ShowMap = true;
            
            string url = "/Facilities/IndexMap";
            return RedirectToPage(url, new { guid = TestGuid, Active = ActiveOnly, Result = ShowResults, Map = ShowMap });
        }
    }
}