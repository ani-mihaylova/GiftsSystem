using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GiftsSystem.Web.Areas.Administration.Controllers
{
    public class UserController : Controller
    {
        // GET: Administration/User
        public ActionResult Index()
        {
            return View();
        }
    }
}