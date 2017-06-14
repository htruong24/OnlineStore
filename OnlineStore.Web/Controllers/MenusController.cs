using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineStore.Data.Entities;
using OnlineStore.Data.Infrastructure;
using OnlineStore.Services.BLL.Services;
using OnlineStore.Data.Interfaces;
using OnlineStore.Common;

namespace OnlineStore.Web.Controllers
{
    public class MenusController : Controller
    {
        private readonly MenuService _menuService;

        public MenusController()
        {
            this._menuService = new MenuService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        // Get top menu
        public ActionResult _TopMenu()
        {
            _menuService.Pagination = new SortingPagingInfo
            {
                SortField = "Name",
                SortDirection = "ascending",
                PageSize = 0
            };

            ViewBag.Menus = _menuService.GetMenus();

            return PartialView();
        }
    }
}
