using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineStore.Data.Infrastructure;
using OnlineStore.Data.Interfaces;
using OnlineStore.Services.BLL.Services;

namespace OnlineStore.Web.Controllers
{
    public class MenuController : Controller
    {
        private readonly ModuleService _moduleService;
        private readonly FunctionService _functionService;

        public MenuController()
        {
            this._moduleService = new ModuleService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._functionService = new FunctionService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TopMenu()
        {
            var modules = _moduleService.GetModules();
            return PartialView(modules);
        }

        public ActionResult SubMenu(string moduleId)
        {
            var functions = _functionService.GetFunctionsByModule(moduleId);
            return PartialView(functions);
        }

        public ActionResult SubMenuList(string moduleId)
        {
            var functions = _functionService.GetFunctionsByModule(moduleId);
            return PartialView(functions);
        }

        // GET: Menu/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Menu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Menu/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Menu/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Menu/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Menu/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Menu/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
