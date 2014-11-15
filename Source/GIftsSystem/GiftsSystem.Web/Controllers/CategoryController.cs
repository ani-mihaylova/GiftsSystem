﻿namespace GiftsSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using GiftsSystem.Data;
    using GiftsSystem.Models;
    using GiftsSystem.Web.ViewModels.Category;

    public class CategoryController : BaseController
    {
        public CategoryController(IGiftsSystemData data)
            : base(data)
        {

        }
        // GET: Category
        public ActionResult Index()
        {
            return View("/");
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return Redirect("/");
            }

            var currentCategory = this.data.Categories.All().Where(c => c.ID == id)
                .Project().To<DetailsCategoryViewModel>().FirstOrDefault();

            return View(currentCategory);
        }

        [Authorize(Roles = "Company")]
        [HttpGet]        
        public ActionResult Create()
        {
            var newModel = new CreateCategoryViewModel();
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


        [Authorize(Roles = "Company")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCategoryViewModel newCategory)
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
                ParentCategoryID = parentCategory,
                Products=new List<Product>()
            };

            this.data.Categories.Add(category);
            this.data.SaveChanges();

            return this.Redirect("/");
        }

        [Authorize(Roles="Company")]
        [HttpGet]
        public ActionResult Edit(string name)
        {
            if (name==null)
            {
                return this.Redirect("/");
            }

            var categoryToEdit = this.data.Categories.All().Where(c => c.Name == name)
                .Project().To<EditCategoryViewModel>().FirstOrDefault();

            return this.View("Edit", categoryToEdit);
        }

        //[HttpPost]
        //public ActionResult Edit(EditCategoryViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        this.data.Categories.UpdateValues({

        //    });
        //        this.data.SaveChanges();
        //        var categoryId=this.data.Categories.GetById(model.ID);

        //        return this.RedirectToAction("Details", new { id = categoryId });
        //    }

        //    return this.View(model);
        //}

    }
}