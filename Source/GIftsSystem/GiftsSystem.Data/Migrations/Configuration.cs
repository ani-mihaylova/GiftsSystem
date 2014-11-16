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
            //Add Reles
            this.SeedRoles(context);

            //Add Catagories
            this.SeedCatagories(context);

            context.SaveChanges();
        }

        private void SeedRoles(ApplicationDbContext context)
        {
            var roles = new List<IdentityRole>()
            {
                new IdentityRole("Admin"),
                 new IdentityRole("Company"),
                  new IdentityRole("User")
            };

            if (context.Roles.Count() != 0)
            {
                return;

            }

            foreach (var role in roles)
            {
                context.Roles.Add(role);
            }


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
