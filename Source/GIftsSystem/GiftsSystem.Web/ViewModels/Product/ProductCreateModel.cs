namespace GiftsSystem.Web.ViewModels.Product
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class ProductCreateModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        public int CategoryId { get; set; }
    }
}