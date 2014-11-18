using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GiftSystem.Web.Infrastructure.Mapping;

namespace GiftsSystem.Web.ViewModels.Category
{
    public class CreateCategoryViewModel:IMapFrom<GiftsSystem.Models.Category>
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

        [Required]
        public int ParentCategoryID { get; set; }

        public HttpPostedFileBase UploadedImage { get; set; }

        public ICollection<SelectListItem> ParentCategories { get; set; }

    }
}