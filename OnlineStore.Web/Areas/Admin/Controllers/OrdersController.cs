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
    public class OrdersController : BaseController
    {
        private readonly OrderService _orderService;
        private readonly CityService _cityService;
        private readonly CustomerService _customerService;

        public OrdersController()
        {
            this._orderService = new OrderService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._customerService = new CustomerService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._cityService = new CityService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        // GET: Admin/Orders
        public ActionResult Index()
        {
            return View();
        }

        // GET: List of orders
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

            _orderService.Pagination = info;
            _orderService.Filter = filter;
            var orders = _orderService.GetOrders();
            TempData["SortingPagingInfo"] = _orderService.Pagination;

            return PartialView(orders);
        }

        // GET: Admin/Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = _orderService.GetOrder(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Admin/Orders/Create
        public ActionResult Create()
        {
            var paymentObjects = new string[] { "Khách vãng lai" };
            var paymentMethods = new string[] { "Tiền mặt khi nhận hàng", "Chuyển khoản" };
            var deliveryMethods = new string[] { "Tận nơi", "Qua bưu điện" };
            var statuses = new string[] { "Mới đặt hàng", "Đang giao hàng", "Đã giao hàng", "Hủy" };
            ViewBag.Customers = GetCustomers();
            ViewBag.Cities = GetCities();

            ViewBag.PaymentObjects = paymentObjects.Select(x => new SelectListItem { Text = x, Value = x }).ToList();
            ViewBag.PaymentMethods = paymentMethods.Select(x => new SelectListItem { Text = x, Value = x }).ToList();
            ViewBag.DeliveryMethods = deliveryMethods.Select(x => new SelectListItem { Text = x, Value = x }).ToList();
            ViewBag.Statuses = statuses.Select(x => new SelectListItem { Text = x, Value = x }).ToList();

            return View();
        }

        // POST: Admin/Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(order);
                _orderService.CreateOrder(order);
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Admin/Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var paymentObjects = new string[] { "Khách vãng lai" };
            var paymentMethods = new string[] { "Tiền mặt khi nhận hàng", "Chuyển khoản" };
            var deliveryMethods = new string[] { "Tận nơi", "Qua bưu điện" };
            var cities = new string[] { "Hồ Chí Minh", "Đà Nẵng", "Vũng Tàu", "Đồng Nai", "Buôn Ma Thuột" };
            var statuses = new string[] { "Mới đặt hàng", "Đang giao hàng", "Đã giao hàng", "Hủy" };

            ViewBag.PaymentObjects = paymentObjects.Select(x => new SelectListItem { Text = x, Value = x }).ToList();
            ViewBag.PaymentMethods = paymentMethods.Select(x => new SelectListItem { Text = x, Value = x }).ToList();
            ViewBag.DeliveryMethods = deliveryMethods.Select(x => new SelectListItem { Text = x, Value = x }).ToList();
            ViewBag.Cities = cities.Select(x => new SelectListItem { Text = x, Value = x }).ToList();
            ViewBag.Statuses = statuses.Select(x => new SelectListItem { Text = x, Value = x }).ToList();

            var order = _orderService.GetOrder(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(order);
                _orderService.UpdateOrder(order);
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Admin/Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = _orderService.GetOrder(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _orderService.DeleteOrder(id);
            return RedirectToAction("Index");
        }

        // Update: CreatedOn, CreatedBy, ModifiedOn, ModifiedBy
        public void UpdateDefaultProperties(Order order)
        {
            var user = Session[CommonConstants.USER_SESSION] as User;
            // Create
            if (user != null)
            {
                if (order.Id == 0)
                {
                    order.CreatedById = user.Id;
                    order.CreatedOn = DateTime.Now;
                    order.ModifiedById = user.Id;
                    order.ModifiedOn = DateTime.Now;
                }
                // Update
                else
                {
                    order.ModifiedById = user.Id;
                    order.ModifiedOn = DateTime.Now;
                }
            }
        }

        // Get customers
        public List<Customer> GetCustomers()
        {
            _customerService.Pagination = new SortingPagingInfo
            {
                SortField = "Name",
                SortDirection = "ascending",
                PageSize = 0
            };
            return _customerService.GetCustomers();
        }

        // Get cities
        public List<City> GetCities()
        {
            _cityService.Pagination = new SortingPagingInfo
            {
                SortField = "Name",
                SortDirection = "ascending",
                PageSize = 0
            };
            return _cityService.GetCities();
        }
    }
}
