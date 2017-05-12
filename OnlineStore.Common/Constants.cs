using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Common
{
    public class CommonConstants
    {
        public static string USER_SESSION = "USER_SESSION";
        public static string CONNECTION_STRING = "OnlineStoreDbContext";
        public static string DEFAULT_PASSWORD = "123";
        public static int PAGE_SIZE = 10;
    }

    public class AutomaticTable
    {
        public static string USER = "USER";
    }

    public class DatetimeFormat
    {
        public const string DDMMYY = "dd-MM-yyyy";
    }

    public class CommonImages
    {
        public const string Avatar = "~/Content/images/placeholder/avatar.png";
    }

    public class StatusGroups
    {
        public static int STATUS = 1;
    }
}
