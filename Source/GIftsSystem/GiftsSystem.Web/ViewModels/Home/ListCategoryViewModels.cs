using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using GiftSystem.Web.Infrastructure.Mapping;

namespace GiftsSystem.Web.ViewModels.Home
{
    public class ListCategoryViewModels : IMapFrom<GiftsSystem.Models.Category>, IHaveCustomMappings
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string ParentCategoryName { get; set; }

        public string Description { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<GiftsSystem.Models.Category, ListCategoryViewModels>()
                .ForMember(pr => pr.ParentCategoryName, opt => opt.MapFrom(t => t.ParentCategory.Name))
                .ReverseMap();
        }
    }
}