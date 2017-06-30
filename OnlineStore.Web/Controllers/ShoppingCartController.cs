using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineStore.Common;
using OnlineStore.Data.Entities;
using OnlineStore.Services.Models;

namespace OnlineStore.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
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
