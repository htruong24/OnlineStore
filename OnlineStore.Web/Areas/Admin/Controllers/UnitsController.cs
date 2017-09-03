using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineStore.Common;
using OnlineStore.Data.Entities;
using OnlineStore.Data.Infrastructure;
using OnlineStore.Data.Interfaces;
using OnlineStore.Services.BLL.Services;

namespace OnlineStore.Web.Areas.Admin.Controllers
{
    public class UnitsController : BaseController
    {
        private readonly UnitService _unitService;

        public UnitsController()
        {
            this._unitService = new UnitService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        // GET: Admin/Units
        public ActionResult Index()
        {
            return View();
        }

        // GET: List of units
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

            _unitService.Pagination = info;
            _unitService.Filter = filter;
            var units = _unitService.GetUnits();
            TempData["SortingPagingInfo"] = _unitService.Pagination;

            return PartialView(units);
        }

        // GET: Admin/Units/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var unit = _unitService.GetUnit(id);
            ;
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        // GET: Admin/Units/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Units/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Unit unit)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(unit);
                _unitService.CreateUnit(unit);
                return RedirectToAction("Index");
            }

            return View(unit);
        }

        // GET: Admin/Units/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var unit = _unitService.GetUnit(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        // POST: Admin/Units/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Unit unit)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(unit);
                _unitService.UpdateUnit(unit);
                return RedirectToAction("Index");
            }
            return View(unit);
        }

        // GET: Admin/Units/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var unit = _unitService.GetUnit(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        // POST: Admin/Units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _unitService.DeleteUnit(id);
            return RedirectToAction("Index");
        }

        // Update: CreatedOn, CreatedBy, ModifiedOn, ModifiedBy
        public void UpdateDefaultProperties(Unit unit)
        {
            var user = Session[CommonConstants.USER_SESSION] as User;
            // Create
            if (user != null)
            {
                if (unit.Id == 0)
                {
                    unit.CreatedById = user.Id;
                    unit.CreatedOn = DateTime.Now;
                    unit.ModifiedById = user.Id;
                    unit.ModifiedOn = DateTime.Now;
                }
                // Update
                else
                {
                    unit.ModifiedById = user.Id;
                    unit.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
