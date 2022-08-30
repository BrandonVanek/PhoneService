﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneService.Data;
using PhoneService.Models;

namespace PhoneService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPlansController : ControllerBase
    {
        private readonly PhoneServiceDbContext _context;

        public UserPlansController(PhoneServiceDbContext context)
        {
            _context = context;
        }

        // GET: api/UserPlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPlan>>> GetUserPlans()
        {
          if (_context.UserPlans == null)
          {
              return NotFound();
          }
            return await _context.UserPlans.ToListAsync();
        }

        // GET: api/UserPlans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPlan>> GetUserPlan(int id)
        {
          if (_context.UserPlans == null)
          {
              return NotFound();
          }
            var userPlan = await _context.UserPlans.FindAsync(id);

            if (userPlan == null)
            {
                return NotFound();
            }

            return userPlan;
        }

        // PUT: api/UserPlans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserPlan(int id, UserPlan userPlan)
        {
            if (id != userPlan.Id)
            {
                return BadRequest();
            }

            _context.Entry(userPlan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPlanExists(id))
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

        // POST: api/UserPlans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserPlan>> PostUserPlan(UserPlan userPlan)
        {
          if (_context.UserPlans == null)
          {
              return Problem("Entity set 'PhoneServiceDbContext.UserPlans'  is null.");
          }
            _context.UserPlans.Add(userPlan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserPlan", new { id = userPlan.Id }, userPlan);
        }

        // DELETE: api/UserPlans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserPlan(int id)
        {
            if (_context.UserPlans == null)
            {
                return NotFound();
            }
            var userPlan = await _context.UserPlans.FindAsync(id);
            if (userPlan == null)
            {
                return NotFound();
            }

            _context.UserPlans.Remove(userPlan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserPlanExists(int id)
        {
            return (_context.UserPlans?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
