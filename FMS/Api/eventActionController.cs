using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAllowedActionsAsync([FromRoute] Guid id)
        {
            var newEvent = new JsonResult(await _listHelper.AllowedActionTakenSelectListAsync(id));
            return Ok(newEvent);
        }
            
    }
}

