namespace GiftsSystem.Web.ViewModels.Category
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using GiftSystem.Web.Infrastructure.Mapping;

    public class CategoryEditView:IMapFrom<GiftsSystem.Models.Category>
    {
        public string Name { get; set; }

        public ICollection<GiftsSystem.Models.Product> Products { get; set; }

        public GiftsSystem.Models.Category ParentCategoryID { get; set; }

        public string Description { get; set; }
    }
}