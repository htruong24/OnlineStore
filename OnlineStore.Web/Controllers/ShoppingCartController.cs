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
        private readonly ProductService _productService;

        public ShoppingCartController()
        {
            this._orderDetailService = new OrderDetailService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._productService = new ProductService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
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
        public ActionResult AddCartItem(int? productId)
        {
            var jsonModel = new JsonModel<bool>
            {
                ErrorCode = "0",
                ErrorMessage = "",
                Result = true
            };

            var cartItems = (List<OrderDetail>)Session[CommonConstants.SHOPPING_CART_SESSION];

            var newCartItem = cartItems.Find(x => x.ProductId == productId);

            if (newCartItem != null)
            {
                newCartItem.Quantity += 1;
            }
            else
            {
                newCartItem = new OrderDetail();
                newCartItem.ProductId = productId;
                newCartItem.Quantity = 1;
                newCartItem.Price = _productService.GetProduct((productId)).Price;
                cartItems.Add(newCartItem);
            }

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

        // Top Categories
        public ActionResult _MiniCart()
        {
            var cartItems = (List<OrderDetail>)Session[CommonConstants.SHOPPING_CART_SESSION];

            ViewBag.TotalAmount = cartItems.Count > 0 ? cartItems.Sum(x => x.Quantity*x.Price) : 0;

            return PartialView();
        }
    }
}
