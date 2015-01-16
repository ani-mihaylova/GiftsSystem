namespace GiftsSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using GiftsSystem.Data.Common;


    public class Category : FullEntity
    {
        private ICollection<Product> products;

        public Category()
        {
            this.products = new HashSet<Product>();
        }

        public int ID { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public ICollection<Product> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }

        public virtual Category ParentCategory { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public int? ImageId { get; set; }

        public virtual Image Image { get; set; }

    }
}
