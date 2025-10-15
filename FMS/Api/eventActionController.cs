using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace FMS.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class eventActionController : ControllerBase
    {
        //[HttpGet("{id:guid}/staff")]
        //public async Task<JsonResult> GetStaffAsync([FromRoute] Guid id) =>
        //new(await officeService.GetStaffAsListItemsAsync(id));

        //[HttpGet("{id:guid}/all-staff")]
        //public async Task<IActionResult> GetAllStaffAsync([FromRoute] Guid id) =>
        //    await authorization.Succeeded(User, Policies.ActiveUser)
        //        ? new JsonResult(await officeService.GetStaffAsListItemsAsync(id, includeInactive: true))
        //        : Unauthorized();

        //[HttpGet("{id:guid}/staff-for-assignment")]
        //public async Task<IActionResult> GetStaffForAssignmentAsync([FromRoute] Guid id)
        //{
        //    if (!await authorization.Succeeded(User, Policies.ActiveUser)) return Unauthorized();

        //    var office = await officeService.FindAsync(id);

        //    return new JsonResult(await authorization.Succeeded(User, office, new OfficeAssignmentRequirement())
        //        ? await officeService.GetStaffAsListItemsAsync(id)
        //        : null);
        //}
    }
}
