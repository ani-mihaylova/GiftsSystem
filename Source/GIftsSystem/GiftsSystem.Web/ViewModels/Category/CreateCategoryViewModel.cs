using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GiftSystem.Web.Infrastructure.Mapping;

namespace GiftsSystem.Web.ViewModels.Category
{
    public class CreateCategoryViewModel:IMapFrom<GiftsSystem.Models.Category>,IHaveCustomMappings
    {
        [Required]
        [UIHint("StringLineText")]
        public string Name { get; set; }

        [Required]
        [Display(Name="Description")]
        [AllowHtml]
        [DataType("tinymce_full")]
        [UIHint("tinymce_full")]
        public string Description { get; set; }

        public string ParentCategoryId { get; set; }

        public HttpPostedFileBase UploadedImage { get; set; }

        public ICollection<SelectListItem> ParentCategories { get; set; }


        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<GiftsSystem.Models.Category, CreateCategoryViewModel>()
                .ForMember(m => m.ParentCategoryId, opt => opt.MapFrom(t => t.ParentCategory.Name))
                .ReverseMap();
        }
    }
}