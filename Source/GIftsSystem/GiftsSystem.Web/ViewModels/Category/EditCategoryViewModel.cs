﻿namespace GiftsSystem.Web.ViewModels.Category
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using GiftSystem.Web.Infrastructure.Mapping;

    public class EditCategoryViewModel:IMapFrom<GiftsSystem.Models.Category>
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public ICollection<GiftsSystem.Models.Product> Products { get; set; }

        [AllowHtml]
        [DataType("tinymce_full")]
        [UIHint("tinymce_full")]
        public string Description { get; set; }
    }
}