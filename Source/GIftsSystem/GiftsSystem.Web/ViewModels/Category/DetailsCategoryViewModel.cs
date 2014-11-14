using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GiftSystem.Web.Infrastructure.Mapping;
using GiftsSystem.Models;
using System.Web.Mvc;

namespace GiftsSystem.Web.ViewModels.Category
{
    public class DetailsCategoryViewModel:IMapFrom<GiftsSystem.Models.Category>
    {
        public string Name { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        public ICollection<GiftsSystem.Models.Product> Products { get; set; }
    }
}