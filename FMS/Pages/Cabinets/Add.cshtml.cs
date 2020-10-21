using System.Threading.Tasks;
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
        public string NewCabinetName { get; private set; }

        private readonly ICabinetRepository _repository;
        public AddModel(ICabinetRepository repository) => _repository = repository;

        public async Task<IActionResult> OnGetAsync()
        {
            NewCabinetName = await _repository.GetNextCabinetName();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var newCabinetId = await _repository.CreateCabinetAsync();
            TempData?.SetDisplayMessage(Context.Success, 
                "New Cabinet successfully created. Be sure to set the first File Label.");
            return RedirectToPage("./Edit", new {id = newCabinetId});
        }
    }
}