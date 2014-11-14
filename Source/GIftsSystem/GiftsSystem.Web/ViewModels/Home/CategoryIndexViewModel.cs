using System;
namespace GiftsSystem.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using GiftsSystem.Models;
    using GiftSystem.Web.Infrastructure.Mapping;

    public class CategoryIndexViewModel : IMapFrom<Category>
    {
        
        public CategoryIndexViewModel()
        {
            this.CategoriesChilderen = new Dictionary<string, List<string>>();

        }
        public string Name { get; set; }

        public Dictionary<string, List<string>> CategoriesChilderen { get; set; }

        public Category ParentCategoryID { get; set; }
    }
}