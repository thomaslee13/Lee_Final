using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lee_Final.Data;
using Lee_Final.Models;
using Microsoft.AspNetCore.Authorization;

namespace Lee_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentSchedulesController : ControllerBase
    {
        private readonly SchoolofhardknocksContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;

        public StudentSchedulesController(SchoolofhardknocksContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        // GET: api/StudentSchedules
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentSchedule>>> GetStudentSchedules()
        {
          if (_context.StudentSchedules == null)
          {
              return NotFound();
          }
            return await _context.StudentSchedules.ToListAsync();
        }

        // GET: api/StudentSchedules/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentSchedule>> GetStudentSchedule(string id)
        {
          if (_context.StudentSchedules == null)
          {
              return NotFound();
          }
            var studentSchedule = await _context.StudentSchedules.FindAsync(id);

            if (studentSchedule == null)
            {
                return NotFound();
            }

            return studentSchedule;
        }

        // PUT: api/StudentSchedules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentSchedule(string id, StudentSchedule studentSchedule)
        {
            if (id != studentSchedule.ScheduleId)
            {
                return BadRequest();
            }

            _context.Entry(studentSchedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentScheduleExists(id))
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

        // POST: api/StudentSchedules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<StudentSchedule>> PostStudentSchedule(StudentSchedule studentSchedule)
        {
          if (_context.StudentSchedules == null)
          {
              return Problem("Entity set 'SchoolofhardknocksContext.StudentSchedules'  is null.");
          }
            _context.StudentSchedules.Add(studentSchedule);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StudentScheduleExists(studentSchedule.ScheduleId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStudentSchedule", new { id = studentSchedule.ScheduleId }, studentSchedule);
        }

        // DELETE: api/StudentSchedules/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentSchedule(string id)
        {
            if (_context.StudentSchedules == null)
            {
                return NotFound();
            }
            var studentSchedule = await _context.StudentSchedules.FindAsync(id);
            if (studentSchedule == null)
            {
                return NotFound();
            }

            _context.StudentSchedules.Remove(studentSchedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentScheduleExists(string id)
        {
            return (_context.StudentSchedules?.Any(e => e.ScheduleId == id)).GetValueOrDefault();
        }
        //[AllowAnonymous]
        //[HttpPost("Authorize")]
        //public IActionResult AuthUser([FromBody] User3 usr)
        //{
        //    var token = jwtAuthenticationManager.Authenticat(usr.username, usr.password);

        //    if (token == null)
        //    {
        //        return Unauthorized();
        //    }
        //    return Ok(token);

        //}

        //public class User3
        //{
        //    public string username { get; set; }
        //    public string password { get; set; }
        //}
    }
}
