namespace GiftsSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GiftsSystem.Data.Common;

    //TODO: Custom validation
    public class Product : FullEntity
    {
        private ICollection<GiftsList> giftsList;
        public Product()
        {
            this.giftsList = new HashSet<GiftsList>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Condition { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }

        public int? ImageId { get; set; }

        public virtual Image Image { get; set; }

        public virtual Category Category { get; set; }

        [DefaultValue(false)]
        public bool IsBought { get; set; }

        public virtual ICollection<GiftsList> GiftsList
        {
            get { return this.giftsList; }
            set { this.giftsList = value; }
        }
    }
}
