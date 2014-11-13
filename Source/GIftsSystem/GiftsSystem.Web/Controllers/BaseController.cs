using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GiftsSystem.Data;

namespace GiftsSystem.Web.Controllers
{
    public class BaseController : Controller
    {
        protected IGiftsSystemData data;

        public BaseController()
        {

        }

        public BaseController(IGiftsSystemData data)
            : this()
        {
            this.data = data;
        }
    }
}