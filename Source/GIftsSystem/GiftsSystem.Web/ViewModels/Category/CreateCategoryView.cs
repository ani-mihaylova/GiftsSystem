using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GiftSystem.Web.Infrastructure.Mapping;

namespace GiftsSystem.Web.ViewModels.Category
{
    public class CreateCategoryView:IMapFrom<GiftsSystem.Models.Category>
    {
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public GiftsSystem.Models.Category ParentCategoryID { get; set; }

        public byte[] Image { get; set; }
    }
}