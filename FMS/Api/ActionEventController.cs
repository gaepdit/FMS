using DocumentFormat.OpenXml.Office2010.Excel;
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
    public class ActionEventController(
        ISelectListHelper _listHelper) : ControllerBase
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAllowedEventsAsync([FromRoute] Guid id) =>
            new JsonResult(await _listHelper.AllowedEventsSelectListAsync(id));
    }
}

