namespace GiftsSystem.Web.Controllers
{
    using System.Web.Mvc;
    using AutoMapper;
    using GiftsSystem.Data;
    using GiftsSystem.Models;
    using GiftsSystem.Web.ViewModels.GiftsList;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using AutoMapper;

    [Authorize]
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
            var newGiftsList = new CreateGiftsListViewModel();

            return this.View(newGiftsList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateGiftsListViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/");
            }

            var newGiftsList = Mapper.Map<GiftsList>(model);
            var currentUser = this.GetCurrentUser();
            newGiftsList.User = currentUser;

            this.data.GiftsLists.Add(newGiftsList);
            currentUser.GiftsCollections.Add(newGiftsList);

            this.data.SaveChanges();

            return this.RedirectToAction("Details", "User", new { id = currentUser.Id });
        }

        [HttpGet]
        public ActionResult Edit(int? collectionID)
        {
            if (collectionID==null)
            {
                return this.Redirect("/User/Details?id=" + this.GetCurrentUser().Id);
            }
            var currentCollection = this.data
                .GiftsLists
                .All()
                .Where(c => c.ID == collectionID)
                .Project()
                .To<EditGiftsListViewModel>()
                .FirstOrDefault();

            return this.View("Edit", currentCollection);
        }

        [HttpPost]
        public ActionResult Edit(EditGiftsListViewModel model)
        {
            var editedModel = Mapper.Map<GiftsList>(model);

            this.data.GiftsLists.Update(editedModel);

            return this.Redirect("/User/Details?id="+this.GetCurrentUser().Id);
        }
        public void RemoveProduct(int productId, int collectionId)
        {
            var currentCollection = this.data.GiftsLists.GetById(collectionId);
            var productToRemove=this.data.Products.GetById(productId);

            currentCollection.Products.Remove(productToRemove);
            this.data.SaveChanges();
        }
    }
}