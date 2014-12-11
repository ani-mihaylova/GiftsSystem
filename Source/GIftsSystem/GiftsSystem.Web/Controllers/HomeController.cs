namespace GiftsSystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using GiftsSystem.Data;
    using GiftsSystem.Models;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using GiftsSystem.Web.ViewModels.Home;
    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    public class HomeController : BaseController
    {
        public HomeController(IGiftsSystemData data)
            : base(data)
        {

        }

        //[OutputCache(Duration = 60 * 60)]
        [HttpGet]
        public ActionResult Index()
        {
            //TODO:Custom binding
            var all = this.data.Categories.All();
            Dictionary<Category, List<Category>> categoriesWithChilderen = new Dictionary<Category, List<Category>>();
            foreach (var item in all)
            {
                if (item.ParentCategory == null)
                {
                    categoriesWithChilderen.Add(item, new List<Category>());
                }
                else
                {
                    categoriesWithChilderen[item.ParentCategory].Add(item);
                }
            }
            return View(categoriesWithChilderen);
        }

        [HttpPost]
        public ActionResult ReadCategory([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.data.Categories
                .All()
                .Where(c => c.ParentCategory == null)
                .Project()
                .To<ListCategoryViewModels>();

            return Json(result.ToDataSourceResult(request));
        }
    }
}