using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace BookStore.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly BookStoreDbContext _dbContext;
        [BindProperty]
        public OrderDetailsVM ODVM { get; set; }
        public OrderController(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            IEnumerable<OrderHeader> orderHeadersList;
            if (User.IsInRole(UserRoles.Role_Admin))
            {
                orderHeadersList = _dbContext.OrderHeaders.ToList();
            }
            else
            {
                orderHeadersList = _dbContext.OrderHeaders.Where(x => x.ApplicationUserId == claim.Value).Include(x => x.ApplicationUser);
            }
            return View(orderHeadersList);
        }
        public IActionResult Pending()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            IEnumerable<OrderHeader> orderHeadersList;
            if (User.IsInRole(UserRoles.Role_Admin))
            {
                orderHeadersList = _dbContext.OrderHeaders.Where(x => x.OrderStatus == OrderStatus.StatusPending);
            }
            else
            {
                orderHeadersList = _dbContext.OrderHeaders.Where(x => x.ApplicationUserId == claim.Value && x.OrderStatus == OrderStatus.StatusPending).Include(x => x.ApplicationUser);
            }
            return View(orderHeadersList);
        }

        public IActionResult Confirmed()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            IEnumerable<OrderHeader> orderHeadersList;
            if (User.IsInRole(UserRoles.Role_Admin))
            {
                orderHeadersList = _dbContext.OrderHeaders.Where(x => x.OrderStatus == OrderStatus.StatusConfirmed);
            }
            else
            {
                orderHeadersList = _dbContext.OrderHeaders.Where(x => x.ApplicationUserId == claim.Value && x.OrderStatus == OrderStatus.StatusConfirmed).Include(x => x.ApplicationUser);
            }
            return View(orderHeadersList);
        }
        public IActionResult InShipping()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            IEnumerable<OrderHeader> orderHeadersList;
            if (User.IsInRole(UserRoles.Role_Admin))
            {
                orderHeadersList = _dbContext.OrderHeaders.Where(x => x.OrderStatus == OrderStatus.StatusInShipping);
            }
            else
            {
                orderHeadersList = _dbContext.OrderHeaders.Where(x => x.ApplicationUserId == claim.Value && x.OrderStatus == OrderStatus.StatusInShipping).Include(x => x.ApplicationUser);
            }
            return View(orderHeadersList);
        }

        public IActionResult Details(int id)
        {
            ODVM = new OrderDetailsVM
            {
                OrderHeader = _dbContext.OrderHeaders.FirstOrDefault(x => x.Id == id),
                OrderDetails = _dbContext.OrderDetails.Where(x => x.OrderId == id).Include(x => x.Book)

            };
            return View(ODVM);
        }
        [HttpPost]
        [Authorize(Roles =UserRoles.Role_Admin)]
        public IActionResult Conf(int id)
        {
            OrderHeader orderHeader = _dbContext.OrderHeaders.FirstOrDefault(x => x.Id == ODVM.OrderHeader.Id);
            orderHeader.OrderStatus = OrderStatus.StatusConfirmed;
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Ship(int id)
        {
            OrderHeader orderHeader = _dbContext.OrderHeaders.FirstOrDefault(x => x.Id == ODVM.OrderHeader.Id);
            orderHeader.OrderStatus = OrderStatus.StatusInShipping;
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
