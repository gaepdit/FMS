using DocumentFormat.OpenXml.Office2010.Excel;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Domain.Services;
using FMS.Infrastructure;
using FMS.Infrastructure.Repositories;
using FMS.Infrastructure.Contexts;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TestSupport.SeedDatabase;
using System.Linq;


namespace FMS.Api
{
    [Authorize]
    [ApiController]
    [Route("api/chem")]
    public class ChemicalSearchController(FmsDbContext _context) : ControllerBase
    {
        [HttpGet("{query:alpha}")]
        public async Task<IActionResult> Get([FromRoute] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Ok(new List<string>()); // Return empty if no query
            }

            var results = new JsonResult(await _context.Chemicals.AsNoTracking()
                                        .Where(e => e.ChemicalName.ToString().Contains(query))
                                        .ToListAsync());

            return Ok(results);
        }
    }
}
