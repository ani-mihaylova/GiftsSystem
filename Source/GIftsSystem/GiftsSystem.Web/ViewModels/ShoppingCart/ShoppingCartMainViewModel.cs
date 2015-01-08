namespace GiftsSystem.Web.ViewModels.ShoppingCart
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using GiftsSystem.Models;
    using GiftSystem.Web.Infrastructure.Mapping;

    public class ShoppingCartMainViewModel:IMapFrom<ApplicationUser>
    {
        public string ID { get; set; }

        public GiftsList ShoppingCart { get; set; }
    }
}