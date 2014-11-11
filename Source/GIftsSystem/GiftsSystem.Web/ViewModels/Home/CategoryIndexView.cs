using System;
namespace GiftsSystem.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using GiftsSystem.Models;
    using GiftSystem.Web.Infrastructure.Mapping;

    public class CategoryIndexView : IMapFrom<Category>
    {
        public string Name { get; set; }
    }
}