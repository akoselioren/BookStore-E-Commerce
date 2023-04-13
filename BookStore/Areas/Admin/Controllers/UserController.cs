using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class UserController : Controller
    {
        private readonly BookStoreDbContext _dbContext;

        public UserController(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var users = _dbContext.ApplicationUsers.ToList();
            var role= _dbContext.Roles.ToList();
            var userRole=_dbContext.UserRoles.ToList();
            foreach (var item in users)
            {
                var roleId = userRole.FirstOrDefault(x => x.UserId == item.Id).RoleId;
                item.Role=role.FirstOrDefault(y=>y.Id==roleId).Name;
            }
            return View(users);
        }


        // GET: Admin/Category/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _dbContext.ApplicationUsers
                .FirstOrDefaultAsync(m => m.Id == id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            return PartialView("_DeleteUserPartialView", user);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var users = await _dbContext.ApplicationUsers.FindAsync(id);
            _dbContext.ApplicationUsers.Remove(users);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _dbContext.ApplicationUsers.Any(e => e.Id == id);
        }
    }
}
