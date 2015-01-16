namespace GiftsSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PaymentInfo
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public RegionNames Region { get; set; }

        public Countries Country { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
