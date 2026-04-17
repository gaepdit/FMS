using FMS.Infrastructure.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FMS.Api
{
    [Authorize]
    [ApiController]
    [Route("api/chem")]
    public class ChemicalSearchController(FmsDbContext _context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Ok(new List<string>()); // Return empty if no query
            }

            var results = new JsonResult(await _context.Chemicals.AsNoTracking()
                                        .Where(e => e.DisplayName.ToString().Contains(query))
                                        .ToListAsync());

            return Ok(results);
        }
    }
}
