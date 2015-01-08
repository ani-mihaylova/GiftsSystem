namespace GiftsSystem.Web.Areas.Administration.ViewModels.Product
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using GiftSystem.Web.Infrastructure.Mapping;

    [Bind(Exclude = "CategoryNames")]
    public class EditProductViewModel : IMapFrom<GiftsSystem.Models.Product>, IHaveCustomMappings
    {
        public int ID { get; set; }

        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string Condition { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public string CategoryName { get; set; }

        public IEnumerable<SelectListItem> CategoryNames { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<GiftsSystem.Models.Product, EditProductViewModel>()
                .ForMember(p => p.CategoryName, opt => opt.MapFrom(t => t.Category.Name))
                .ReverseMap();
        }
    }
}