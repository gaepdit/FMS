using FMS.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly FmsDbContext _context;

        public IndexModel(FmsDbContext context)
        {
            _context = context;
        }

        public string Name { get; set; }
        [UIHint("EmailAddress")]
        public string Email { get; set; }

        public List<UserSearchResult> SearchResults { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

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