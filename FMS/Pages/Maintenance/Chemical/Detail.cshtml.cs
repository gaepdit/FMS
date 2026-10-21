using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.Chemical
{
    public class DetailModel : PageModel
    {
        private readonly IChemicalRepository _repository;
        public DetailModel(IChemicalRepository repository) => _repository = repository;

        public IReadOnlyList<ChemicalSummaryDto> Chemicals { get; private set; }

        public ChemicalSummaryDto Chemical { get; }

        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Chemicals = await _repository.GetChemicalListAsync();
            DisplayMessage = TempData?.GetDisplayMessage();
            return Page();
        }
    }
}
