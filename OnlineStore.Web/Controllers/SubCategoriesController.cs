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
using OnlineStore.Services.BLL.Services;
using OnlineStore.Data.Interfaces;

namespace OnlineStore.Web.Controllers
{
    public class SubCategoriesController : Controller
    {
        private readonly SubCategoryService _subCategoryService;
        private readonly ProductService _productService;
        private readonly BrandService _brandService;

        public SubCategoriesController()
        {
            this._subCategoryService = new SubCategoryService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._productService = new ProductService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._brandService = new BrandService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        private OnlineStoreDbContext db = new OnlineStoreDbContext();

        // GET: SubCategories
        public ActionResult Index(int? id)
        {
            var subCategories = db.SubCategories.Include(s => s.Category).Include(s => s.CreatedBy).Include(s => s.ModifiedBy);
            return View(subCategories.ToList());
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

        // GET: SubCategories/Details/5
        public ActionResult Details(int? id)
        {
            var subCategory = _subCategoryService.GetSubCategory(id);
            return View(subCategory);
        }

        // Related brands
        public ActionResult _RelatedBrands(int? subCategoryId)
        {
            _brandService.Pagination = new SortingPagingInfo
            {
                SortField = "Name",
                SortDirection = "ascending",
                PageSize = 10
            };

            _brandService.Filter = new DefaultFilter()
            {
                SubCategoryId = subCategoryId == null ? 0 : subCategoryId.Value
            };

            var brands = _brandService.GetBrands();

            return PartialView(brands);
        }

        //Filter
        public ActionResult _Filter(int? subCategoryId)
        {
            ViewBag.SubCategoryId = subCategoryId;

            return PartialView();
        }
    }
}
