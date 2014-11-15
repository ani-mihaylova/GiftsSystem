namespace GiftsSystem.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using GiftsSystem.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            //TODO:Remove in production
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //Add Admin
           // this.SeedAdmin(context);

            //Add Catagories
            this.SeedCatagories(context);                  

        }

        private void SeedAdmin(ApplicationDbContext context)
        {
            IdentityRole role = new IdentityRole("Admin");

            if (context.Roles.Contains(role))
            {
                return;

            }

            context.Roles.Add(role);
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
                 new Category(){ Name="Jewelry", ParentCategoryID=fashoinCategory},
                new Category(){ Name="Watches", ParentCategoryID=fashoinCategory},
                new Category(){ Name="Handbags", ParentCategoryID=fashoinCategory},
                 new Category(){ Name="Bath", ParentCategoryID=homeCategoryId},
                  new Category(){ Name="Furniture", ParentCategoryID=homeCategoryId},
                   new Category(){ Name="HomeDecor", ParentCategoryID=homeCategoryId},
                    new Category(){ Name="Lamp", ParentCategoryID=homeCategoryId}
            };
            foreach (var item in subCategories)
            {
                context.Categories.Add(item);
            }
            context.SaveChanges();
        }
    }
}
