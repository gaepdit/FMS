using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Pages.Files
{
    public class IndexModel : PageModel
    {
        private readonly IFileRepository _repository;
        public IndexModel(IFileRepository repository) => _repository = repository;

        public FileSpec Spec { get; set; }
        public IReadOnlyList<FileDetailDto> Files { get; set; }
        public SelectList Counties => new SelectList(Data.Counties, "Id", "Name");
        public bool ShowResults { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnGetSearchAsync(FileSpec spec)
        {
            Files = await _repository.GetFileListAsync(spec);
            ShowResults = true;
            return Page();
        }
    }
}
