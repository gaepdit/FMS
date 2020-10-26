using System;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Admin
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class DetailsComplianceOfficerModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IComplianceOfficerRepository _complianceOfficerRepository;
        public DetailsComplianceOfficerModel(IUserService userService, IComplianceOfficerRepository complianceOfficerRepository)
        {
            _userService = userService;
            _complianceOfficerRepository = complianceOfficerRepository;
        }

        [BindProperty]
        public bool Add { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        [BindProperty]
        public string DisplayName { get; set; }

        public string Email { get; set; }

        [BindProperty]
        public bool IsCO { get; set; }

        [BindProperty]
        public bool IsUser { get; set; }

        public ComplianceOfficerCreateDto NewComplianceOfficer { get; set; }

        public ComplianceOfficerDetailDto ComplianceOfficer { get; set; }

        public ApplicationUser AppUser { get; set; }

        public DisplayMessage Message { get; private set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // see if id is coming in as a Guid from Users table or ComplianceOfficer table
            AppUser = await _userService.GetUserByIdAsync(id.Value);
            ComplianceOfficer = await _complianceOfficerRepository.GetComplianceOfficerAsync(id.Value);

            if (AppUser != null && AppUser.Id != Guid.Empty)
            {
                Id = AppUser.Id;
                DisplayName = AppUser.DisplayName;
                Email = AppUser.Email;
                IsUser = true;
            }
            if (ComplianceOfficer != null)
            {
                Add = ComplianceOfficer.Active;
                Id = ComplianceOfficer.Id;
                DisplayName = ComplianceOfficer.DisplayName;
                IsCO = true;
            }

            // if IsUser is true, then see if the user is already in the Compliance Officer
            // table, by using the User's full Name
            if (IsUser)
            {
                ComplianceOfficer = await _complianceOfficerRepository.GetComplianceOfficerAsync
                    (AppUser.DisplayName);

                if (ComplianceOfficer != null && ComplianceOfficer.Id != Guid.Empty)
                {
                    DisplayName = ComplianceOfficer.Name;
                    Email = AppUser.Email;
                    IsCO = true;
                }
            }
            // if IsCO is true, then see if the user is already an FMS User, by using the User's full Name
            if (IsCO)
            {
                AppUser = await _userService.GetUserAsync(ComplianceOfficer.DisplayName);

                if (AppUser != null && AppUser.Id != Guid.Empty)
                {
                    DisplayName = AppUser.DisplayName;
                    Email = AppUser.Email;
                    IsUser = true;
                }
            }

            if (IsCO && IsUser)
            {
                if(ComplianceOfficer.Active)
                {
                    TempData?.SetDisplayMessage(Context.Success, $"User {AppUser?.DisplayName} is a current User and a Compliance Officer. You may change whether User is an Active Compliance Officer or not.");
                }
                else
                {
                    TempData?.SetDisplayMessage(Context.Primary, $"User {AppUser?.DisplayName} is a current User and a Compliance Officer, but is Not currently an active Compliance officer");
                }
                Add = ComplianceOfficer.Active;
            }
            else
            {
                if (IsCO && !IsUser)
                {
                    TempData?.SetDisplayMessage(Context.Danger, $"User {ComplianceOfficer?.Name} is in the Compliance Officer List, but has not logged in to FMS. Please have User log in to FMS before setting them as an active Compliance Officer");
                }
                if (IsUser && !IsCO)
                {
                    // if user has a value and co does not, the user exists in FMS but not a CO
                    TempData?.SetDisplayMessage(Context.Secondary, $"User {AppUser?.DisplayName} is not currently a Compliance Officer. You may add them to the Compliance Officer list.");
                    Add = true;
                }
                if (!IsUser && !IsCO)
                {
                    // if user has no value and co has no value, then user does NOT exist in FMS
                    TempData?.SetDisplayMessage(Context.Danger, $"You have specified a non-existent user in FMS");
                }
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

            if (IsCO && IsUser)
            {
                // User exists in FMS and in in CO table, so Update the CO Active status
                try
                {
                    await _complianceOfficerRepository.UpdateComplianceOfficerStatusAsync(Id, Add);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _complianceOfficerRepository.ComplianceOfficerIdExistsAsync(Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                if (IsUser && !IsCO)
                {
                    // ADD User to CO list and make active
                    try
                    {
                        AppUser = await _userService.GetUserByIdAsync(Id);

                        NewComplianceOfficer = new ComplianceOfficerCreateDto
                        {
                            Active = true,
                            FamilyName = AppUser.FamilyName,
                            GivenName = AppUser.GivenName
                        };

                        Id = await _complianceOfficerRepository.CreateComplianceOfficerAsync(NewComplianceOfficer);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!await _complianceOfficerRepository.ComplianceOfficerIdExistsAsync(Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }

            return RedirectToPage("./DetailsComplianceOfficer", Id);
        }
    }
}
