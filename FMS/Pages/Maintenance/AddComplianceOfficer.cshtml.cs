using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FMS.Domain.Entities.Users;
using FMS.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddComplianceOfficerModel : PageModel
    {
        private readonly IUserService _userService;
        public AddComplianceOfficerModel(IUserService userService) => _userService = userService;

        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public bool ShowResults { get; set; }
        public List<UsersSearchResult> SearchResults { get; set; }
        public void OnGet()
        {
            // Method intentionally left empty.
        }

        public async Task<IActionResult> OnGetSearchAsync(string name, string email)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var users = await _userService.GetUsersAsync(name, email);

            ShowResults = true;
            SearchResults = users.Select(e =>
                new UsersSearchResult()
                {
                    Email = e.Email,
                    Name = e.SortableFullName,
                    Id = e.Id
                }).ToList();

            return Page();
        }

        public class UsersSearchResult
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }
    }
}
