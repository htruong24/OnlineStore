using OnlineStore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineStore.Data.Entities;

namespace OnlineStore.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (Session[CommonConstants.USER_SESSION] == null)
            {
                Response.Redirect("~/Account/Login");
            }
        }

        public string GetUserId()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                var user = Session[CommonConstants.USER_SESSION] as User;
                return user.Id;
            }
            return string.Empty;
        }
    }
}