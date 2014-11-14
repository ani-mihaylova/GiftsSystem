namespace GiftsSystem.Web.Controllers
{
    using System.Web.Mvc;
    using GiftsSystem.Data;

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