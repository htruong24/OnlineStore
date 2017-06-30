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
    public class CategoriesController : Controller
    {
        private readonly CategoryService _categoryService;
        private readonly ProductService _productService;

        public CategoriesController()
        {
            this._categoryService = new CategoryService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._productService = new ProductService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        private OnlineStoreDbContext db = new OnlineStoreDbContext();

        // GET: Categories
        public ActionResult Index(int? id)
        {
            var categories = db.Categories.Include(c => c.CreatedBy).Include(c => c.ModifiedBy);
            return View(categories.ToList());
        }

        // GET: List of photos
        public ActionResult _List(SortingPagingInfo info, DefaultFilter filter)
        {
            if (info.SortField == null)
            {
                info = new SortingPagingInfo
                {
                    SortField = "Title",
                    SortDirection = "ascending",
                    PageSize = CommonConstants.PAGE_SIZE,
                    CurrentPage = 1
                };
            }

            _productService.Pagination = info;
            _productService.Filter = filter;
            var photos = _productService.GetProducts();
            TempData["SortingPagingInfo"] = _productService.Pagination;

            return PartialView(photos);
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.ProductId = id;
            return View();
        }

        // Get Sub Categories
        public ActionResult _CategorySelection()
        {
            _categoryService.Pagination = new SortingPagingInfo
            {
                SortField = "OrderNumber",
                SortDirection = "ascending",
                PageSize = 0
            };

            ViewBag.Categories = _categoryService.GetCategories();

            return PartialView();
        }

        // Main Menu
        public ActionResult _MainMenu()
        {
            _categoryService.Pagination = new SortingPagingInfo
            {
                SortField = "OrderNumber",
                SortDirection = "ascending",
                PageSize = 0
            };

            ViewBag.Categories = _categoryService.GetCategories();

            return PartialView();
        }
    }
}
