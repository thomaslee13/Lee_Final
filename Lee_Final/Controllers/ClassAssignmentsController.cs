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
    public class ClassAssignmentsController : ControllerBase
    {
        private readonly SchoolofhardknocksContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;

        public ClassAssignmentsController(SchoolofhardknocksContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        // GET: api/ClassAssignments
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassAssignment>>> GetClassAssignments()
        {
          if (_context.ClassAssignments == null)
          {
              return NotFound();
          }
            return await _context.ClassAssignments.ToListAsync();
        }

        // GET: api/ClassAssignments/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassAssignment>> GetClassAssignment(string id)
        {
          if (_context.ClassAssignments == null)
          {
              return NotFound();
          }
            var classAssignment = await _context.ClassAssignments.FindAsync(id);

            if (classAssignment == null)
            {
                return NotFound();
            }

            return classAssignment;
        }

        // PUT: api/ClassAssignments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClassAssignment(string id, ClassAssignment classAssignment)
        {
            if (id != classAssignment.ClassAssignmentId)
            {
                return BadRequest();
            }

            _context.Entry(classAssignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassAssignmentExists(id))
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

        // POST: api/ClassAssignments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ClassAssignment>> PostClassAssignment(ClassAssignment classAssignment)
        {
          if (_context.ClassAssignments == null)
          {
              return Problem("Entity set 'SchoolofhardknocksContext.ClassAssignments'  is null.");
          }
            _context.ClassAssignments.Add(classAssignment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClassAssignmentExists(classAssignment.ClassAssignmentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetClassAssignment", new { id = classAssignment.ClassAssignmentId }, classAssignment);
        }

        // DELETE: api/ClassAssignments/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassAssignment(string id)
        {
            if (_context.ClassAssignments == null)
            {
                return NotFound();
            }
            var classAssignment = await _context.ClassAssignments.FindAsync(id);
            if (classAssignment == null)
            {
                return NotFound();
            }

            _context.ClassAssignments.Remove(classAssignment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassAssignmentExists(string id)
        {
            return (_context.ClassAssignments?.Any(e => e.ClassAssignmentId == id)).GetValueOrDefault();
        }

        //[AllowAnonymous]
        //[HttpPost("Authorize")]
        //public IActionResult AuthUser([FromBody] User7 usr)
        //{
        //    var token = jwtAuthenticationManager.Authenticat(usr.username, usr.password);

        //    if (token == null)
        //    {
        //        return Unauthorized();
        //    }
        //    return Ok(token);

        //}

        //public class User7
        //{
        //    public string username { get; set; }
        //    public string password { get; set; }
        //}
    }
}
