namespace GiftsSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GiftsSystem.Data.Common;

    public class GiftsList : FullEntity
    {
        private ICollection<Product> products;
        private ICollection<Product> boughtProducts;

        public GiftsList()
        {
            this.products = new HashSet<Product>();
            this.boughtProducts = new HashSet<Product>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Product> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }

        public virtual ICollection<Product> BoughtProducts
        {
            get { return this.boughtProducts; }
            set { this.boughtProducts = value; }
        }

        public virtual ApplicationUser User { get; set; }
    }
}
