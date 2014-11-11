using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GiftsSystem.Data;
using GiftsSystem.Data.Common.Repositories;
using GiftsSystem.Models;
using AutoMapper.QueryableExtensions;
using GiftsSystem.Web.ViewModels.Home;

namespace GiftsSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private IGenericRepository<Category> categories;
     
        public HomeController(IGenericRepository<Category> categories)
        {
            this.categories = categories;
        }

        public ActionResult Index()
        {
            var all = this.categories.All().Project().To<CategoryIndexView>();
            return View(all);
        }
    }
}