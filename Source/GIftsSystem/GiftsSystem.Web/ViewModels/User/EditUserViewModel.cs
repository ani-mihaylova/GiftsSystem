namespace GiftsSystem.Web.ViewModels.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using GiftsSystem.Models;
    using GiftSystem.Web.Infrastructure.Mapping;

    public class EditUserViewModel : IMapFrom<ApplicationUser>
    {
        public string ID { get; set; }

        public string UserName { get; set; }

        public HttpPostedFileBase UploadedImage { get; set; }

        public ICollection<GiftsList> GiftsCollections { get; set; }
    }
}