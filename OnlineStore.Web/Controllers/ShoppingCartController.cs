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

        // GET: Successful
        public ActionResult Successful()
        {
            ViewBag.Message = Utils.SendMail("tqhieu91@gmail.com", "nexmitesting@gmail.com", "Cindy Shop", "", "Xác nhận đơn hàng", "Đặt hàng thành công! <br/> Đây là thông tin đơn hàng của bạn!");
            return View();
        }

        // GET: Checkout
        public ActionResult Checkout()
        {
            var cities = new string[] { "", "Hồ Chí Minh", "Đà Nẵng", "Vũng Tàu", "Đồng Nai", "Buôn Ma Thuột" };
            ViewBag.Cities = cities.Select(x => new SelectListItem { Text = x, Value = x }).ToList();
            var cartItems = Session[CommonConstants.SHOPPING_CART_SESSION] == null ? new List<OrderDetail>() : (List<OrderDetail>)Session[CommonConstants.SHOPPING_CART_SESSION];
            return View(cartItems);
        }

        // GET: List of products
        public ActionResult _List(SortingPagingInfo info, DefaultFilter filter)
        {
            var cartItems = Session[CommonConstants.SHOPPING_CART_SESSION] == null ? new List<OrderDetail>() : (List<OrderDetail>)Session[CommonConstants.SHOPPING_CART_SESSION];
            return PartialView(cartItems);
        }

        // Shoppping Cart
        public ActionResult AddCartItem(int? productId, int quantity)
        {
            var jsonModel = new JsonModel<bool>
            {
                ErrorCode = "0",
                ErrorMessage = "",
                Result = true
            };

            var cartItems = Session[CommonConstants.SHOPPING_CART_SESSION] == null ? new List<OrderDetail>() : (List<OrderDetail>)Session[CommonConstants.SHOPPING_CART_SESSION];
            var newCartItem = cartItems.Find(x => x.ProductId == productId);

            if (newCartItem != null)
            {
                newCartItem.Quantity += quantity;
            }
            else
            {
                newCartItem = new OrderDetail()
                {
                    ProductId = productId,
                    Quantity = quantity
                };
                var product = _productService.GetProduct((productId));
                newCartItem.Product = product;
                newCartItem.Price = product.Price;
                cartItems.Add(newCartItem);
            }
            Session[CommonConstants.SHOPPING_CART_SESSION] = cartItems;

            return Json(jsonModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveCartItem(int? orderDetailId)
        {
            var jsonModel = new JsonModel<bool>
            {
                ErrorCode = "0",
                ErrorMessage = "",
                Result = true
            };

            var cartItems = Session[CommonConstants.SHOPPING_CART_SESSION] == null ? new List<OrderDetail>() : (List<OrderDetail>)Session[CommonConstants.SHOPPING_CART_SESSION];

            var removedCartItem = cartItems.FirstOrDefault(p => p.Id == orderDetailId);
            if (removedCartItem != null)
            {
                cartItems.Remove(removedCartItem);
                Session[CommonConstants.TEMPORARY_PRODUCT_PHOTO_SESSION] = removedCartItem;
            }

            return Json(jsonModel);
        }

        public ActionResult UpdateShoppingCart(string productIds, string quantities)
        {
            var cartItems = Session[CommonConstants.SHOPPING_CART_SESSION] == null ? new List<OrderDetail>() : (List<OrderDetail>)Session[CommonConstants.SHOPPING_CART_SESSION];

            var jsonModel = new JsonModel<bool>
            {
                ErrorCode = "0",
                ErrorMessage = "",
                Result = true
            };

            string[] productIdArr = productIds.Split(';');
            string[] quantityArr = quantities.Split(';');

            var removeProducts = new List<int>();

            for (var i = 0; i < productIdArr.Count(); i++)
            {
                var cartItem = cartItems.FirstOrDefault(p => p.ProductId == int.Parse(productIdArr[i]));
                if (cartItem != null)
                {
                    cartItem.Quantity = int.Parse(quantityArr[i]);
                }
            }

            for (var j = 0; j < cartItems.Count; j++)
            {
                var existedItem = productIdArr.FirstOrDefault(x => int.Parse(x) == cartItems[j].ProductId);
                if (existedItem == null)
                {
                    cartItems.Remove(cartItems[j]);
                }
            }

            return Json(jsonModel, JsonRequestBehavior.AllowGet);
        }

        // Top Categories
        public ActionResult _MyCart()
        {
            var cartItems = Session[CommonConstants.SHOPPING_CART_SESSION] != null ? (List<OrderDetail>)Session[CommonConstants.SHOPPING_CART_SESSION] : new List<OrderDetail>();

            ViewBag.TotalAmount = cartItems.Count > 0 ? cartItems.Sum(x => x.Quantity*x.Price) : 0;

            return PartialView();
        }

        // Delivery Information
        public ActionResult DeliveryInformation()
        {
            var cities = new string[] { "", "Hồ Chí Minh", "Đà Nẵng", "Vũng Tàu", "Đồng Nai", "Buôn Ma Thuột" };
            ViewBag.Cities = cities.Select(x => new SelectListItem { Text = x, Value = x }).ToList();
            var cartItems = Session[CommonConstants.SHOPPING_CART_SESSION] == null ? new List<OrderDetail>() : (List<OrderDetail>)Session[CommonConstants.SHOPPING_CART_SESSION];
            return View(cartItems);
        }

        public ActionResult PaymentInformation()
        {
            return View();
        }
    }
}
