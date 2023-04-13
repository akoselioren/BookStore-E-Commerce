using BookStore.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookStore.ViewComponents
{
    public class RightNBookList : ViewComponent
    {
        private readonly BookStoreDbContext _Dbcontext;

        public RightNBookList(BookStoreDbContext dbcontext)
        {
            _Dbcontext = dbcontext;
        }
        public IViewComponentResult Invoke()
        {
            var nlist = _Dbcontext.Books.ToList();
            return View(nlist);
        }
    }
}
