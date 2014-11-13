namespace GiftsSystem.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GiftsSystem.Data.Repositories;
    using GiftsSystem.Models;

    public interface IGiftsSystemData
    {
        IApplicationDbContext Context { get; }

        IDeletableEntityRepository<Product> Products { get; }

        IDeletableEntityRepository<Category> Categories { get; }

        IGenericRepository<ApplicationUser> Users { get; }

        int SaveChanges();
    }
}
