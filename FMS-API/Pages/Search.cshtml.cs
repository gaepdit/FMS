using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SQLitePCL;

namespace FMS
{
    public class SearchModel : PageModel
    {
        private readonly ILogger<SearchModel> _logger;

        public JsonSearchService JsonService;

        public IEnumerable<Facility> Facilities { get; private set; }

        public IEnumerable<County> Counties { get; private set; }

        public SearchModel(
            ILogger<SearchModel> logger,
            JsonSearchService jsonService)
        {
            _logger = logger;
            JsonService = jsonService;
        }
        public void Page_Load()
        {

        }

        public void OnGet()
        {
            Counties = JsonService.GetCounties();
        }

        public void OnSubmit()
        {

        }
    }
}