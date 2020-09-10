using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Cabinets
{
    // TODO #38: Add authorize attribute in production 
    public class EditModel : PageModel
    {
        [BindProperty]
        public CabinetEditDto CabinetEdit { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public string OriginalCabinetName { get; set; }

        private readonly ICabinetRepository _repository;
        public EditModel(ICabinetRepository repository) => _repository = repository;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            CabinetEdit = new CabinetEditDto(await _repository.GetCabinetAsync(id.Value));

            if (CabinetEdit == null)
            {
                return NotFound();
            }

            OriginalCabinetName = CabinetEdit.Name;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (await _repository.CabinetNameExistsAsync(CabinetEdit.Name, Id))
            {
                ModelState.AddModelError("CabinetEdit.Name", "There is already a Cabinet with that name.");
                OriginalCabinetName = (await _repository.GetCabinetAsync(Id)).Name;
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
                else
                {
                    throw;
                }
            }

            TempData?.SetDisplayMessage(Context.Success, "Cabinet successfully updated.");
            return RedirectToPage("./Details", new { id = CabinetEdit.Name });
        }
    }
}
