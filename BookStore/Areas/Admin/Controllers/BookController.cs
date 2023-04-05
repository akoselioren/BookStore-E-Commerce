using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Models;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Book
        public async Task<IActionResult> Index()
        {
            var bookStoreDbContext = _context.Books.Include(b => b.Author).Include(b => b.Category);
            return View(await bookStoreDbContext.ToListAsync());
        }

        // GET: Admin/Book/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Admin/Book/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "AuthorName");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: Admin/Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryId,AuthorId,Title,PageCount,PublishDate,Description,ImagePath,ReadCount,Price,IsStock,IsActive")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "AuthorName", book.AuthorId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", book.CategoryId);
            return View(book);
        }

        // GET: Admin/Book/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "AuthorName", book.AuthorId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", book.CategoryId);
            return View(book);
        }

        // POST: Admin/Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,AuthorId,Title,PageCount,PublishDate,Description,ImagePath,ReadCount,Price,IsStock,IsActive")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "AuthorName", book.AuthorId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", book.CategoryId);
            return View(book);
        }

        // GET: Admin/Book/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Admin/Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
