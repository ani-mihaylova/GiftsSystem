namespace GiftsSystem.Web.ViewModels.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using GiftsSystem.Models;
    using GiftSystem.Web.Infrastructure.Mapping;

    public class DetailsUserViewModel:IMapFrom<ApplicationUser>,IHaveCustomMappings
    {
        public string ID { get; set; }

        public string UserName { get; set; }

        public ICollection<GiftsList> GiftsCollections { get; set; }

        public int? ImageId { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<ApplicationUser, DetailsUserViewModel>()
                .ForMember(m => m.GiftsCollections, opt => opt.MapFrom(t => t.GiftsCollections))              
                .ReverseMap();
        }
    }
}