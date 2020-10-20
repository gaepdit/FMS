using System;
using System.Threading.Tasks;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Files
{
    [Authorize(Roles = UserRoles.FileEditor)]
    public class EditModel : PageModel
    {
        [BindProperty]
        public bool Delete { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public string FileLabel { get; set; }

        private readonly IFileRepository _repository;
        public EditModel(IFileRepository repository) => _repository = repository;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _repository.GetFileAsync(id.Value);

            if (file == null)
            {
                return NotFound();
            }

            Id = file.Id;
            FileLabel = file.FileLabel;
            Delete = !file.Active;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                FileLabel = (await _repository.GetFileAsync(Id)).FileLabel;
                return Page();
            }

            if (Delete && await _repository.FileHasActiveFacilities(Id))
            {
                TempData?.SetDisplayMessage(Context.Danger, "File has active Facilities and cannot be deleted.");
                FileLabel = (await _repository.GetFileAsync(Id)).FileLabel;
                return RedirectToPage("./Details", new {id = FileLabel});
            }

            try
            {
                await _repository.UpdateFileAsync(Id, !Delete);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.FileExistsAsync(Id))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success, "File successfully updated.");
            FileLabel = (await _repository.GetFileAsync(Id)).FileLabel;
            return RedirectToPage("./Details", new {id = FileLabel});
        }
    }
}