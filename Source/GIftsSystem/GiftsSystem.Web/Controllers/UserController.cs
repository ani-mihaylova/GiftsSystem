using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GiftsSystem.Data;
using GiftsSystem.Web.Models;
using Microsoft.AspNet.Identity;

namespace GiftsSystem.Web.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IGiftsSystemData data)
            : base(data)
        {

        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

      
        //[ChildActionOnly]
        //public PartialViewResult AddInfo(IndexViewModel model)
        //{
        //    return this.PartialView("_AdditionToManage", model);
        //}
    }
}