using FMS.Domain.Services;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FMS.Pages.Account
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly ISelectListHelper _listHelper;
        public IndexModel(IUserService userService, ISelectListHelper listHelper)
        {
            _userService = userService;
            _listHelper = listHelper;
        }

        [BindProperty]
        public Guid Id { get; set; }
        public UserView CurrentUser { get; private set; }
        public IList<string> Roles { get; private set; }

        [BindProperty]
        [Display(Name = "Program")]
        public Guid? UserProgramId { get; set; }

        [BindProperty]
        [Display(Name = "Unit")]
        public Guid? UserUnitId { get; set; }
        
        [BindProperty]
        [Display(Name = "Position")]
        public Guid? UserPositionId { get; set; }

        public SelectList UserUnits { get; private set; } 

        public SelectList UserPrograms { get; private set; }

        public SelectList UserPositions { get; private set; }

        public DisplayMessage Message { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id != null)
            {
                CurrentUser = await _userService.GetUserByIdAsync(id.Value)
                 ?? throw new Exception("Current user not found");
                Id = CurrentUser.Id;
            }
            else
            {
                CurrentUser = await _userService.GetCurrentUserAsync()
                    ?? throw new Exception("Current user not found");
                Id = CurrentUser.Id;
            }
            Roles = await _userService.GetCurrentUserRolesAsync();
            UserProgramId = CurrentUser.UserProgram?.Id;
            UserUnitId = CurrentUser.UserUnit?.Id;
            UserPositionId = CurrentUser.UserPosition?.Id;
            await PopulateSelectsAsync();
            Message = TempData?.GetDisplayMessage();
            return Page();
        }

        public async Task<ActionResult> OnPostAsync()
        {
            if (Id != null)
            {
                CurrentUser = await _userService.GetUserByIdAsync(Id)
                 ?? throw new Exception("Current user not found");
            }
            else
            {
                TempData?.SetDisplayMessage(Context.Danger, $"Unable to find user with Id: {Id.ToString()}");
                await PopulateSelectsAsync();
                return Page();
            }
           
            Roles = await _userService.GetCurrentUserRolesAsync();
            try
            {
                await _userService.UpdateUserDesignationsAsync(CurrentUser.Id, 
                    UserProgramId,
                    UserUnitId,
                    UserPositionId);
            }
            catch (Exception ex)
            {
                TempData?.SetDisplayMessage(Context.Danger, $"Unable to update user designations: {ex.Message}");
                await PopulateSelectsAsync();
                return Page();
            }

            TempData?.SetDisplayMessage(Context.Success, $"User designations successfully updated.");
            return RedirectToPage();
        }   

        private async Task PopulateSelectsAsync()
        {
            UserPrograms = await _listHelper.UserProgramsSelectListAsync();
            UserUnits = await _listHelper.OrganizationalUnitsSelectListAsync();
            UserPositions = await _listHelper.UserPositionsSelectListAsync();
            // false, ["Remedial Sites 1", "Remedial Sites 2", "Remedial Sites 3", "DOD Facilities", "NPL Unit", "Treatment & Storage", "SW Env. Monitoring Compliance", "Voluntary Remediation"]
        }
    }
}