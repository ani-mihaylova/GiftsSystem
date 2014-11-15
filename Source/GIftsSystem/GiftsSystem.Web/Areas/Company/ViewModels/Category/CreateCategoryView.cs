using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GiftSystem.Web.Infrastructure.Mapping;

namespace GiftsSystem.Web.Areas.Company.ViewModels.Category
{
    public class CreateCategoryView:IMapFrom<GiftsSystem.Models.Category>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name="Description")]
        [AllowHtml]
        [DataType("tinymce_full")]
        [UIHint("tinymce_full")]
        public string Description { get; set; }

        [Required]
        public int ParentCategoryID { get; set; }

        public byte[] Image { get; set; }
    }
}