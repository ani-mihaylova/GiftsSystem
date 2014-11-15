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

        protected ApplicationUser GetCurrentUser()
        {
            var currentUserId = this.User.Identity.GetUserId();
            var currentUser = this.data.Users.GetById(currentUserId);

            return currentUser;
        }
       
    }
}