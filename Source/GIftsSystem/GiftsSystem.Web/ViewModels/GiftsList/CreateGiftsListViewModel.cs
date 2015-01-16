namespace GiftsSystem.Web.ViewModels.GiftsList
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using GiftsSystem.Models;
    using GiftSystem.Web.Infrastructure.Mapping;

    public class CreateGiftsListViewModel:IMapFrom<GiftsList>
    {
        [Required]
        [UIHint("StringLineText")]
        public string Name { get; set; }
    }
}