using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChassisController : ControllerBase
    {
        private readonly FleetContext _context;

        public ChassisController(FleetContext context)
        {
            _context = context;
        }

        // GET: api/Chassis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chassis>>> GetChasseez()
        {
            return await _context.Chasseez.ToListAsync();
        }

        // GET: api/Chassis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chassis>> GetChassis(int id)
        {
            var chassis = await _context.Chasseez.FindAsync(id);

            if (chassis == null)
            {
                return NotFound();
            }

            return chassis;
        }

        // PUT: api/Chassis/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChassis(int id, Chassis chassis)
        {
            if (id != chassis.Id)
            {
                return BadRequest();
            }

            _context.Entry(chassis).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChassisExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Chassis
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Chassis>> PostChassis(Chassis chassis)
        {
            var chassisCheck = _context.Chasseez.Where(c => c.Number == chassis.Number && c.Series == chassis.Series)
                .FirstOrDefault();
            if (chassisCheck != null)
            {
                if (_context.Vehicles.Where(v => v.ChassisId == chassisCheck.Id).ToList().Count > 0)
                    return BadRequest("This chassis is already vinculated to another car.");
                else
                    return chassisCheck;
            }

            _context.Chasseez.Add(chassis);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChassis", new { id = chassis.Id }, chassis);
        }

        // DELETE: api/Chassis/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Chassis>> DeleteChassis(int id)
        {
            var chassis = await _context.Chasseez.FindAsync(id);
            if (chassis == null)
            {
                return NotFound();
            }

            _context.Chasseez.Remove(chassis);
            await _context.SaveChangesAsync();

            return chassis;
        }

        private bool ChassisExists(int id)
        {
            return _context.Chasseez.Any(e => e.Id == id);
        }
    }
}
