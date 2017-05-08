using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Common
{
    public class SortingPagingInfo
    {
        public string SortField { get; set; }
        public string SortDirection { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public int TotalRows { get; set; }

        public SortingPagingInfo()
        {
            SortField = "Id";
            SortDirection = "ascending";
            PageSize = CommonConstants.PAGE_SIZE;
            CurrentPage = 1;
            TotalRows = 1;
        }
    }
}
