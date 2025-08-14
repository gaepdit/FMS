using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.HsrpFacilityProperties
{
    [Authorize(Roles = UserRoles.FileEditor)]
    public class EditModel : PageModel
    {
        private readonly IHsrpFacilityPropertiesRepository _repository;

        public EditModel(IHsrpFacilityPropertiesRepository repository)
        {
            _repository = repository;
        }


        public void OnGet()
        {
        }
    }
}
