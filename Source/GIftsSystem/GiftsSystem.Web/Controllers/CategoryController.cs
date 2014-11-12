using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GiftsSystem.Data.Common.Repositories;
using GiftsSystem.Models;
using AutoMapper.QueryableExtensions;
using GiftsSystem.Web.ViewModels.Category;

namespace GiftsSystem.Web.Controllers
{
    public class CategoryController : Controller
    {
        private IGenericRepository<Category> categories;

        public CategoryController(IGenericRepository<Category> categories)
        {
            this.categories = categories;
        }

        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id==null)
            {
                return Redirect("/Home");
            }

            var currentCategory=this.categories.All().Where(c=>c.ID==id)
                .Project().To<CategoryDetailsView>().FirstOrDefault();

            return View(currentCategory);
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult Create()
        {
            var newModel = new CreateCategoryView();

            return this.View(newModel);
        }


        //[HttpPost]
        //public ActionResult Create()
        //{

        //}
    }
}