namespace GiftsSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using GiftsSystem.Data;
    using GiftsSystem.Models;
    using AutoMapper.QueryableExtensions;
    using GiftsSystem.Web.ViewModels.ShoppingCart;
    using AutoMapper;

    public class ShoppingCartController : BaseController
    {
        public ShoppingCartController(IGiftsSystemData data)
            : base(data)
        {

        }

        [HttpGet]
        [ChildActionOnly]
        [Authorize]
        //[OutputCache(Duration = 1000)]
        public ActionResult ShoppingCartIndex()
        {
            return UpdateShoppingCart();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Details()
        {
            var result = this.GetCurrentShoppingCart();
            if (result == null)
            {
                return Redirect("/");
            }
            return this.View("Details", result);

        }

        [HttpPost]
        [Authorize]
        public ActionResult Details(ShoppingCartMainViewModel inputModel)
        {
            return this.View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Proceed()
        {
            var newPaymentInfo = new CreatePaymentInfoViewModel();

            var regionItems = new List<SelectListItem>();
            var regionNames = Enum.GetNames(typeof(RegionNames));
            foreach (var name in regionNames)
            {
                regionItems.Add(
                    new SelectListItem()
                    {
                        Text = name,
                        Value = name
                    });
            }
            newPaymentInfo.Regions = regionItems;

            var countryItems = new List<SelectListItem>();
            var countryNames = Enum.GetNames(typeof(Countries));
            foreach (var name in countryNames)
            {
                countryItems.Add(
                    new SelectListItem()
                    {
                        Text = name,
                        Value = name
                    });
            }
            newPaymentInfo.Countries = countryItems;

            return this.View("Pay", newPaymentInfo);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Pay(CreatePaymentInfoViewModel inputModel)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.RedirectToAction("Proceed");
            }

            var newPaymentInfo = Mapper.Map<PaymentInfo>(inputModel);
            var currentUser = this.GetCurrentUser();
            newPaymentInfo.User = currentUser;
            this.data.PaymentInfos.Add(newPaymentInfo);

            var currentShoppingCart = this.GetCurrentShoppingCart().ShoppingCart;

            foreach (var product in currentShoppingCart.Products)
            {
                if (!currentShoppingCart.BoughtProducts.Contains(product))
                {
                    currentShoppingCart.BoughtProducts.Add(product);
                    currentShoppingCart.Products.Remove(product);
                }
            }

            this.data.SaveChanges();

            return this.Redirect("/");
        }

        [Authorize]
        public ActionResult RemoveProduct(int id)
        {
            var currentUser = GetCurrentUser();
            var productToRemove = this.data.Products.GetById(id);
            if (productToRemove == null)
            {
                return this.View();
            }

            currentUser.ShoppingCart.Products.Remove(productToRemove);

            this.data.SaveChanges();

            return this.RedirectToAction("Details");
        }

        [HttpPost]
        [Authorize]
        public ActionResult ByeProduct(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            var product = this.data.Products.GetById(id);

            var currentUser = this.GetCurrentUser();
            if (currentUser.ShoppingCart == null)
            {
                CreateShoppingCart(currentUser);
            }

            currentUser.ShoppingCart.Products.Add(product);

            this.data.SaveChanges();

            return UpdateShoppingCart();
        }

        private void CreateShoppingCart(ApplicationUser currentUser)
        {
            var newGiftsList = new GiftsList() { Name = currentUser.UserName + "Cart" };
            this.data.GiftsLists.Add(newGiftsList);
            currentUser.ShoppingCart = newGiftsList;
        }



        [HttpPost]
        [Authorize]
        public void BuyProductFromCollection(int productId, string userID, int collectionID)
        {
            var isAjax = Request.IsAjaxRequest();
            var user = this.data.Users.GetById(userID);
            var collection = user.GiftsCollections.FirstOrDefault(c => c.ID == collectionID);
            var product = this.data.Products.GetById(productId);
            collection.BoughtProducts.Add(product);

            var currentUser = this.GetCurrentUser();
            if (currentUser.ShoppingCart == null)
            {
                CreateShoppingCart(currentUser);
            }

            currentUser.ShoppingCart.Products.Add(product);

            this.data.SaveChanges();
        }

        private ShoppingCartMainViewModel GetCurrentShoppingCart()
        {
            var currentUser = this.GetCurrentUser();

            if (currentUser == null)
            {
                var newCart = new ShoppingCartMainViewModel { ShoppingCart = new GiftsList() };
                return newCart;
            }

            var result = this.data.Users
                .All()
                .Where(u => u.Id == currentUser.Id)
                .Project()
                .To<ShoppingCartMainViewModel>()
                .FirstOrDefault();

            return result;
        }

        private ActionResult UpdateShoppingCart()
        {
            var result = this.GetCurrentShoppingCart();

            return this.PartialView("_ShoppingCartIndex", result);
        }

    }
}