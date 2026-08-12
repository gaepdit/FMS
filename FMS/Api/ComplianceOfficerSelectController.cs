using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FMS.Api
{
    [Authorize]
    [ApiController]
    [Route("api/compliance-officers")]
    [Produces("application/json")]
    public class ComplianceOfficerSelectController(
        ISelectListHelper _listHelper,
        IComplianceOfficerRepository _complianceOfficerRepository) : ControllerBase
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetComplianceOfficersAsync([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid unit ID.");
            }
            var complianceOfficers = await _complianceOfficerRepository.GetComplianceOfficerListByUnitAsync(id);
            var selectList = await _listHelper.ComplianceOfficersSelectListAsync(false, complianceOfficers);
            if(selectList == null || !selectList.Any())
            {
                selectList = await _listHelper.ComplianceOfficersSelectListAsync(false);
            }
            return new JsonResult(selectList);
        }
    }
}
