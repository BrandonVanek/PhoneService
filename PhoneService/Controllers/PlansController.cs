using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneService.Data;
using PhoneService.DTO;
using PhoneService.Models;

namespace PhoneService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        private readonly PhoneServiceDbContext _context;

        public PlansController(PhoneServiceDbContext context)
        {
            _context = context;
        }

        // GET: api/Plans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plan>>> GetPlans()
        {
          if (_context.Plans == null)
          {
              return NotFound();
          }
            return await _context.Plans.ToListAsync();
        }

        // GET: api/Plans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plan>> GetPlan(int id)
        {
          if (_context.Plans == null)
          {
              return NotFound();
          }
            var plan = await _context.Plans.FindAsync(id);

            if (plan == null)
            {
                return NotFound();
            }
            var users = await _context.Users.Where(u => u.UserPlans.Where(us => us.PlanId == plan.Id).Any()).ToListAsync();

            var planDTO = new PlanDetailsDTO
            {
                Id = plan.Id,
                Name = plan.Name,
                Limit = plan.Limit,
                Price = plan.Price,
                Description = plan.Description,
                Users = users
            };

            return Ok(planDTO);
        }

        // PUT: api/Plans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlan(int id, Plan plan)
        {
            if (id != plan.Id)
            {
                return BadRequest();
            }

            _context.Entry(plan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanExists(id))
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

        // POST: api/Plans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Plan>> PostPlan(PlanDTO planDto)
        {
          if (_context.Plans == null)
          {
              return Problem("Entity set 'PhoneServiceDbContext.Plans'  is null.");
          }
            var userPlans = new List<UserPlan>();
            var plan = new Plan
            {
                Name = planDto.Name,
                Limit = planDto.Limit,
                Price = planDto.Price,
                Description = planDto.Description,
                UserPlans = userPlans
            };
            _context.Plans.Add(plan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlan", new { id = plan.Id }, plan);
        }

        // DELETE: api/Plans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlan(int id)
        {
            if (_context.Plans == null)
            {
                return NotFound();
            }
            var plan = await _context.Plans.FindAsync(id);
            if (plan == null)
            {
                return NotFound();
            }

            _context.Plans.Remove(plan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlanExists(int id)
        {
            return (_context.Plans?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
