﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Common
{
    public class DefaultFilter
    {
        public string Keyword { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int CategoryId { get; set; }
        public int CustomerId { get; set; }
        public int SubCategoryId { get; set; }
        public bool Featured { get; set; }
        public string PriceRange { get; set; }
        public string Brands { get; set; }
    }
}
