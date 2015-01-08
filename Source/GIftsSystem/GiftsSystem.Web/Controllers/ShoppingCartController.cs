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

        // GET: ShoppingCar
        public ActionResult Index()
        {

            var result = this.GetCurrentShoppingCart();
            if (result == null)
            {
                return Redirect("/");
            }

            return this.View("Index", result);
        }

        [HttpGet]
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
        public ActionResult Details(ShoppingCartMainViewModel inputModel)
        {
            return this.View();
        }

        [HttpGet]
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

            return this.View("Pay",newPaymentInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pay(CreatePaymentInfoViewModel inputModel)
        {
            if (this.ModelState.IsValid==false)
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
                if (!currentShoppingCart.BoughtProducts.ContainsKey(product.ID))
                {
                    currentShoppingCart.BoughtProducts.Add(product.ID, true);
                }
            }

            this.data.SaveChanges();

            return this.Redirect("/");
        }

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
        public void ByeProduct(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            var product = this.data.Products.GetById(id);

            var currentUser = this.GetCurrentUser();
            if (currentUser.ShoppingCart == null)
            {
                var newGiftsList = new GiftsList() { Name = currentUser.UserName + "Cart" };
                this.data.GiftsLists.Add(newGiftsList);
                currentUser.ShoppingCart = newGiftsList;
            }

            currentUser.ShoppingCart.Products.Add(product);

            //this.data.Users.Update(currentUser);
            //this.data.Products.Update(product);

            this.data.SaveChanges();
        }


        [HttpPost]
        public void ByeProductFromCollection(int id, string userID, int collectionID)
        {
            //var isAjax = Request.IsAjaxRequest();
            //var product = this.data.Products.GetById(id);

            ////Set product as bought in collection
            //var userWithCollection = this.data.Users.GetById(userID);
            //var currentCollection = this.data.GiftsLists.GetById(collectionID);
            //currentCollection.BoughtProducts.Add(product.ID, true);

            ////Add product to current user's collection of bought prducts
            //var currentUser = this.GetCurrentUser();
            //currentUser.ShoppingCart.Products.Add(product);

            //this.data.Users.Update(currentUser);
            //this.data.Products.Update(product);

            //this.data.SaveChanges();
        }

        private ShoppingCartMainViewModel GetCurrentShoppingCart()
        {
            var currentUser = this.GetCurrentUser();

            var result = this.data.Users
                .All()
                .Where(u => u.Id == currentUser.Id)
                .Project()
                .To<ShoppingCartMainViewModel>().FirstOrDefault();

            return result;
        }
    }
}