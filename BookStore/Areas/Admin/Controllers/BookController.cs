using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =UserRoles.Role_Admin)]
    public class BookController : Controller
    {
        private readonly BookStoreDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public BookController(BookStoreDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
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
        public async Task<IActionResult> Create([Bind("Id,CategoryId,AuthorId,Title,PageCount,PublishDate,Description,ImagePath,Price,StockCount")] Book book)
        {
            if (!ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count>0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(_environment.WebRootPath, @"img\Books");
                    var ext = Path.GetExtension(files[0].FileName);
                    if (book.ImagePath!=null)
                    {
                        var imagepath = Path.Combine(_environment.WebRootPath, book.ImagePath.TrimStart('\\'));
                        if (System.IO.File.Exists(imagepath))
                        {
                            System.IO.File.Delete(imagepath);
                        }
                    }
                    using (var fileStreams=new FileStream(Path.Combine(uploads, fileName +ext),FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    book.ImagePath=@"\img\Books\" +fileName+ext;
                }
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "AuthorName", book.AuthorId);
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
        public async Task<IActionResult> Edit( Book book)
        {

            if (ModelState.IsValid)
            {
              
                var files = HttpContext.Request.Form.Files;
                if (files.Count>0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(_environment.WebRootPath, @"img\Books");
                    var ext = Path.GetExtension(files[0].FileName);
                    if (book.ImagePath!=null)
                    {
                        var imagepath = Path.Combine(_environment.WebRootPath, book.ImagePath.TrimStart('\\'));
                        if (System.IO.File.Exists(imagepath))
                        {
                            System.IO.File.Delete(imagepath);
                        }
                    }
                    using (var fileStreams=new FileStream(Path.Combine(uploads, fileName +ext),FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    book.ImagePath=@"\img\Books\" +fileName+ext;
                    
                }
                _context.Update(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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

            return PartialView("_DeleteBookPartialView", book);
        }

        // POST: Admin/Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            var imagepath = Path.Combine(_environment.WebRootPath, book.ImagePath);
            if (System.IO.File.Exists(imagepath))
            {
                System.IO.File.Delete(imagepath);
            }
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
