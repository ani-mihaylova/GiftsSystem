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
    using System.IO;

    public class ApplicationUser : IdentityUser, IFullEntry
    {
        public ApplicationUser()
        {
            // This will prevent UserManager.CreateAsync from causing exception
            this.CreatedOn = DateTime.Now;           
            this.GiftsCollections = new HashSet<GiftsList>();
            this.ImagePath = "~/Images/default-user-image.png";
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        private void SetDefaultImage()
        {
            //DirectoryInfo path = Server.MapPath("~\Images\default-user-image");
            //FileInfo[] images = path.GetFiles();
            //this.Image=images.FirstOrDefault(f=>f.Name=="default-user-image");
        }

        public virtual ICollection<GiftsList> GiftsCollections { get; set; }

        //public ICollection<Product> BoughtProducts { get; set; }

        public string ImagePath { get; set; }

        public byte[] Image { get; set; }     

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }


        public DateTime CreatedOn { get; set; }


        public bool PreserveCreatedOn { get; set; }


        public DateTime? ModifiedOn { get; set; }
       
    }
}
