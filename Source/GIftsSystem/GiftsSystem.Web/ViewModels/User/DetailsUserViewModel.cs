namespace GiftsSystem.Web.ViewModels.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using GiftsSystem.Models;
    using GiftSystem.Web.Infrastructure.Mapping;

    public class DetailsUserViewModel:IMapFrom<ApplicationUser>
    {
        public string ID { get; set; }

        public string UserName { get; set; }

        public string ImagePath { get; set; }

        public ICollection<GiftsList> GiftsCollections { get; set; }
    }
}