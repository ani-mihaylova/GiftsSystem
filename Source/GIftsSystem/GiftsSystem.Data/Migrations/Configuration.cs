namespace GiftsSystem.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using GiftsSystem.Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using System.IO;
    using System.Drawing;
    using GiftsSystem.Models;
    using System.Net.Mime;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        private UserManager<ApplicationUser> userManager;

        public Configuration()
        {
            //TODO:Remove in production
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            this.userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            this.SeedRoles(context);
            this.SeedUsers(context);

            //Add Catagories
            this.SeedCatagories(context);

            context.SaveChanges();
        }

        private void SeedRoles(ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole(GlobalConstants.AdminRoleName));
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole(GlobalConstants.CompanyRoleName));
            context.SaveChanges();
        }

        private void SeedUsers(ApplicationDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var adminUser = new ApplicationUser
            {
                Email = "admin@mysite.com",
                UserName = "admin@mysite.com"
            };

            var companyUser = new ApplicationUser
            {
                Email = "cmompany@mysite.com",
                UserName = "cmompany@mysite.com"
            };

            this.userManager.Create(companyUser, "admin123456");
            this.userManager.Create(adminUser, "admin123456");

            this.userManager.AddToRole(adminUser.Id, GlobalConstants.AdminRoleName);
            this.userManager.AddToRole(companyUser.Id, GlobalConstants.CompanyRoleName);
        }

        private void SeedCatagories(ApplicationDbContext context)
        {
            if (context.Categories.Count() != 0)
            {
                return;
            }

            List<Category> categories = new List<Category>()
           {
               new Category(){ Name="Home"},
               new Category(){ Name="Fashion"},
                new Category(){ Name="Electronics"},
                new Category(){ Name="Art"},
                new Category(){ Name="Sport"}
               
           };

            List<string> imagePaths = new List<string>()
            {
                @"D:\TelerikAcademy\ASP.NET MVC\Project-GiftsSystem\GiftsSystem\Source\GIftsSystem\GiftsSystem.Common\Images\Home.jpg",
                @"D:\TelerikAcademy\ASP.NET MVC\Project-GiftsSystem\GiftsSystem\Source\GIftsSystem\GiftsSystem.Common\Images\fashion.jpg",
                @"D:\TelerikAcademy\ASP.NET MVC\Project-GiftsSystem\GiftsSystem\Source\GIftsSystem\GiftsSystem.Common\Images\Electronics.jpg",
                @"D:\TelerikAcademy\ASP.NET MVC\Project-GiftsSystem\GiftsSystem\Source\GIftsSystem\GiftsSystem.Common\Images\Art.jpg",
                @"D:\TelerikAcademy\ASP.NET MVC\Project-GiftsSystem\GiftsSystem\Source\GIftsSystem\GiftsSystem.Common\Images\Sports.jpg"
            };

            for (int i = 0; i < imagePaths.Count; i++)
            {
                var img = System.Drawing.Image.FromFile(imagePaths[i]);

                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                var imageAsArr = ms.ToArray();

                var newImage = new Models.Image();
                newImage.Content = imageAsArr;
                newImage.FileExtension = "jpeg";
                context.Images.Add(newImage);

                categories[i].Image = newImage;

            }


            foreach (var item in categories)
            {
                context.Categories.Add(item);
            }

            context.SaveChanges();
            this.SeedSubCategories(context);
        }

        private void SeedSubCategories(ApplicationDbContext context)
        {
            var fashoinCategory = context.Categories.FirstOrDefault(c => c.Name == "Fashion");
            var homeCategoryId = context.Categories.FirstOrDefault(c => c.Name == "Home");

            List<Category> subCategories = new List<Category>()
            {
                 new Category(){ Name="Jewelry", ParentCategory=fashoinCategory},
                new Category(){ Name="Watches", ParentCategory=fashoinCategory},
                new Category(){ Name="Handbags", ParentCategory=fashoinCategory},
                 new Category(){ Name="Bath", ParentCategory=homeCategoryId},
                  new Category(){ Name="Furniture", ParentCategory=homeCategoryId},
                   new Category(){ Name="HomeDecor", ParentCategory=homeCategoryId},
                    new Category(){ Name="Lamp", ParentCategory=homeCategoryId}
            };
            foreach (var item in subCategories)
            {
                context.Categories.Add(item);
            }
        }
    }
}
