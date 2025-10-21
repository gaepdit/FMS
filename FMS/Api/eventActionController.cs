using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Domain.Services;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading.Tasks;
using TestSupport.SeedDatabase;

namespace FMS.Api
{
    [Authorize]
    [ApiController]
    [Route("api")]
    [Produces("application/json")]
    public class EventActionController(
        IEventTypeRepository _eventTypeRepository,
        IActionTakenRepository _actionTakenRepository,
        IAllowedActionTakenRepository _allowedActionTakenRepository) : ControllerBase
    {

        [HttpGet("eventType")]
        public async Task<JsonResult> GetEventTypesAsync() =>
            new JsonResult(await _eventTypeRepository.GetEventTypeListAsync());

        [HttpGet("all-actions")]
        public async Task<IActionResult> GetAllActionsAsync() =>
            new JsonResult(await _actionTakenRepository.GetActionTakenListAsync());

        [HttpGet("{id:guid}/all-allowed-actions")]
        public async Task<IActionResult> GetStaffForAssignmentAsync([FromRoute] Guid id) =>
            new JsonResult(await _allowedActionTakenRepository.GetAllowedActionTakenListAsync(id));
    }
}

