namespace GiftsSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using GiftsSystem.Data.Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUser : IdentityUser, IFullEntry
    {
        public ApplicationUser()
        {
            // This will prevent UserManager.CreateAsync from causing exception
            this.CreatedOn = DateTime.Now;
            this.WishList = new HashSet<Product>();
            this.ImagePath = "~/Images/default-user-image.png";           
            
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual ICollection<Product> WishList { get; set; }

        //public ICollection<Product> BoughtProducts { get; set; }

        public string ImagePath { get; set; }

        //TODO:Make multiple collections
        //public ICollection<Product> PersonalCollection { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }


        public DateTime CreatedOn { get; set; }


        public bool PreserveCreatedOn { get; set; }


        public DateTime? ModifiedOn { get; set; }
       
    }
}
