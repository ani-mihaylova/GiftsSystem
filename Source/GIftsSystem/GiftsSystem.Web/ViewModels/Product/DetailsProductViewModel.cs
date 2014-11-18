namespace GiftsSystem.Web.ViewModels.Product
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using GiftSystem.Web.Infrastructure.Mapping;

    public class DetailsProductViewModel : IMapFrom<GiftsSystem.Models.Product>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Condition { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public HttpPostedFileBase UploadedImage { get; set; }

        public string CategoryName { get; set; }

        public bool IsBought { get; set; }

        public int? ImageId { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<GiftsSystem.Models.Product, DetailsProductViewModel>()
                .ForMember(m => m.ID, opt => opt.MapFrom(t => t.ID))
                .ForMember(m => m.CategoryName, opt => opt.MapFrom(t => t.Category.Name))
                .ReverseMap();

        }
    }
}