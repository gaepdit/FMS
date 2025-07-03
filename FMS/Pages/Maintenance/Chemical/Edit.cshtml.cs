using System;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.Chemical
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class EditModel : PageModel
    {
        private readonly IChemicalRepository _repository;
        public EditModel(IChemicalRepository repository) => _repository = repository;

        [BindProperty]
        public ChemicalEditDto Chemical { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            Chemical = await _repository.GetChemicalByIdAsync(id.Value);

            if (Chemical == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Chemical.TrimAll();

            // If editing Chemical, make sure the new Chemical doesn't already exist before trying to save.
            if (await _repository.ChemicalCasNoExistsAsync(Chemical.CasNo, Id))
            {
                ModelState.AddModelError("Chemical.CasNo", "CasNo entered already exists.");
            }

            if (await _repository.ChemicalChemicalNameExistsAsync(Chemical.ChemicalName, Id))
            {
                ModelState.AddModelError("Chemical.ChemicalName", "Chemical Name entered already exists.");
            }

            if (await _repository.ChemicalCommonNameExistsAsync(Chemical.CommonName, Id))
            {
                ModelState.AddModelError("Chemical.CommonName", "Common Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _repository.UpdateChemicalAsync(Id, Chemical);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.ChemicalExistsAsync(Id))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success, $"{MaintenanceOptions.Chemical} \"{Chemical.ChemicalName}\" successfully updated.");
            return RedirectToPage("./Index");
        }
    }
}