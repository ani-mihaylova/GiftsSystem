namespace GiftsSystem.Web.ViewModels.Product
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class CreateProductViewModel
    {
        [Required]
        [UIHint("StringLineText")]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        [UIHint("MultiLineText")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Duration)]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [UIHint("Integer")]
        public int Quantity { get; set; }
      
        [Required]
        [UIHint("Number")]
        public double Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public byte[] Image { get; set; }

        public ICollection<SelectListItem> Categories { get; set; }

    }
}