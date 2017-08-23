using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineStore.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Product Detail", "san-pham/{metatitle}-{id}",
                new { controller = "Products", action = "Details", id = UrlParameter.Optional },
                new[] { "OnlineStore.Web.Controllers" }
                );

            routes.MapRoute("Category Detail", "danh-muc/{metatitle}-{id}",
                new { controller = "Categories", action = "Details", id = UrlParameter.Optional },
                new[] { "OnlineStore.Web.Controllers" }
                );

            routes.MapRoute("SubCategory Detail", "danh-muc-con/{metatitle}-{id}",
                new { controller = "SubCategories", action = "Details", id = UrlParameter.Optional },
                new[] { "OnlineStore.Web.Controllers" }
                );

            routes.MapRoute("Brand Detail", "thuong-hieu/{metatitle}-{id}",
                new { controller = "Brands", action = "Details", id = UrlParameter.Optional },
                new[] { "OnlineStore.Web.Controllers" }
                );

            routes.MapRoute("Shopping Cart", "gio-hang",
                new { controller = "ShoppingCart", action = "Index", id = UrlParameter.Optional },
                new[] { "OnlineStore.Web.Controllers" }
                );

            routes.MapRoute("Check Out", "thanh-toan",
                new { controller = "ShoppingCart", action = "Checkout", id = UrlParameter.Optional },
                new[] { "OnlineStore.Web.Controllers" }
                );

            routes.MapRoute("Về chúng tôi", "ve-chung-toi",
                new { controller = "Home", action = "AboutUs", id = UrlParameter.Optional },
                new[] { "OnlineStore.Web.Controllers" }
                );

            routes.MapRoute("Liên hệ", "lien-he",
                new { controller = "Home", action = "Contact", id = UrlParameter.Optional },
                new[] { "OnlineStore.Web.Controllers" }
                );

            routes.MapRoute("Chính sách", "chinh-sach",
                new { controller = "Home", action = "Policy", id = UrlParameter.Optional },
                new[] { "OnlineStore.Web.Controllers" }
                );

            routes.MapRoute("Kiểm tra đơn hàng", "kiem-tra-don-hang",
                new { controller = "Orders", action = "CheckOrder", id = UrlParameter.Optional },
                new[] { "OnlineStore.Web.Controllers" }
                );

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional},
                new[] { "OnlineStore.Web.Controllers" }
                );
        }
    }
}
