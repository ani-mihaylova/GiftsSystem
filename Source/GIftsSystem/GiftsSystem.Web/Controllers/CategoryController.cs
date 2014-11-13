using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GiftsSystem.Models;
using AutoMapper.QueryableExtensions;
using GiftsSystem.Web.ViewModels.Category;
using GiftsSystem.Data.Repositories;
using GiftsSystem.Data;

namespace GiftsSystem.Web.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(IGiftsSystemData data)
            : base(data)
        {

        }
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return Redirect("/Home");
            }

            var currentCategory = this.data.Categories.All().Where(c => c.ID == id)
                .Project().To<CategoryDetailsView>().FirstOrDefault();

            return View(currentCategory);
        }

        [HttpGet]
        
        public ActionResult Create()
        {
            var newModel = new CreateCategoryView();
            var categoriesSelection = new List<SelectListItem>();
            foreach (var item in this.data.Categories.All())
            {
                categoriesSelection.Add(new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.ID.ToString()
                });
            }
            ViewBag.CategoriesForDr = categoriesSelection;
            return this.View(newModel);
        }

        //public PartialViewResult GetDropdown()
        //{
        //    var allCategory = this.categories.All().ToList();

        //    return PartialView("_DropdownCategory", allCategory);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCategoryView newCategory)
        {
            if (!ModelState.IsValid)
            {
                return this.View(newCategory);
            }

            var parentCategory = this.data.Categories.GetById(newCategory.ParentCategoryID);
            var category = new Category()
            {
                Name = newCategory.Name,
                Description = newCategory.Description,
                CreatedOn = DateTime.Now,
                ParentCategoryID = parentCategory
            };

            this.data.Categories.Add(category);
            this.data.SaveChanges();

            return this.Redirect("/");
        }
    }
}