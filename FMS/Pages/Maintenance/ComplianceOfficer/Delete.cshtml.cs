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

namespace FMS.Pages.Maintenance.ComplianceOfficer
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class DeleteModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IComplianceOfficerRepository _complianceOfficerRepository;

        public DeleteModel(IUserService userService,
            IComplianceOfficerRepository complianceOfficerRepository)
        {
            _userService = userService;
            _complianceOfficerRepository = complianceOfficerRepository;
        }

        [BindProperty]
        public bool Add { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public string DisplayName { get; private set; }
        public string Email { get; private set; }

        [BindProperty]
        public bool IsCO { get; set; }

        [BindProperty]
        public bool IsUser { get; set; }

        public DisplayMessage Message { get; private set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // see if id is coming in as a Guid from Users table or ComplianceOfficer table
            var appUser = await _userService.GetUserByIdAsync(id.Value);

            var complianceOfficer = await _complianceOfficerRepository.GetComplianceOfficerAsync(id.Value);

            if (appUser == null && complianceOfficer == null)
            {
                return NotFound();
            }

            Id = id.Value;

            if (appUser != null)
            {
                DisplayName = appUser.DisplayName;
                Email = appUser.Email;
                IsUser = true;

                // if IsUser is true, then see if the user is already in the Compliance Officer
                // table, by using the User's full Name
                complianceOfficer = await _complianceOfficerRepository
                    .GetComplianceOfficerAsync(appUser.Email);

                if (complianceOfficer != null)
                {
                    IsCO = true;
                }
            }
            else // complianceOfficer != null
            {
                Add = complianceOfficer.Active;
                DisplayName = complianceOfficer.DisplayName;
                Email = complianceOfficer.Email;
                IsCO = true;

                // if IsCO is true, then see if the user is already an FMS User, by using the User's full Name
                appUser = await _userService.GetUserAsync(complianceOfficer.Email);

                if (appUser != null)
                {
                    IsUser = true;
                }
            }

            if (IsCO && IsUser)
            {
                if (complianceOfficer!.Active)
                {
                    TempData?.SetDisplayMessage(Context.Success,
                        $"User {appUser?.DisplayName} is a current User and a Compliance Officer. You may change whether User is an Active Compliance Officer or not.");
                }
                else
                {
                    TempData?.SetDisplayMessage(Context.Primary,
                        $"User {appUser?.DisplayName} is a current User and a Compliance Officer, but is Not currently an active Compliance officer");
                }

                Add = complianceOfficer.Active;
            }
            else
            {
                if (IsCO && !IsUser)
                {
                    TempData?.SetDisplayMessage(Context.Danger,
                        $"User {complianceOfficer?.Name} is in the Compliance Officer List, but has not logged in to FMS. Please have User log in to FMS before setting them as an active Compliance Officer");
                }

                if (IsUser && !IsCO)
                {
                    // if user has a value and co does not, the user exists in FMS but not a CO
                    TempData?.SetDisplayMessage(Context.Info,
                        $"User {appUser?.DisplayName} is not currently a Compliance Officer. You may add them to the Compliance Officer list.");
                    Add = true;
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
                    if (await _complianceOfficerRepository.ComplianceOfficerIdExistsAsync(Id))
                    {
                        await _complianceOfficerRepository.UpdateComplianceOfficerStatusAsync(Id, Add);
                    }
                    else
                    {
                        var appUser = await _userService.GetUserByIdAsync(Id);
                        var complianceOfficer =
                            await _complianceOfficerRepository
                                .GetComplianceOfficerAsync(appUser.Email);
                        if (complianceOfficer != null)
                        {
                            await _complianceOfficerRepository
                                .UpdateComplianceOfficerStatusAsync(complianceOfficer.Id, Add);
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _complianceOfficerRepository.ComplianceOfficerIdExistsAsync(Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
            }
            else
            {
                if (IsUser && !IsCO)
                {
                    // ADD User to CO list and make active
                    try
                    {
                        var appUser = await _userService.GetUserByIdAsync(Id);

                        var newComplianceOfficer = new ComplianceOfficerCreateDto
                        {
                            FamilyName = appUser.FamilyName,
                            GivenName = appUser.GivenName,
                            Email = appUser.Email,
                        };

                        Id = await _complianceOfficerRepository.CreateComplianceOfficerAsync(newComplianceOfficer);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!await _complianceOfficerRepository.ComplianceOfficerIdExistsAsync(Id))
                        {
                            return NotFound();
                        }

                        throw;
                    }
                }
            }

            return RedirectToPage("./Delete", Id);
        }
    }
}