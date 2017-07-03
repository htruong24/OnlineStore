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
    public class OrderDetailsController : BaseController
    {
        private readonly OrderDetailService _orderDetailService;

        public OrderDetailsController()
        {
            this._orderDetailService = new OrderDetailService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        // GET: Admin/OrderDetails
        public ActionResult Index()
        {
            return View();
        }

        // GET: List of order details
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

            _orderDetailService.Pagination = info;
            _orderDetailService.Filter = filter;
            var orderDetails = _orderDetailService.GetOrderDetails();
            TempData["SortingPagingInfo"] = _orderDetailService.Pagination;

            return PartialView(orderDetails);
        }

        // GET: Admin/OrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = _orderDetailService.GetOrderDetail(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(orderDetail);
                _orderDetailService.CreateOrderDetail(orderDetail);
                return RedirectToAction("Index");
            }

            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = _orderDetailService.GetOrderDetail(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: Admin/OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(orderDetail);
                _orderDetailService.UpdateOrderDetail(orderDetail);
                return RedirectToAction("Index");
            }
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = _orderDetailService.GetOrderDetail(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: Admin/OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _orderDetailService.DeleteOrderDetail(id);
            return RedirectToAction("Index");
        }

        // Update: CreatedOn, CreatedBy, ModifiedOn, ModifiedBy
        public void UpdateDefaultProperties(OrderDetail orderDetail)
        {
            var user = Session[CommonConstants.USER_SESSION] as User;
            // Create
            if (user != null)
            {
                if (orderDetail.Id == 0)
                {
                    orderDetail.CreatedById = user.Id;
                    orderDetail.CreatedOn = DateTime.Now;
                    orderDetail.ModifiedById = user.Id;
                    orderDetail.ModifiedOn = DateTime.Now;
                }
                // Update
                else
                {
                    orderDetail.ModifiedById = user.Id;
                    orderDetail.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
