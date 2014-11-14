namespace GiftsSystem.Web.Controllers
{
    using System.Web.Mvc;
    using GiftsSystem.Data;
    using GiftsSystem.Models;
    using Microsoft.AspNet.Identity;

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

        protected ApplicationUser CurrentUser { get; set; }
       
    }
}