namespace GiftsSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using GiftsSystem.Data;
    using GiftsSystem.Models;

    public class GiftsListController : BaseController
    {
        public GiftsListController(IGiftsSystemData data)
            : base(data)
        {

        }

        // GET: GiftsList
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var newGiftsList = new GiftsList()
            {
                CreatedOn = DateTime.Now
            };

            return this.View(newGiftsList);
        }

        [HttpPost]
        public ActionResult Create(GiftsList model)
        {
            var newGiftsList = new GiftsList()
            {
                Name = model.Name
            };

            var currentUser = this.GetCurrentUser();

            newGiftsList.User = currentUser;
            newGiftsList.Products = new List<Product>();
            this.data.GiftsLists.Add(newGiftsList);
            currentUser.GiftsCollections.Add(newGiftsList);
            this.data.Users.Update(currentUser);

            this.data.SaveChanges();

            return this.RedirectToAction("Details", "User", new { id = currentUser.Id });
        }
    }
}