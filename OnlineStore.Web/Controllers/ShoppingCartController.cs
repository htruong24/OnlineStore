using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineStore.Common;
using OnlineStore.Data.Entities;
using OnlineStore.Data.Infrastructure;
using OnlineStore.Data.Interfaces;
using OnlineStore.Services.BLL.Services;
using OnlineStore.Services.Models;

namespace OnlineStore.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly OrderDetailService _orderDetailService;

        public ShoppingCartController()
        {
            this._orderDetailService = new OrderDetailService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        // GET: Checkout
        public ActionResult Checkout()
        {
            return View();
        }

        // GET: List of products
        public ActionResult _List(SortingPagingInfo info, DefaultFilter filter)
        {
            var cartItems = (List<OrderDetail>)Session[CommonConstants.SHOPPING_CART_SESSION];

            return PartialView(cartItems);
        }

        // Shoppping Cart
        public ActionResult AddCartItem(OrderDetail orderDetail)
        {
            var jsonModel = new JsonModel<bool>
            {
                ErrorCode = "0",
                ErrorMessage = "",
                Result = true
            };

            var cartItems = (List<OrderDetail>)Session[CommonConstants.SHOPPING_CART_SESSION];
            cartItems.Add(orderDetail);

            return Json(jsonModel);
        }

        public ActionResult RemoveCartItem(int? orderDetailId)
        {
            var jsonModel = new JsonModel<bool>
            {
                ErrorCode = "0",
                ErrorMessage = "",
                Result = true
            };

            var cartItems = (List<OrderDetail>)Session[CommonConstants.SHOPPING_CART_SESSION];

            var removedCartItem = cartItems.FirstOrDefault(p => p.Id == orderDetailId);
            if (removedCartItem != null)
            {
                cartItems.Remove(removedCartItem);
                Session[CommonConstants.TEMPORARY_PRODUCT_PHOTO_SESSION] = removedCartItem;
            }

            return Json(jsonModel);
        }
    }
}
