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

        public OrdersController()
        {
            this._orderService = new OrderService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
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
                _orderService.CreateOrder(order);
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
    }
}
