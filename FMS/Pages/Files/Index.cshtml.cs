using System.Threading.Tasks;
using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Dto.PaginatedList;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FMS.Pages.Files
{
    public class IndexModel : PageModel
    {
        private readonly IFileRepository _repository;
        public IndexModel(IFileRepository repository) => _repository = repository;

        public FileSpec Spec { get; set; }
        public IPaginatedResult FileList { get; private set; }
        public static SelectList Counties => new SelectList(Data.Counties, "Id", "Name");
        public bool ShowResults { get; private set; }

        public void OnGet()
        {
            // Method intentionally left empty.
        }

        public async Task<IActionResult> OnGetSearchAsync(FileSpec spec, [FromQuery] int p = 1)
        {
            FileList = await _repository.GetFileListAsync(spec, p, GlobalConstants.PageSize);
            Spec = spec;
            ShowResults = true;
            return Page();
        }
    }
}