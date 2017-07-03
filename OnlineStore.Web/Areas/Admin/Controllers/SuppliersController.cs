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
    public class SuppliersController : BaseController
    {
        private readonly SupplierService _supplierService;

        public SuppliersController()
        {
            this._supplierService = new SupplierService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        // GET: Admin/Suppliers
        public ActionResult Index()
        {
            return View();
        }

        // GET: List of suppliers
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

            _supplierService.Pagination = info;
            _supplierService.Filter = filter;
            var modules = _supplierService.GetSuppliers();
            TempData["SortingPagingInfo"] = _supplierService.Pagination;

            return PartialView(modules);
        }

        // GET: Admin/Suppliers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var supplier = _supplierService.GetSupplier(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: Admin/Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(supplier);
                _supplierService.CreateSupplier(supplier);
                return RedirectToAction("Index");
            }

            return View(supplier);
        }

        // GET: Admin/Suppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var supplier = _supplierService.GetSupplier(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Admin/Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(supplier);
                _supplierService.UpdateSupplier(supplier);
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        // GET: Admin/Suppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var supplier = _supplierService.GetSupplier(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Admin/Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _supplierService.DeleteSupplier(id);
            return RedirectToAction("Index");
        }

        // Update: CreatedOn, CreatedBy, ModifiedOn, ModifiedBy
        public void UpdateDefaultProperties(Supplier supplier)
        {
            var user = Session[CommonConstants.USER_SESSION] as User;
            // Create
            if (user != null)
            {
                if (supplier.Id == 0)
                {
                    supplier.CreatedById = user.Id;
                    supplier.CreatedOn = DateTime.Now;
                    supplier.ModifiedById = user.Id;
                    supplier.ModifiedOn = DateTime.Now;
                }
                // Update
                else
                {
                    supplier.ModifiedById = user.Id;
                    supplier.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
