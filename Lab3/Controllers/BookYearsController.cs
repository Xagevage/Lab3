using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab3;
using Lab3.Models;

namespace Lab3.Controllers
{
    public class BookYearController : Controller
    {
        private readonly ApplicationContext _context;

        public BookYearController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: BookYear
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.BookYear.Include(b => b.Year).Include(b => b.Book);
            return View(await applicationContext.ToListAsync());
        }

        // GET: BookYear/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookYear = await _context.BookYear
                .Include(b => b.Year)
                .Include(b => b.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookYear == null)
            {
                return NotFound();
            }

            return View(bookYear);
        }

        // GET: BookYear/Create
        public IActionResult Create()
        {
            ViewData["YearId"] = new SelectList(_context.Year, "Id", "YearValue");
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title");
            return View();
        }

        // POST: BookYear/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookId,YearId")] BookYear bookYear)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookYear);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["YearId"] = new SelectList(_context.Year, "Id", "YearValue", bookYear.YearId);
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title", bookYear.BookId);
            return View(bookYear);
        }

        // GET: BookYear/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookYear = await _context.BookYear.FindAsync(id);
            if (bookYear == null)
            {
                return NotFound();
            }
            ViewData["YearId"] = new SelectList(_context.Year, "Id", "YearValue", bookYear.YearId);
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title", bookYear.BookId);
            return View(bookYear);
        }

        // POST: BookYear/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,YearId")] BookYear bookYear)
        {
            if (id != bookYear.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookYear);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookYearExists(bookYear.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["YearId"] = new SelectList(_context.Year, "Id", "YearValue", bookYear.YearId);
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title", bookYear.BookId);
            return View(bookYear);
        }

        // GET: BookYear/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookYear = await _context.BookYear
                .Include(b => b.Year)
                .Include(b => b.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookYear == null)
            {
                return NotFound();
            }

            return View(bookYear);
        }

        // POST: BookYear/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookYear = await _context.BookYear.FindAsync(id);
            if (bookYear != null)
            {
                _context.BookYear.Remove(bookYear);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookYearExists(int id)
        {
            return _context.BookYear.Any(e => e.Id == id);
        }
    }
}