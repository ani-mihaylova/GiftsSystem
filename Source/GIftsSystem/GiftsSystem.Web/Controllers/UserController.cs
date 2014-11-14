namespace GiftsSystem.Web.Controllers
{
    using System.Web.Mvc;
    using GiftsSystem.Data;

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