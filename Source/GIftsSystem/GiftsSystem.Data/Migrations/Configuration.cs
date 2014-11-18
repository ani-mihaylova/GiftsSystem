namespace GiftsSystem.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using GiftsSystem.Common;
    using GiftsSystem.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        private UserManager<ApplicationUser> userManager;

        public Configuration()
        {
            //TODO:Remove in production
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
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
                UserName = "Administrator"
            };

            var companyUser = new ApplicationUser
            {
                Email = "cmompany@mysite.com",
                UserName = "Company"
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
