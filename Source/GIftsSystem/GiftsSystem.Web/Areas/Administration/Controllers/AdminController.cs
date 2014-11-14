using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GiftsSystem.Data;
using GiftsSystem.Web.Controllers;

namespace GiftsSystem.Web.Areas.Administration.Controllers
{
    //[Authorize(Roles="Admin")]
    public abstract class AdminController : BaseController
    {
        public AdminController(IGiftsSystemData data)
            : base(data)
        {

        }

        [HttpGet]
        public ActionResult Index()
        {
            return this.View("Index");
        }
    }
}