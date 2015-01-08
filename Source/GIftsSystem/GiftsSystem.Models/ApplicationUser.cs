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
    using GiftsSystem.Data.Contracts;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        private ICollection<GiftsList> wishLists;

        public ApplicationUser()
        {
            // This will prevent UserManager.CreateAsync from causing exception
            this.CreatedOn = DateTime.Now;
            this.wishLists = new HashSet<GiftsList>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual ICollection<GiftsList> GiftsCollections
        {
            get { return this.wishLists; }
            set { this.wishLists = value; }
        }

        public virtual GiftsList ShoppingCart { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }


        public DateTime CreatedOn { get; set; }


        public bool PreserveCreatedOn { get; set; }


        public DateTime? ModifiedOn { get; set; }

        public int? ImageId { get; set; }

        public virtual Image Image { get; set; }

    }
}
