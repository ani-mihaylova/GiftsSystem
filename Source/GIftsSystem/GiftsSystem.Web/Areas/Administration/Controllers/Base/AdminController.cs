namespace GiftsSystem.Web.Areas.Administration.Controllers.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using GiftsSystem.Common;
    using GiftsSystem.Data;
    using GiftsSystem.Web.Controllers;

    //[Authorize(Roles = GlobalConstants.AdminRoleName)]
    public abstract class AdminController : BaseController
    {
        public AdminController(IGiftsSystemData data)
            : base(data)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.data.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}