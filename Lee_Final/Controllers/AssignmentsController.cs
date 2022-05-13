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
    public class AssignmentsController : ControllerBase
    {
        private readonly SchoolofhardknocksContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;

        public AssignmentsController(SchoolofhardknocksContext context,JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        // GET: api/Assignments
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignments()
        {
          if (_context.Assignments == null)
          {
              return NotFound();
          }
            return await _context.Assignments.ToListAsync();
        }

        // GET: api/Assignments/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Assignment>> GetAssignment(string id)
        {
          if (_context.Assignments == null)
          {
              return NotFound();
          }
            var assignment = await _context.Assignments.FindAsync(id);

            if (assignment == null)
            {
                return NotFound();
            }

            return assignment;
        }

        // PUT: api/Assignments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssignment(string id, Assignment assignment)
        {
            if (id != assignment.AssignmentId)
            {
                return BadRequest();
            }

            _context.Entry(assignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignmentExists(id))
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

        // POST: api/Assignments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Assignment>> PostAssignment(Assignment assignment)
        {
          if (_context.Assignments == null)
          {
              return Problem("Entity set 'SchoolofhardknocksContext.Assignments'  is null.");
          }
            _context.Assignments.Add(assignment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AssignmentExists(assignment.AssignmentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAssignment", new { id = assignment.AssignmentId }, assignment);
        }

        // DELETE: api/Assignments/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(string id)
        {
            if (_context.Assignments == null)
            {
                return NotFound();
            }
            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }

            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AssignmentExists(string id)
        {
            return (_context.Assignments?.Any(e => e.AssignmentId == id)).GetValueOrDefault();
        }
        //    [AllowAnonymous]
        //    [HttpPost("Authorize")]
        //    public IActionResult AuthUser([FromBody] User usr)
        //    {
        //        var token = jwtAuthenticationManager.Authenticat(usr.username, usr.password);

        //        if(token == null)
        //        {
        //            return Unauthorized();
        //        }
        //        return Ok(token);     

        //    }

        //    public class User
        //    {
        //        public string username { get; set; }
        //        public string password { get; set; }
        //    }
    }
}
