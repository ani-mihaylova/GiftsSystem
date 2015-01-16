namespace GiftsSystem.Web.Areas.Administration.Controllers.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using GiftsSystem.Data;
    using Kendo.Mvc.UI;
    using System.Collections;
    using AutoMapper;
    using System.Data.Entity;
    using Kendo.Mvc.Extensions;

    public abstract class KendoGridAdminController : AdminController
    {
        public KendoGridAdminController(IGiftsSystemData data)
            : base(data)
        {
        }

        [HttpPost]
        public ActionResult Read([DataSourceRequest]
                                 DataSourceRequest request)
        {
            var data = this.GetData()
                           .ToDataSourceResult(request);

            return this.Json(data);
        }

        protected abstract IEnumerable GetData();

        protected abstract T GetById<T>(object id) where T : class;

        [NonAction]
        protected virtual T Create<T>(object model) where T : class
        {
            if (model != null && this.ModelState.IsValid)
            {
                var dbModel = Mapper.Map<T>(model);
                this.ChangeEntityStateAndSave(dbModel, EntityState.Added);
                return dbModel;
            }

            return null;
        }

        [NonAction]
        protected virtual void Update<TModel, TViewModel>(TViewModel model, object id)
            where TModel : class
            where TViewModel : class
        {
            if (model != null && this.ModelState.IsValid)
            {
                var dbModel = this.GetById<TModel>(id);
                Mapper.Map<TViewModel, TModel>(model, dbModel);
                this.ChangeEntityStateAndSave(dbModel, EntityState.Modified);
            }
        }

        protected JsonResult GridOperation<T>(T model, [DataSourceRequest]
                                              DataSourceRequest request)
        {
            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }

        private void ChangeEntityStateAndSave(object dbModel, EntityState state)
        {
            var entry = this.data.Context.Entry(dbModel);
            entry.State = state;
            this.data.SaveChanges();
        }
    }
}