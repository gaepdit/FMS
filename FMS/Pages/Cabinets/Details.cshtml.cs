using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Cabinets
{
    public class DetailsModel : PageModel
    {
        public CabinetSummaryDto CabinetSummary { get; private set; }
        public DisplayMessage Message { get; private set; }

        private readonly ICabinetRepository _repository;
        public DetailsModel(ICabinetRepository repository) => _repository = repository;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            CabinetSummary = await _repository.GetCabinetSummaryAsync(id);

            if (CabinetSummary == null)
            {
                return NotFound();
            }

            Message = TempData?.GetDisplayMessage();
            return Page();
        }
    }
}