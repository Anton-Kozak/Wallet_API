﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wallets_API.DTO
{
    public class PhotoForReturnDTO
    {
        public string Url { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }
    }
}
