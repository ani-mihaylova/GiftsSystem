namespace GiftsSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
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
        [Authorize]
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

        [HttpGet]
        [Authorize]
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
            newModel.ParentCategories = categoriesSelection;

            //ViewBag.CategoriesForDr = categoriesSelection;
            return this.View(newModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCategoryViewModel categoryModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(categoryModel);
            }

            var parentCategory = this.data.Categories.All()
                .FirstOrDefault(c => c.ID.ToString() == categoryModel.ParentCategoryId);

            var newCategory = Mapper.Map<Category>(categoryModel);
            newCategory.ParentCategory = parentCategory;
            newCategory.CreatedOn = DateTime.Now;

            if (categoryModel.UploadedImage != null)
            {
                using (var memory = new MemoryStream())
                {
                    categoryModel.UploadedImage.InputStream.CopyTo(memory);
                    var content = memory.GetBuffer();

                    newCategory.Image = new Image
                    {
                        Content = content,
                        FileExtension = categoryModel.UploadedImage.FileName.Split(new[] { '.' }).Last()
                    };
                }
            }

            this.data.Categories.Add(newCategory);
            this.data.SaveChanges();

            return this.Redirect("/");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(string name)
        {
            if (name == null)
            {
                return this.Redirect("/");
            }

            var categoryToEdit = this.data.Categories.All().Where(c => c.Name == name)
                .Project().To<EditCategoryViewModel>().FirstOrDefault();

            return this.View("Edit", categoryToEdit);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var categoryForUpdate = this.data.Categories.All().FirstOrDefault(c => c.ID == model.ID);
                categoryForUpdate.Name = model.Name;
                categoryForUpdate.Description = model.Description;
                
                //this.data.Categories.UpdateValues(c => new
                //{
                //    ID = model.ID,
                //    Name = model.Name,
                //    Description = model.Description,
                //    Products =model.Products
                //});
                this.data.Categories.Update(categoryForUpdate);
                this.data.SaveChanges();
                return this.RedirectToAction("Details", new { id = model.ID });
            }

            return this.RedirectToAction("Details", new { id = model.ID });
        }

        public ActionResult Image(int id)
        {
            var image = this.data.Images.GetById(id);
            if (image == null)
            {
                throw new HttpException(404, "Image not found");
            }

            return File(image.Content, "image/" + image.FileExtension);
        }

    }
}