﻿namespace GiftsSystem.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IOrderable
    {
        int OrderBy { get; set; }
    }
}