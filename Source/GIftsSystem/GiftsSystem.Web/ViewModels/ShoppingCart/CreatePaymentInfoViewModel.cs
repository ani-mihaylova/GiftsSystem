namespace GiftsSystem.Web.ViewModels.ShoppingCart
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using GiftsSystem.Models;
    using GiftSystem.Web.Infrastructure.Mapping;

    public class CreatePaymentInfoViewModel : IMapFrom<PaymentInfo>
    {
        [Required]
        [UIHint("StringLineText")]
        public string FirstName { get; set; }

        [Required]
        [UIHint("StringLineText")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required]
        [UIHint("StringLineText")]
        public string City { get; set; }

        public string RegionName { get; set; }

        public string CountryName { get; set; }

        public ICollection<SelectListItem> Regions { get; set; }

        public ICollection<SelectListItem> Countries { get; set; }
    }
}