﻿using System;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Facilities
{
    [Authorize(Roles = UserRoles.FileEditor)]
    public class UndeleteModel : PageModel
    {
        [BindProperty]
        [HiddenInput]
        public Guid Id { get; set; }

        public FacilityDetailDto FacilityDetail { get; set; }

        private readonly IFacilityRepository _repository;
        public UndeleteModel(IFacilityRepository repository) => _repository = repository;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FacilityDetail = await _repository.GetFacilityAsync(id.Value);

            if (FacilityDetail == null)
            {
                return NotFound();
            }

            Id = id.Value;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _repository.UndeleteFacilityAsync(Id);
            TempData?.SetDisplayMessage(Context.Success, "Facility successfully restored.");
            return RedirectToPage("./Details", new {id = Id});
        }
    }
}