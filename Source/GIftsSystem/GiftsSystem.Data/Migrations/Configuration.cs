namespace GiftsSystem.Data.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using GiftsSystem.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {      
        
        public Configuration()
        {
            //TODO:Remove in production
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (context.Categories.Count() != 0)
            {
                return;
            }

            List<Category> categories = new List<Category>()
           {
               new Category(){ Name="Home"},
               new Category(){ Name="Fashion"},
                new Category(){ Name="Electronics"}
           };

            foreach (var item in categories)
            {
                context.Categories.Add(item);
            }
            context.SaveChanges();
        }
    }
}
