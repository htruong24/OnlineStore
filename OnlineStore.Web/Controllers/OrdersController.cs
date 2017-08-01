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
    public class OrdersController : Controller
    {
        private readonly OrderService _orderService;
        private readonly OrderDetailService _orderDetailService;

        public OrdersController()
        {
            this._orderService = new OrderService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._orderDetailService = new OrderDetailService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(order);
                var savedOrder = _orderService.CreateOrder(order);
                var orderDetails = (List<OrderDetail>)Session[CommonConstants.SHOPPING_CART_SESSION];
                foreach (var orderDetail in orderDetails)
                {
                    UpdateDefaultProperties(orderDetail);
                }
                _orderDetailService.CreateMultipleOrderDetails(orderDetails, savedOrder.Id);

                return RedirectToAction("Successful", "ShoppingCart");
            }
            return RedirectToAction("Checkout", "ShoppingCart");
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

        public ActionResult CheckOrder()
        {
            return View();
        }
    }
}
