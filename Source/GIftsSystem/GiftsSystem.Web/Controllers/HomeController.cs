namespace GiftsSystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using GiftsSystem.Data;
    using GiftsSystem.Models;

    public class HomeController : BaseController
    {
        public HomeController(IGiftsSystemData data)
            : base(data)
        {

        }
        public ActionResult Index()
        {
            //TODO:Custom binding
            var all = this.data.Categories.All();
            Dictionary<Category, List<Category>> categoriesWithChilderen = new Dictionary<Category, List<Category>>();
            foreach (var item in all)
            {
                if (item.ParentCategoryID == null)
                {
                    categoriesWithChilderen.Add(item, new List<Category>());
                }
                else
                {                   
                    categoriesWithChilderen[item.ParentCategoryID].Add(item);
                }
            }

            return View(categoriesWithChilderen);
        }
    }
}