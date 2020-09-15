using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FMS.Pages.Files
{
    public class DetailsModel : PageModel
    {
        private readonly IFileRepository _repository;
        public DetailsModel(IFileRepository repository) => _repository = repository;

        public FileDetailDto FileDetail { get; set; }
        public DisplayMessage Message { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            FileDetail = await _repository.GetFileAsync(id);

            if (FileDetail == null)
            {
                return NotFound();
            }

            Message = TempData?.GetDisplayMessage();
            return Page();
        }
    }
}