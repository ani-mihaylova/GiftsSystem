using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GiftSystem.Web.Infrastructure.Mapping;
using GiftsSystem.Models;

namespace GiftsSystem.Web.ViewModels.Category
{
    public class CategoryDetailsView:IMapFrom<GiftsSystem.Models.Category>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<GiftsSystem.Models.Product> Products { get; set; }
    }
}