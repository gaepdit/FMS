using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Cabinets
{
    public class IndexModel : PageModel
    {
        public IReadOnlyList<CabinetSummaryDto> Cabinets { get; private set; }

        private readonly ICabinetRepository _repository;
        public IndexModel(ICabinetRepository repository) => _repository = repository;

        public async Task<IActionResult> OnGetAsync()
        {
            Cabinets = await _repository.GetCabinetListAsync();
            return Page();
        }
    }
}