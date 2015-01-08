namespace GiftsSystem.Web.Areas.Administration.Controllers
{
    using GiftsSystem.Data;
    using GiftsSystem.Web.Areas.Administration.Controllers.Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Model = GiftsSystem.Models.Product;
    using ViewModel = GiftsSystem.Web.Areas.Administration.ViewModels.Product.EditProductViewModel;
    using Kendo.Mvc.UI;
    using System.Collections;
    using AutoMapper.QueryableExtensions;
   
    public class ProductController : KendoGridAdminController
    {
        public ProductController(IGiftsSystemData data)
            : base(data)
        {
            this.data = data;
        }

        // GET: Administration/Product
        public ActionResult Index()
        {
            var catNames = this.data.Categories.All().Select(c => c.Name);
            var list = new SelectList(catNames);
            this.ViewBag.CategoryNames = list;
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.data.Products.All().Project().To<ViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.data.Products.GetById(id) as T;
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var dbModel = base.Create<Model>(model);
            if (dbModel != null)
            {
                model.ID = dbModel.ID;
                var category = this.data.Categories.All().FirstOrDefault(c => c.Name == model.CategoryName);
                dbModel.Category = category;
                category.Products.Add(dbModel);
                model.CategoryName = category.Name;

                this.data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Update<Model, ViewModel>(model, model.ID);

            var dbModel = this.data.Products.GetById(model.ID);

            if (model.CategoryName!=null && dbModel.Category.Name!=model.CategoryName)
            {
                var newCategory = this.data.Categories.All().FirstOrDefault(c => c.Name == model.CategoryName);
                dbModel.Category.Products.Remove(dbModel);
                dbModel.Category = newCategory;
                newCategory.Products.Add(dbModel);

                
            }

            this.data.SaveChanges();
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var product = this.data.Products.GetById(model.ID);
                this.data.Products.ActualDelete(product);
                this.data.SaveChanges();                
            }

            return this.GridOperation(model, request);
        }


    }
}