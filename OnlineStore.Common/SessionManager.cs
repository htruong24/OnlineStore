using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OnlineStore.Common
{
    public static class SessionManager
    {
        /// <summary>
        /// Gets or sets the filter for searching.
        /// </summary>
        /// <value>The filter.</value>
        public static DefaultFilter Filter
        {
            get
            {
                if (HttpContext.Current.Session["Filter"] == null)
                    HttpContext.Current.Session["Filter"] = new DefaultFilter();

                return HttpContext.Current.Session["Filter"] as DefaultFilter;
            }
            set
            {
                HttpContext.Current.Session["Filter"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the mode for Create/Edit action.
        /// </summary>
        /// <value>The mode.</value>
        public static int ActionMode
        {
            get
            {
                return (int)HttpContext.Current.Session["ActionMode"];
            }
            set
            {
                HttpContext.Current.Session["ActionMode"] = value;
            }
        }
    }
}
