using System.Collections.Generic;
using FMS.Domain.Entities;
using FMS.Infrastructure.Contexts;
using FMS.Infrastructure;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace FMS
{
    //[Route("Facility")]
    public class FacilitySearchModel : PageModel
    {
        
        private readonly ILogger<FacilitySearchModel> _logger;

        public JsonSearchService _JsonService;

        private readonly FmsDbContext _context;

        //public FacilitySearchModel(FmsDbContext context)
        //{
        //    _context = context;
        //}

        public IEnumerable<Facility> facilities { get; private set; }

        public IEnumerable<County> counties { get; private set; }

        public IEnumerable<BudgetCode> budgetCodes { get; private set; }

        public FacilitySearchModel(
            ILogger<FacilitySearchModel> logger,
            JsonSearchService jsonService,
            FmsDbContext context)
        {
            _logger = logger;
            _JsonService = jsonService;
            _context = context;
        }
        
        public void OnGet()
        {
            counties = _JsonService.GetCounties();
            //budgetCodes = _context.GetBudgetCodes();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string url = "FacilitySearchResults";
            return RedirectToPage(url);
        }
    }
}