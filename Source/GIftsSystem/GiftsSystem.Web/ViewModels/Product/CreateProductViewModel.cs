namespace GiftsSystem.Web.ViewModels.Product
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class CreateProductViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Duration)]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public int Quantity { get; set; }

        //TODO:Custom validation for future date
        [Required]  
        [DataType(DataType.Currency)]
        public int Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public byte[] Image { get; set; }
    }
}