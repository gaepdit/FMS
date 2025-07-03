using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.Chemical
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddModel : PageModel
    {
        private readonly IChemicalRepository _repository;
        public AddModel(IChemicalRepository repository) => _repository = repository;

        [BindProperty]
        public ChemicalCreateDto Chemical { get; set; }

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

            Chemical.TrimAll();

            // When adding a new Chemical, make sure the CasNo, Chemical Name and Common Name don't already exist before trying to save.
            if (await _repository.ChemicalCasNoExistsAsync(Chemical.CasNo))
            {
                ModelState.AddModelError("Chemical.CasNo", "CasNo entered already exists.");
            }

            if (await _repository.ChemicalChemicalNameExistsAsync(Chemical.ChemicalName))
            {
                ModelState.AddModelError("Chemical.ChemicalName", "Chemical Name entered already exists.");
            }

            if (Chemical.CommonName != null && await _repository.ChemicalCommonNameExistsAsync(Chemical.CommonName))
            {
                ModelState.AddModelError("Chemical.CommonName", "Common Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.CreateChemicalAsync(Chemical);

            TempData?.SetDisplayMessage(Context.Success, $"{MaintenanceOptions.Chemical} \"{Chemical.ChemicalName}\" successfully created.");
            return RedirectToPage("./Index");
        }
    }
}