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
        [HttpGet("{id:guid?}")]
        public async Task<IActionResult> GetComplianceOfficersAsync([FromRoute] Guid? id)
        {
            // If no UnitId is provided, return the full list of COs.
            if (id == Guid.Empty || id == null)
            {
                return new JsonResult(await _listHelper.ComplianceOfficersSelectListAsync());
            }
            // Get list of CO Guids from the UnitId, then get the select list for those COs. If no COs are found for that Unit, return the full list of COs.
            var complianceOfficers = await _complianceOfficerRepository.GetComplianceOfficerListByUnitAsync((Guid)id);
            var selectList = await _listHelper.ComplianceOfficersSelectListAsync(false, complianceOfficers);

            if(selectList == null || !selectList.Any())
            {
                selectList = await _listHelper.ComplianceOfficersSelectListAsync();
            }
            return new JsonResult(selectList);
        }
    }
}
