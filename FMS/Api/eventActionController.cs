using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FMS.Api
{
    [Authorize]
    [ApiController]
    [Route("api")]
    [Produces("application/json")]
    public class EventActionController(
        ISelectListHelper _listHelper) : ControllerBase
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAllowedActionsAsync([FromRoute] Guid id) =>
            new JsonResult(await _listHelper.AllowedActionTakenSelectListAsync(id));
    }
}

