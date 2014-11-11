namespace GiftsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            //TODO:Remove in production
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
           
        }
    }
}
