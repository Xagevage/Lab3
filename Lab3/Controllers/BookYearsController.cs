using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookYearController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public BookYearController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/BookYear
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookYear>>> GetBookYears()
        {
            return await _context.BookYear
                .Include(b => b.Book)
                .Include(b => b.Year)
                .ToListAsync();
        }

        // GET: api/BookYear/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookYear>> GetBookYear(int id)
        {
            var bookYear = await _context.BookYear
                .Include(b => b.Book)
                .Include(b => b.Year)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (bookYear == null)
                return NotFound(new { message = "BookYear not found" });

            return bookYear;
        }

        // POST: api/BookYear
        [HttpPost]
        public async Task<ActionResult<BookYear>> CreateBookYear([FromBody] BookYear bookYear)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.BookYear.Add(bookYear);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookYear), new { id = bookYear.Id }, bookYear);
        }

        // PUT: api/BookYear/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookYear(int id, [FromBody] BookYear bookYear)
        {
            if (id != bookYear.Id)
                return BadRequest(new { message = "ID mismatch" });

            _context.Entry(bookYear).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookYearExists(id))
                    return NotFound(new { message = "BookYear not found" });
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/BookYear/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookYear(int id)
        {
            var bookYear = await _context.BookYear.FindAsync(id);
            if (bookYear == null)
                return NotFound(new { message = "BookYear not found" });

            _context.BookYear.Remove(bookYear);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookYearExists(int id)
        {
            return _context.BookYear.Any(e => e.Id == id);
        }
    }
}
