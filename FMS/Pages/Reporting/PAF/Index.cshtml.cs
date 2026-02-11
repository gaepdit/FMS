using Dapper;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Pages.Reporting.PAF
{
    public class IndexModel : PageModel
    {
        private readonly IReportingRepository _repository;
        public IndexModel(IReportingRepository repository) => _repository = repository;

        public IReadOnlyList<PAFReportDto> PAFReport { get; set; }

        public DisplayMessage DisplayMessage { get; private set; }

        public async Task OnGet()
        {
            PAFReport = await _repository.GetPAFReportAsync();
        }

    }
}
