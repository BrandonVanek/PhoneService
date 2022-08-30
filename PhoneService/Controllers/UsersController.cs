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
    public class UsersController : ControllerBase
    {
        private readonly PhoneServiceDbContext _context;

        public UsersController(PhoneServiceDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var plans = await _context.Plans.Where(u => u.UserPlans.Where(us => us.UserId == user.Id).Any()).ToListAsync();

            var devices = await _context.Devices.Where(d => d.UserId == user.Id).ToListAsync();

            var phoneNumbers = new List<PhoneNumber>();
            devices.ForEach(i => phoneNumbers.Add(new PhoneNumber { 
                Id = i.Id,
                Number = i.PhoneNumber,
                UserId = i.UserId,
                User = i.User
            }));

            var userDTO = new UserDetailsDTO
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                PhoneNumbers = phoneNumbers,
                Plans = plans,
                Devices = devices
            };

            return Ok(userDTO);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserDTO userDto)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'PhoneServiceDbContext.Users'  is null.");
          }
            var phoneNumbers = new List<PhoneNumber>();
            var userPlans = new List<UserPlan>();
            var devices = new List<Device>();
            var user = new User
            {
                Name = userDto.Name,
                Username = userDto.Username,
                Email = userDto.Email,
                Password = userDto.Password,
                PhoneNumbers = phoneNumbers,
                UserPlans = userPlans,
                Devices = devices

            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
