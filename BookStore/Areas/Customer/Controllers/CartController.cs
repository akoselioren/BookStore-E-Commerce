using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;

namespace BookStore.Areas.Customer.Controllers
{
    [Area(nameof(Customer))]
    public class CartController : Controller
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<IdentityUser> _userManager;
        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public CartController(BookStoreDbContext dbContext, IEmailSender emailSender, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _emailSender = emailSender;
            _userManager = userManager;
        }
       
            public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ShoppingCartVM = new ShoppingCartVM()
            {
                OrderHeader = new Models.OrderHeader(),
                ListCart = _dbContext.ShoppingCarts.Where(x => x.ApplicationUserId == claim.Value).Include(x => x.Book)
            };
            ShoppingCartVM.OrderHeader.OrderTotal = 0;
            ShoppingCartVM.OrderHeader.ApplicationUser = _dbContext.ApplicationUsers.FirstOrDefault(x => x.Id == claim.Value);
            foreach (var item in ShoppingCartVM.ListCart)
            {
                ShoppingCartVM.OrderHeader.OrderTotal += (item.Count * item.Book.Price);
            }
            return View(ShoppingCartVM);
        }
        [HttpPost]
        [ActionName(nameof(Index))]
        public async Task<IActionResult> IndexPOST()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var user = _dbContext.ApplicationUsers.FirstOrDefault(x => x.Id == claim.Value);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Doğrulama Email'i boş");
            }
            ShoppingCartVM = new ShoppingCartVM();
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = user.Id, code = code },
                protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
            ModelState.AddModelError(string.Empty, "Email Doğrulama kodu gönder.");
            return RedirectToAction("Success");

        }
        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Add(int cartId)
        {
            var cart = _dbContext.ShoppingCarts.FirstOrDefault(x => x.Id == cartId);
            cart.Count += 1;
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Decrease(int cartId)
        {
            var cart = _dbContext.ShoppingCarts.FirstOrDefault(x => x.Id == cartId);
            if (cart.Count == 1)
            {
                var count = _dbContext.ShoppingCarts.Where(y => y.ApplicationUserId == cart.ApplicationUserId).ToList().Count();
                _dbContext.Remove(cart);
                _dbContext.SaveChanges();
                HttpContext.Session.SetInt32(UserRoles.SessionShoppingCart, count - 1);
            }
            else
            {
                cart.Count -= 1;
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(int cartId)
        {
            var cart = _dbContext.ShoppingCarts.FirstOrDefault(x => x.Id == cartId);
                var count = _dbContext.ShoppingCarts.Where(y => y.ApplicationUserId == cart.ApplicationUserId).ToList().Count();
                _dbContext.Remove(cart);
                _dbContext.SaveChanges();
                HttpContext.Session.SetInt32(UserRoles.SessionShoppingCart, count - 1);
            
                cart.Count -= 1;
                _dbContext.SaveChanges();
           
            return RedirectToAction(nameof(Index));
        }
    }
}
