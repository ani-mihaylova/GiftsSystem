using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GiftSystem.Web.Infrastructure.Mapping;

namespace GiftsSystem.Web.ViewModels.Product
{
    public class ListProductsViewModel:IMapFrom<GiftsSystem.Models.Product>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        //public HttpPostedFileBase UploadedImage { get; set; }

        public string CategoryName { get; set; }

        public int? ImageId { get; set; }


        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<GiftsSystem.Models.Product, ListProductsViewModel>()
                .ForMember(p => p.CategoryName, opt => opt.MapFrom(t => t.Category.Name))
                .ReverseMap();
        }
    }
}