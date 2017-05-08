using System;
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

        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRows { get; set; }
    }
}
