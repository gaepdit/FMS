using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Pages.Cabinet
{
    public class IndexModel : PageModel
    {
        public IReadOnlyList<CabinetSummaryDto> Cabinets { get; set; }
        public bool ShowInactive { get; set; }
        public DisplayMessage Message { get; set; }

        [BindProperty]
        public CabinetCreateDto CabinetCreate { get; set; }
        [TempData]
        public Guid? NewCabinetId { get; set; }

        private readonly ICabinetRepository _repository;
        public IndexModel(ICabinetRepository repository) => _repository = repository;

        public async Task OnGetAsync()
        {
            Message = TempData?.GetDisplayMessage();
            Cabinets = await _repository.GetCabinetListAsync();
        }

        public async Task OnGetSearchAsync(bool showInactive)
        {
            ShowInactive = showInactive;
            Cabinets = await _repository.GetCabinetListAsync(showInactive);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Cabinets = await _repository.GetCabinetListAsync();
                return Page();
            }

            if (await _repository.CabinetNameExistsAsync(CabinetCreate.Name))
            {
                ModelState.AddModelError("CabinetCreate.Name", "There is already a Cabinet with that name.");
                Cabinets = await _repository.GetCabinetListAsync();
                return Page();
            }

            NewCabinetId = await _repository.CreateCabinetAsync(CabinetCreate);
            TempData?.SetDisplayMessage(Context.Success, $"Cabinet {CabinetCreate.Name} successfully created.");
            return RedirectToPage("Index");
        }
    }
}
