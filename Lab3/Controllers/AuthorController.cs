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
    public class AuthorController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public AuthorController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Author
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            return await _context.Author.ToListAsync();
        }

        // GET: api/Author/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _context.Author.FindAsync(id);

            if (author == null)
            {
                return NotFound(new { message = "Author not found" });
            }

            return author;
        }

        // POST: api/Author
        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthor([FromBody] Author author)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        // PUT: api/Author/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] Author author)
        {
            if (id != author.Id)
            {
                return BadRequest(new { message = "ID in URL does not match ID in body" });
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Author.Any(e => e.Id == id))
                {
                    return NotFound(new { message = "Author not found" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Author/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Author.FindAsync(id);
            if (author == null)
            {
                return NotFound(new { message = "Author not found" });
            }

            _context.Author.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
