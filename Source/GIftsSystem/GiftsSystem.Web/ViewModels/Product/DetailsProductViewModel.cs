﻿namespace GiftsSystem.Web.ViewModels.Product
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using GiftSystem.Web.Infrastructure.Mapping;

    public class DetailsProductViewModel : IMapFrom<GiftsSystem.Models.Product>
    {
        [HiddenInput(DisplayValue=false)]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Condition { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public byte[] Image { get; set; }

        public GiftsSystem.Models.Category CategoryID { get; set; }
    }
}