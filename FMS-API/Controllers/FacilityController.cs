using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FMS_API.Models;
//****************************************************************************
// This is here as an example ... not to go into finished project
//****************************************************************************
namespace FMS_API.Controllers
{
    [Route("api/Facility")]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        private readonly FacilityContext _context;

        public object Facility { get; private set; }

        public FacilityController(FacilityContext context)
        {
            _context = context;
        }

        // GET: api/Facility
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Facility>>> GetFacility()
        {
            return await _context.FacilityItems.ToListAsync();
        }

        // GET: api/Facility/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Facility>> GetFacility(long id)
        {
            var facility = await _context.FacilityItems.FindAsync(id);

            if (facility == null)
            {
                return NotFound();
            }

            return facility;
        }

        // PUT: api/Facility/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFacility(long id, Facility facility)
        {
            if (id != facility.FacID)
            {
                return BadRequest();
            }

            _context.Entry(facility).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!Models.Facility.FacID(id))
                //{
                //    return base.NotFound();
                //}
                //else
                //{
                    throw;
                //}
            }

            return NoContent();
        }

        // POST: api/Facility
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Facility>> PostFacility(Facility facility)
        {
            _context.FacilityItems.Add(facility);
            await _context.SaveChangesAsync();

            //return base.CreatedAtAction("GetFacility", new { id = Models.Facility.FacID }, facility);
            return CreatedAtAction(nameof(GetFacility), new { id = facility.FacID }, facility);
        }

        // DELETE: api/Facility/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Facility>> DeleteFacility(long id)
        {
            var facility = await _context.FacilityItems.FindAsync(id);
            if (facility == null)
            {
                return NotFound();
            }

            _context.FacilityItems.Remove(facility);
            await _context.SaveChangesAsync();

            return facility;
        }

        private bool FacilityExists(Guid id)
        {
            return _context.FacilityItems.Any(e => e.Id == id);
        }
    }
}
