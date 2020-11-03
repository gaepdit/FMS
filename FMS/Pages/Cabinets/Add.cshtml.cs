using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Cabinets
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddModel : PageModel
    {
        [BindProperty]
        public CabinetEditDto NewCabinet { get; set; }

        private readonly ICabinetRepository _repository;
        public AddModel(ICabinetRepository repository) => _repository = repository;

        public void OnGet()
        {
            // Method intentionally left empty.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            NewCabinet.TrimAll();
            
            if (await _repository.CabinetNameExistsAsync(NewCabinet.Name))
            {
                ModelState.AddModelError("CabinetEdit.Name", "There is already a Cabinet with that name.");
            }

            if (!Domain.Entities.File.IsValidFileLabelFormat(NewCabinet.FirstFileLabel))
            {
                ModelState.AddModelError("CabinetEdit.FirstFileLabel", "The File Label is invalid.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.CreateCabinetAsync(NewCabinet);
            
            TempData?.SetDisplayMessage(Context.Success, 
                "Cabinet successfully created. Be sure to check File Label sorting.");
            return RedirectToPage("./Details", new {id = NewCabinet.Name});
        }
    }
}