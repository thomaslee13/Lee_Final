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
    public class FacultiesController : ControllerBase
    {
        private readonly SchoolofhardknocksContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;

        public FacultiesController(SchoolofhardknocksContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        // GET: api/Faculties
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Faculty>>> GetFaculties()
        {
          if (_context.Faculties == null)
          {
              return NotFound();
          }
            return await _context.Faculties.ToListAsync();
        }

        // GET: api/Faculties/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Faculty>> GetFaculty(string id)
        {
          if (_context.Faculties == null)
          {
              return NotFound();
          }
            var faculty = await _context.Faculties.FindAsync(id);

            if (faculty == null)
            {
                return NotFound();
            }

            return faculty;
        }

        // PUT: api/Faculties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFaculty(string id, Faculty faculty)
        {
            if (id != faculty.FacultyId)
            {
                return BadRequest();
            }

            _context.Entry(faculty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacultyExists(id))
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

        // POST: api/Faculties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Faculty>> PostFaculty(Faculty faculty)
        {
          if (_context.Faculties == null)
          {
              return Problem("Entity set 'SchoolofhardknocksContext.Faculties'  is null.");
          }
            _context.Faculties.Add(faculty);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FacultyExists(faculty.FacultyId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFaculty", new { id = faculty.FacultyId }, faculty);
        }

        // DELETE: api/Faculties/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaculty(string id)
        {
            if (_context.Faculties == null)
            {
                return NotFound();
            }
            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty == null)
            {
                return NotFound();
            }

            _context.Faculties.Remove(faculty);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FacultyExists(string id)
        {
            return (_context.Faculties?.Any(e => e.FacultyId == id)).GetValueOrDefault();
        }
        //[AllowAnonymous]
        //[HttpPost("Authorize")]
        //public IActionResult AuthUser([FromBody] User5 usr)
        //{
        //    var token = jwtAuthenticationManager.Authenticat(usr.username, usr.password);

        //    if (token == null)
        //    {
        //        return Unauthorized();
        //    }
        //    return Ok(token);

        //}

        //public class User5
        //{
        //    public string username { get; set; }
        //    public string password { get; set; }
        //}
    }
}
