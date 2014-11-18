namespace GiftsSystem.Web.ViewModels.Category
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using GiftSystem.Web.Infrastructure.Mapping;

    public class EditCategoryViewModel:IMapFrom<GiftsSystem.Models.Category>, IHaveCustomMappings
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public ICollection<GiftsSystem.Models.Product> Products { get; set; }

        [AllowHtml]
        [DataType("tinymce_full")]
        [UIHint("tinymce_full")]
        public string Description { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<GiftsSystem.Models.Category, EditCategoryViewModel>()
                 .ForMember(m => m.Products, opt => opt.MapFrom(t => t.Products))
                 .ReverseMap();

        }
    }
}