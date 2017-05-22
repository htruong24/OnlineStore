using System;
using System.Net;
using System.Web.Mvc;
using OnlineStore.Common;
using OnlineStore.Data.Entities;
using OnlineStore.Data.Infrastructure;
using OnlineStore.Data.Interfaces;
using OnlineStore.Services.BLL.Services;

namespace OnlineStore.Web.Areas.Admin.Controllers
{
    public class ModulesController : Controller
    {
        private readonly ModuleService _moduleService;

        public ModulesController()
        {
            this._moduleService = new ModuleService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        // GET: Admin/Modules
        public ActionResult Index()
        {
            return View();
        }

        // GET: List of modules
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

            _moduleService.Pagination = info;
            _moduleService.Filter = filter;
            var modules = _moduleService.GetModules();
            TempData["SortingPagingInfo"] = _moduleService.Pagination;

            return PartialView(modules);
        }

        // GET: Admin/Modules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var module = _moduleService.GetModule(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // GET: Admin/Modules/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Modules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Module module)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(module);
                _moduleService.CreateModule(module);
                return RedirectToAction("Index");
            }
            return View(module);
        }

        // GET: Admin/Modules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var module = _moduleService.GetModule(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: Admin/Modules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Module module)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(module);
                _moduleService.UpdateModule(module);
                return RedirectToAction("Index");
            }
            return View(module);
        }

        // GET: Admin/Modules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var module = _moduleService.GetModule(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: Admin/Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _moduleService.DeleteModule(id);
            return RedirectToAction("Index");
        }

        public ActionResult _LeftNavigation()
        {
            var info = new SortingPagingInfo
            {
                SortField = "OrderNumber",
                SortDirection = "ascending",
                PageSize = 0,
                CurrentPage = 1
            };

            _moduleService.Pagination = info;
            var modules = _moduleService.GetModules();
            return PartialView(modules);
        }

        // Update: CreatedOn, CreatedBy, ModifiedOn, ModifiedBy
        public void UpdateDefaultProperties(Module module)
        {
            var user = Session[CommonConstants.USER_SESSION] as User;
            // Create
            if (user != null)
            {
                if (module.Id == 0)
                {
                    module.CreatedById = user.Id;
                    module.CreatedOn = DateTime.Now;
                    module.ModifiedById = user.Id;
                    module.ModifiedOn = DateTime.Now;
                }
                // Update
                else
                {
                    module.ModifiedById = user.Id;
                    module.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
