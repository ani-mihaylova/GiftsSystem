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
            //TODO:Custom binding
            var all = this.categories.All();
            Dictionary<Category, List<Category>> categoriesWithChilderen = new Dictionary<Category, List<Category>>();
            foreach (var item in all)
            {
                if (item.ParentCategoryID==null)
                {
                    categoriesWithChilderen.Add(item, new List<Category>());
                }
                else
                {
                    categoriesWithChilderen[item.ParentCategoryID].Add(item);
                }
            }

            //foreach (var item in categoriesWithChilderen)
            //{
               
            //}

            return View(categoriesWithChilderen);
        }
    }
}