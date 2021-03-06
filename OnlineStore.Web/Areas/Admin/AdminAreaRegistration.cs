﻿using System.Web.Mvc;

namespace OnlineStore.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new {action = "Index", controller = "Home", id = UrlParameter.Optional},
                new[] {"OnlineStore.Web.Areas.Admin.Controllers"}
                );
        }
    }
}