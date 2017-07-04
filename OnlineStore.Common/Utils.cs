using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Common
{
    public static class Utils
    {
        public static string ToCurrency(this decimal? price)
        {
            var convertPrice = price ?? 0;
            return convertPrice.ToString("N0").Replace(',', '.');
        }

        public static string ToCurrency(this decimal price)
        {
            return price.ToString("N0").Replace(',', '.');
        }
    }
}
