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
    public class FacultyClassesController : ControllerBase
    {
        private readonly SchoolofhardknocksContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;

        public FacultyClassesController(SchoolofhardknocksContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        // GET: api/FacultyClasses
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacultyClass>>> GetFacultyClasses()
        {
          if (_context.FacultyClasses == null)
          {
              return NotFound();
          }
            return await _context.FacultyClasses.ToListAsync();
        }

        // GET: api/FacultyClasses/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<FacultyClass>> GetFacultyClass(string id)
        {
          if (_context.FacultyClasses == null)
          {
              return NotFound();
          }
            var facultyClass = await _context.FacultyClasses.FindAsync(id);

            if (facultyClass == null)
            {
                return NotFound();
            }

            return facultyClass;
        }

        // PUT: api/FacultyClasses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFacultyClass(string id, FacultyClass facultyClass)
        {
            if (id != facultyClass.FacultyClassesId)
            {
                return BadRequest();
            }

            _context.Entry(facultyClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacultyClassExists(id))
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

        // POST: api/FacultyClasses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<FacultyClass>> PostFacultyClass(FacultyClass facultyClass)
        {
          if (_context.FacultyClasses == null)
          {
              return Problem("Entity set 'SchoolofhardknocksContext.FacultyClasses'  is null.");
          }
            _context.FacultyClasses.Add(facultyClass);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FacultyClassExists(facultyClass.FacultyClassesId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFacultyClass", new { id = facultyClass.FacultyClassesId }, facultyClass);
        }

        // DELETE: api/FacultyClasses/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacultyClass(string id)
        {
            if (_context.FacultyClasses == null)
            {
                return NotFound();
            }
            var facultyClass = await _context.FacultyClasses.FindAsync(id);
            if (facultyClass == null)
            {
                return NotFound();
            }

            _context.FacultyClasses.Remove(facultyClass);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FacultyClassExists(string id)
        {
            return (_context.FacultyClasses?.Any(e => e.FacultyClassesId == id)).GetValueOrDefault();
        }
        //[AllowAnonymous]
        //[HttpPost("Authorize")]
        //public IActionResult AuthUser([FromBody] User4 usr)
        //{
        //    var token = jwtAuthenticationManager.Authenticat(usr.username, usr.password);

        //    if (token == null)
        //    {
        //        return Unauthorized();
        //    }
        //    return Ok(token);

        //}

        //public class User4
        //{
        //    public string username { get; set; }
        //    public string password { get; set; }
        //}
    }
}
