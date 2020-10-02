using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FMS.Pages.Files
{
    [Authorize(Roles = UserConstants.AdminRole)]
    public class EditCabinets : PageModel
    {
        private readonly IFileRepository _repository;
        public EditCabinets(IFileRepository repository) => _repository = repository;

        public FileDetailDto FileDetail { get; private set; }
        public DisplayMessage Message { get; private set; }

        [BindProperty]
        [HiddenInput]
        public Guid FileId { get; set; }

        [BindProperty]
        [Display(Name = "Add a Cabinet")]
        public Guid? CabinetToAdd { get; set; }

        [BindProperty]
        [Display(Name = "Remove a Cabinet")]
        public Guid? CabinetToRemove { get; set; }

        public SelectList CabinetsAvailableForFile { get; private set; }
        public SelectList CabinetsWithFile { get; private set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FileDetail = await _repository.GetFileAsync(id.Value);

            if (FileDetail == null)
            {
                return NotFound();
            }

            FileId = FileDetail.Id;
            await PopulateSelectsAsync();
            Message = TempData?.GetDisplayMessage();
            return Page();
        }

        public async Task<IActionResult> OnPostAddCabinetAsync()
        {
            if (!CabinetToAdd.HasValue)
            {
                ModelState.AddModelError(nameof(CabinetToAdd), "Select a Cabinet.");
            }

            if (!ModelState.IsValid)
            {
                FileDetail = await _repository.GetFileAsync(FileId);

                if (FileDetail == null)
                {
                    return NotFound();
                }

                FileId = FileDetail.Id;
                await PopulateSelectsAsync();
                return Page();
            }

            await _repository.AddCabinetToFileAsync(CabinetToAdd.Value, FileId);

            FileDetail = await _repository.GetFileAsync(FileId);

            if (FileDetail == null)
            {
                return NotFound();
            }

            FileId = FileDetail.Id;
            await PopulateSelectsAsync();
            Message = new DisplayMessage(Context.Success, "Cabinet added to File.");
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveCabinetAsync()
        {
            if (!CabinetToRemove.HasValue)
            {
                ModelState.AddModelError(nameof(CabinetToRemove), "Select a Cabinet.");
            }

            if (!ModelState.IsValid)
            {
                FileDetail = await _repository.GetFileAsync(FileId);

                if (FileDetail == null)
                {
                    return NotFound();
                }

                FileId = FileDetail.Id;
                await PopulateSelectsAsync();
                return Page();
            }

            await _repository.RemoveCabinetFromFileAsync(CabinetToRemove.Value, FileId);

            FileDetail = await _repository.GetFileAsync(FileId);

            if (FileDetail == null)
            {
                return NotFound();
            }

            FileId = FileDetail.Id;
            await PopulateSelectsAsync();
            Message = new DisplayMessage(Context.Success, "Cabinet removed from File.");
            return Page();
        }

        private async Task PopulateSelectsAsync()
        {
            CabinetsWithFile = new SelectList(await _repository.GetCabinetsForFileAsync(FileId),
                nameof(CabinetSummaryDto.Id), nameof(CabinetSummaryDto.Name));
            CabinetsAvailableForFile = new SelectList(await _repository.GetCabinetsAvailableForFileAsync(FileId),
                nameof(CabinetSummaryDto.Id), nameof(CabinetSummaryDto.Name));
        }
    }
}