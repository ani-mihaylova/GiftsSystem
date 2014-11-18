namespace GiftsSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Image
    {
        public int ID { get; set; }

        public byte[] Content { get; set; }

        public string FileExtension { get; set; }
    }
}
