using System;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Cabinets
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class EditModel : PageModel
    {
        [BindProperty]
        public CabinetEditDto CabinetEdit { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public DisplayMessage Message { get; private set; }

        private readonly ICabinetRepository _repository;
        public EditModel(ICabinetRepository repository) => _repository = repository;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            CabinetEdit = new CabinetEditDto(await _repository.GetCabinetSummaryAsync(id.Value));

            if (CabinetEdit == null)
            {
                return NotFound();
            }

            Message = TempData?.GetDisplayMessage();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            CabinetEdit.FirstFileLabel = CabinetEdit.FirstFileLabel?.Trim();

            if (!Domain.Entities.File.IsValidFileLabelFormat(CabinetEdit.FirstFileLabel))
            {
                ModelState.AddModelError("CabinetEdit.FirstFileLabel", "The File Label is invalid.");
                return Page();
            }

            try
            {
                await _repository.UpdateCabinetAsync(Id, CabinetEdit);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.CabinetExistsAsync(Id))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success, "Cabinet successfully updated.");
            return RedirectToPage("./Details", new {id = CabinetEdit.CabinetNumber});
        }
    }
}