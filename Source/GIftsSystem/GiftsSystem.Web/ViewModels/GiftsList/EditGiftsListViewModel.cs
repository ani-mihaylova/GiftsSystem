namespace GiftsSystem.Web.ViewModels.GiftsList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using GiftSystem.Web.Infrastructure.Mapping;
    using GiftsSystem.Models;
    using System.ComponentModel.DataAnnotations;

    public class EditGiftsListViewModel : IMapFrom<GiftsSystem.Models.GiftsList>
    {
        public int ID { get; set; }

        [UIHint("StringLineText")]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}