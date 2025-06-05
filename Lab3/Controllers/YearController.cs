using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab3;
using Lab3.Models;

namespace Lab3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YearController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public YearController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Year
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Year>>> GetYears()
        {
            return await _context.Year.ToListAsync();
        }

        // GET: api/Year/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Year>> GetYear(int id)
        {
            var year = await _context.Year.FindAsync(id);

            if (year == null)
            {
                return NotFound();
            }

            return year;
        }

        // POST: api/Year
        [HttpPost]
        public async Task<ActionResult<Year>> CreateYear(Year year)
        {
            _context.Year.Add(year);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetYear), new { id = year.Id }, year);
        }

        // PUT: api/Year/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateYear(int id, Year year)
        {
            if (id != year.Id)
            {
                return BadRequest();
            }

            _context.Entry(year).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YearExists(id))
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

        // DELETE: api/Year/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteYear(int id)
        {
            var year = await _context.Year.FindAsync(id);
            if (year == null)
            {
                return NotFound();
            }

            _context.Year.Remove(year);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool YearExists(int id)
        {
            return _context.Year.Any(e => e.Id == id);
        }
    }
}
