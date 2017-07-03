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
    public class ShipmentsController : BaseController
    {
        private readonly ShipmentService _shipmentService;

        public ShipmentsController()
        {
            this._shipmentService = new ShipmentService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        // GET: Admin/Shipments
        public ActionResult Index()
        {
            return View();
        }

        // GET: List of shipments
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

            _shipmentService.Pagination = info;
            _shipmentService.Filter = filter;
            var shipments = _shipmentService.GetShipments();
            TempData["SortingPagingInfo"] = _shipmentService.Pagination;

            return PartialView(shipments);
        }

        // GET: Admin/Shipments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var shipment = _shipmentService.GetShipment(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            return View(shipment);
        }

        // GET: Admin/Shipments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Shipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(shipment);
                _shipmentService.CreateShipment(shipment);
                return RedirectToAction("Index");
            }

            return View(shipment);
        }

        // GET: Admin/Shipments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var shipment = _shipmentService.GetShipment(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            return View(shipment);
        }

        // POST: Admin/Shipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(shipment);
                _shipmentService.UpdateShipment(shipment);
                return RedirectToAction("Index");
            }
            return View(shipment);
        }

        // GET: Admin/Shipments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var shipment = _shipmentService.GetShipment(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            return View(shipment);
        }

        // POST: Admin/Shipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _shipmentService.DeleteShipment(id);
            return RedirectToAction("Index");
        }

        // Update: CreatedOn, CreatedBy, ModifiedOn, ModifiedBy
        public void UpdateDefaultProperties(Shipment shipment)
        {
            var user = Session[CommonConstants.USER_SESSION] as User;
            // Create
            if (user != null)
            {
                if (shipment.Id == 0)
                {
                    shipment.CreatedBy = user.Id;
                    shipment.CreatedOn = DateTime.Now;
                    shipment.ModifiedBy = user.Id;
                    shipment.ModifiedOn = DateTime.Now;
                }
                // Update
                else
                {
                    shipment.ModifiedBy = user.Id;
                    shipment.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
