using BookStore.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookStore.ViewComponents
{
    public class LeftCategoryList:ViewComponent
    {
        private readonly BookStoreDbContext _Dbcontext;

        public LeftCategoryList(BookStoreDbContext dbcontext)
        {
            _Dbcontext = dbcontext;
        }
        public IViewComponentResult Invoke()
        {
            var category=_Dbcontext.Categories.ToList();
            return View(category);
        }
    }
}
