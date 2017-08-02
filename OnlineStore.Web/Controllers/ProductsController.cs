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
using OnlineStore.Data.Interfaces;
using OnlineStore.Services.BLL.Services;
using OnlineStore.Services.Models;
using OnlineStore.Common;

namespace OnlineStore.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;

        public ProductsController()
        {
            this._productService = new ProductService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _productService.GetProduct(id);
            var previewProducts = Session[CommonConstants.RECENTLY_PREVIEW_PRODUCT] == null ? new List<Product>() : (List<Product>)Session[CommonConstants.RECENTLY_PREVIEW_PRODUCT];
            previewProducts.Add(product);
            Session[CommonConstants.RECENTLY_PREVIEW_PRODUCT] = previewProducts;
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // Update number of clicks
        [HttpPost]
        public ActionResult UpdateNumberOfClicks(int productId)
        {
            var jsonModel = new JsonModel<bool>
            {
                ErrorCode = "0",
                ErrorMessage = "",
                Result = true
            };

            var product = _productService.GetProduct(productId);
            product.NumberOfClicks = product.NumberOfClicks + 1;
            _productService.UpdateProduct(product);
           
            return Json(jsonModel);
        }
    }
}
