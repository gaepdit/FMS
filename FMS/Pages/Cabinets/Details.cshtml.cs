using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FMS.Pages.Cabinets
{
    public class DetailsModel : PageModel
    {
        public CabinetDetailDto CabinetDetail { get; set; }
        public DisplayMessage Message { get; set; }

        private readonly ICabinetRepository _repository;
        public DetailsModel(ICabinetRepository repository) => _repository = repository;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            CabinetDetail = await _repository.GetCabinetDetailsAsync(id);

            if (CabinetDetail == null)
            {
                return NotFound();
            }

            Message = TempData?.GetDisplayMessage();
            return Page();
        }
    }
}
