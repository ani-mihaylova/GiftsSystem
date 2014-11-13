using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GiftsSystem.Web.Models;

namespace GiftsSystem.Web.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult AddInfo(IndexViewModel model)
        {
            return this.PartialView("_AdditionToManage", model);
        }
    }
}