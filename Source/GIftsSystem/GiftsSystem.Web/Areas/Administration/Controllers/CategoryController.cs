namespace GiftsSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using GiftsSystem.Data;
    using GiftsSystem.Models;
    using GiftsSystem.Web.Controllers;
    using Kendo.Mvc.UI;
    using AutoMapper.QueryableExtensions;
    using GiftsSystem.Web.Areas.Administration.ViewModels.Category;
    using GiftsSystem.Web.Areas.Administration.Controllers.Base;
    using System.Collections;

    using Model = GiftsSystem.Models.Category;
    using ViewModel = GiftsSystem.Web.Areas.Administration.ViewModels.Category.EditCategoryViewModel;

    public class CategoryController : KendoGridAdminController
    {
        public CategoryController(IGiftsSystemData data)
            : base(data)
        {
            this.data = data;
        }

        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.data.Categories.All().Project().To<ViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.data.Categories.GetById(id) as T;
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var dbModel = base.Create<Model>(model);
            if (dbModel != null)
            {
                model.ID = dbModel.ID;
            }

            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Update<Model, ViewModel>(model, model.ID);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var category = this.data.Categories.GetById(model.ID);
                this.data.Categories.Delete(model.ID);           
                return this.GridOperation(model, request);
            }
           
            return this.GridOperation(model, request);
        }

    }
}