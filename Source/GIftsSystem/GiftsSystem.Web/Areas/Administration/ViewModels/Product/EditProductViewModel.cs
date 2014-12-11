namespace GiftsSystem.Web.Areas.Administration.ViewModels.Product
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
using GiftSystem.Web.Infrastructure.Mapping;

    public class EditProductViewModel:IMapFrom<GiftsSystem.Models.Product>
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Condition { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }


    }
}