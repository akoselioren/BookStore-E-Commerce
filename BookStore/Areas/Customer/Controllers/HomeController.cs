using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookStoreDbContext _DbContext;

        public HomeController(ILogger<HomeController> logger, BookStoreDbContext dbContext)
        {
            _logger = logger;
            _DbContext = dbContext;
        }

        public IActionResult Index()
        {
            var books = _DbContext.Books.Where(book => (bool)book.IsActive).ToList();
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim!=null)
            {
                var count = _DbContext.ShoppingCarts.Where(x => x.ApplicationUserId == claim.Value).ToList().Count();
                HttpContext.Session.SetInt32(UserRoles.SessionShoppingCart, count);
            }
            return View(books);
        }
        public IActionResult Details(int id)
        {
            var books = _DbContext.Books.FirstOrDefault(book => book.Id == id);
            ShoppingCart cart = new ShoppingCart()
            {
                Book = books,
                BookId = books.Id

            };
            return View(cart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            shoppingCart.Id = 0;
            if (ModelState.IsValid)
            {
                var claimsIdentity=(ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                shoppingCart.ApplicationUserId = claim.Value;
                ShoppingCart cart = _DbContext.ShoppingCarts.FirstOrDefault(x => x.ApplicationUserId == shoppingCart.ApplicationUserId && x.BookId == shoppingCart.BookId);
                if (cart==null)
                {
                    _DbContext.ShoppingCarts.Add(shoppingCart);
                }
                else
                {
                    cart.Count += shoppingCart.Count;
                }
                _DbContext.SaveChanges();
                var count = _DbContext.ShoppingCarts.Where(x => x.ApplicationUserId == shoppingCart.ApplicationUserId).ToList().Count();
                HttpContext.Session.SetInt32(UserRoles.SessionShoppingCart, count);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var books = _DbContext.Books.FirstOrDefault(book => book.Id == shoppingCart.Id);
                ShoppingCart cart = new ShoppingCart()
                {
                    Book = books,
                    BookId = books.Id

                };
            }
            
            return View(shoppingCart);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
