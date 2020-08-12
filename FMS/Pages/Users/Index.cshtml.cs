using FMS.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public string Name { get; set; }
        [UIHint("EmailAddress")]
        public string Email { get; set; }

        public List<UserSearchResult> SearchResults { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var users = await _userService.GetUsersAsync(Name, Email);

            SearchResults = users.Select(e =>
                new UserSearchResult()
                {
                    Email = e.Email,
                    Name = e.SortableFullName,
                    Id = e.Id
                }).ToList();

            if (SearchResults == null || SearchResults.Count == 0)
                SearchResults = new List<UserSearchResult>
                {
                    new UserSearchResult{ Id = default, Email = "example.one@example.com", Name = "Sample User" },
                    new UserSearchResult{ Id = default, Email = "example.two@example.net", Name = "Another Sample" }
                };

            return Page();
        }

        public class UserSearchResult
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }
    }
}