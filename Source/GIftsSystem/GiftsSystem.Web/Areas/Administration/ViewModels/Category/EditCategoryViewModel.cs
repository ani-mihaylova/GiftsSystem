using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AutoMapper;
using GiftsSystem.Models;
using GiftSystem.Web.Infrastructure.Mapping;
using Model = GiftsSystem.Models.Category;

namespace GiftsSystem.Web.Areas.Administration.ViewModels.Category
{
    public class EditCategoryViewModel : IMapFrom<GiftsSystem.Models.Category>,IHaveCustomMappings
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string ParentCategoryName { get; set; }

        public string Description { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Model, EditCategoryViewModel>()
                .ForMember(p => p.ID, opt => opt.MapFrom(t => t.ID))
                .ForMember(p => p.Name, opt => opt.MapFrom(t => t.Name))
                .ForMember(p => p.Description, opt => opt.MapFrom(t => t.Description))
                .ForMember(p => p.ParentCategoryName, opt => opt.MapFrom(t => t.ParentCategory.Name))
                .ReverseMap();
        }
    }
}