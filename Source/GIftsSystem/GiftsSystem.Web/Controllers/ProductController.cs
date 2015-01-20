namespace GiftsSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using GiftsSystem.Data;
    using GiftsSystem.Models;
    using GiftsSystem.Web.ViewModels.Product;
    using Microsoft.AspNet.Identity;

    public class ProductController : BaseController
    {
        public ProductController(IGiftsSystemData data)
            : base(data)
        {

        }

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create(string categoryName)
        {
            var productModel = new CreateProductViewModel();
            var categoriesSelection = new List<SelectListItem>();
            if (categoryName == null)
            {
                foreach (var item in this.data.Categories.All())
                {
                    categoriesSelection.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.ID.ToString()
                    });
                }

            }
            else
            {
                var categoryId = this.data.Categories.All().Where(c => c.Name == categoryName).FirstOrDefault().ID;

                categoriesSelection.Add(new SelectListItem()
                    {
                        Text = categoryName,
                        Value = categoryId.ToString()
                    });
            }

            //ViewBag.CategoriesForDr = categoriesSelection;
            productModel.Categories = categoriesSelection;

            return this.View(productModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                return this.View(product);
            }

            var category = this.data.Categories.All().FirstOrDefault(c => c.ID.ToString() == product.CategoryId);
            DateTime expirationDate = (DateTime)product.ExpirationDate;

            var newProduct = new Product()
            {
                Name = product.Name,
                Description = product.Description,
                ExpirationDate = expirationDate,
                CreatedOn = DateTime.Now,
                Price = product.Price,
                Quantity = product.Quantity,
                Category = category,
                Condition = "New"

            };

            if (product.UploadedImage != null)
            {
                using (var memory = new MemoryStream())
                {
                    product.UploadedImage.InputStream.CopyTo(memory);
                    var content = memory.GetBuffer();

                    newProduct.Image = new Image
                    {
                        Content = content,
                        FileExtension = product.UploadedImage.FileName.Split(new[] { '.' }).Last()
                    };
                }
            }

            this.data.Products.Add(newProduct);
            this.data.SaveChanges();

            return this.Redirect("/");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var currentProduct = this.data.Products.GetById(id);
            var currentUser = this.GetCurrentUser();
            bool isUserHaveProduct = false;

            var userCollectionSelection = new List<SelectListItem>();

            foreach (var collection in currentUser.GiftsCollections)
            {
                userCollectionSelection.Add(new SelectListItem()
                {
                    Text = collection.Name,
                    Value = collection.ID.ToString()
                });

                if (collection.Products.Count != 0 && collection.Products.Contains(currentProduct))
                {
                    isUserHaveProduct = true;
                }

            }

            ViewBag.UserCollections = userCollectionSelection;

            if (isUserHaveProduct)
            {
                ViewBag.IsUserHaveProduct = true;
            }

            var result = this.data.Products.All().Where(p => p.ID == id)
                .Project().To<DetailsProductViewModel>().FirstOrDefault();

            return this.View(result);
        }

        [HttpPost]
        [Authorize]
        public void AddToWishList(int id, string userCollection)
        {
            var isAjax = Request.IsAjaxRequest();
            var productToAdd = this.data.Products.GetById(id);
            var currentUser = this.GetCurrentUser();
            var collection = currentUser.GiftsCollections.FirstOrDefault(c => c.ID == int.Parse(userCollection));
            collection.Products.Add(productToAdd);
            productToAdd.GiftsList.Add(collection);

            this.data.Products.Update(productToAdd);
            this.data.GiftsLists.Update(collection);
            this.data.Users.Update(currentUser);

            this.data.SaveChanges();
        }

        public ActionResult Image(int id)
        {
            var image = this.data.Images.GetById(id);
            if (image == null)
            {
                throw new HttpException(404, "Image not found");
            }

            return File(image.Content, "image/" + image.FileExtension);
        }

        [HttpGet]
        public ActionResult Search(string inputProduct)
        {
            if (inputProduct == null && inputProduct == string.Empty)
            {
                return this.View();
            }

            var allMatchingProducts = this.data.Products
                .All()
                .Where(p => p.Name.Contains(inputProduct))
                .Project()
                .To<ListProductsViewModel>()
                .ToList();

            return this.View("ListProducts", allMatchingProducts);
        }

    }
}