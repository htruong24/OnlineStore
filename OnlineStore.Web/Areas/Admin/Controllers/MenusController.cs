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

namespace OnlineStore.Web.Areas.Admin.Controllers
{
    public class MenusController : Controller
    {
        private readonly MenuService _menuService;

        public MenusController()
        {
            this._menuService = new MenuService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        // GET: Admin/Menus
        public ActionResult Index()
        {
            return View();
        }

        // GET: List of menus
        public ActionResult _List(SortingPagingInfo info, DefaultFilter filter)
        {
            if (info.SortField == null)
            {
                info = new SortingPagingInfo
                {
                    SortField = "Name",
                    SortDirection = "ascending",
                    PageSize = CommonConstants.PAGE_SIZE,
                    CurrentPage = 1
                };
            }

            _menuService.Pagination = info;
            _menuService.Filter = filter;
            var menus = _menuService.GetMenus();
            TempData["SortingPagingInfo"] = _menuService.Pagination;

            return PartialView(menus);
        }

        // GET: Admin/Menus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var menu = _menuService.GetMenu(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: Admin/Menus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(menu);
                _menuService.CreateMenu(menu);
                return RedirectToAction("Index");
            }

            return View(menu);
        }

        // GET: Admin/Menus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var menu = _menuService.GetMenu(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Admin/Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(menu);
                _menuService.UpdateMenu(menu);
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        // GET: Admin/Menus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var menu = _menuService.GetMenu(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Admin/Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _menuService.DeleteMenu(id);
            return RedirectToAction("Index");
        }

        // Update: CreatedOn, CreatedBy, ModifiedOn, ModifiedBy
        public void UpdateDefaultProperties(Menu menu)
        {
            var user = Session[CommonConstants.USER_SESSION] as User;
            // Create
            if (user != null)
            {
                if (menu.Id == 0)
                {
                    menu.CreatedById = user.Id;
                    menu.CreatedOn = DateTime.Now;
                    menu.ModifiedById = user.Id;
                    menu.ModifiedOn = DateTime.Now;
                }
                // Update
                else
                {
                    menu.ModifiedById = user.Id;
                    menu.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
