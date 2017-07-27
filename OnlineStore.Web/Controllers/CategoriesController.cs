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
        private readonly BrandService _brandService;

        public CategoriesController()
        {
            this._categoryService = new CategoryService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._productService = new ProductService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._brandService = new BrandService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        private OnlineStoreDbContext db = new OnlineStoreDbContext();

        // GET: Categories
        public ActionResult Index(int? id)
        {
            var categories = db.Categories.Include(c => c.CreatedBy).Include(c => c.ModifiedBy);
            return View(categories.ToList());
        }

        // GET: List of products
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
            var products = _productService.GetProducts();
            TempData["SortingPagingInfo"] = _productService.Pagination;

            return PartialView(products);
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.CategoryId = id;
            var category = _categoryService.GetCategory(id);
            return View(category);
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

        //Filter
        public ActionResult _Filter(int? categoryId)
        {
            ViewBag.CategoryId = categoryId;

            return PartialView();
        }

        // Related brands
        public ActionResult _RelatedBrands(int? categoryId)
        {
            _brandService.Pagination = new SortingPagingInfo
            {
                SortField = "Name",
                SortDirection = "ascending",
                PageSize = 10
            };

            _brandService.Filter = new DefaultFilter()
            {
                CategoryId = categoryId == null ? 0 : categoryId.Value
            };

            var brands = _brandService.GetBrands();

            return PartialView(brands);
        }
    }
}
