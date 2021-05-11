using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Files
{
    public class DetailsModel : PageModel
    {
        private readonly IFileRepository _repository;
        public DetailsModel(IFileRepository repository) => _repository = repository;

        public FileDetailDto FileDetail { get; private set; }
        public DisplayMessage Message { get; private set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
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