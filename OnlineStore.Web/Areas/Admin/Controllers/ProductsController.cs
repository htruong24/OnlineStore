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
using OnlineStore.Data.Interfaces;
using OnlineStore.Services.BLL.Services;

namespace OnlineStore.Web.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;
        private readonly BrandService _brandService;
        private readonly SubCategoryService _subCategoryService;
        private readonly UnitService _unitService;

        public ProductsController()
        {
            this._productService = new ProductService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._brandService = new BrandService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._subCategoryService = new SubCategoryService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._unitService = new UnitService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        // GET: Admin/Products
        public ActionResult Index()
        {
            return View();
        }

        // GET: List of products
        public ActionResult _List(SortingPagingInfo info, DefaultFilter filter)
        {
            if (info.SortField == null)
            {
                info = new SortingPagingInfo
                {
                    SortField = "Name",
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

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _productService.GetProduct(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            Session[CommonConstants.PHOTO_SESSION] = new List<Photo>();
            Session[CommonConstants.PRODUCT_PHOTO_SESSION] = new List<Photo>();
            Session[CommonConstants.TEMP_PHOTO_SESSION] = new List<Photo>();

            ViewBag.SubCategories = GetSubCategories();
            ViewBag.Brands = GetBrands();
            ViewBag.Units = GetUnits();

            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(product);
                _productService.CreateProduct(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _productService.GetProduct(id);
            ViewBag.SubCategories = GetSubCategories();
            ViewBag.Brands = GetBrands();
            ViewBag.Units = GetUnits();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(product);
                _productService.UpdateProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _productService.GetProduct(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }

        // Update: CreatedOn, CreatedBy, ModifiedOn, ModifiedBy
        public void UpdateDefaultProperties(Product product)
        {
            var user = Session[CommonConstants.USER_SESSION] as User;
            // Create
            if (user != null)
            {
                if (product.Id == 0)
                {
                    product.CreatedById = user.Id;
                    product.CreatedOn = DateTime.Now;
                    product.ModifiedById = user.Id;
                    product.ModifiedOn = DateTime.Now;
                }
                // Update
                else
                {
                    product.ModifiedById = user.Id;
                    product.ModifiedOn = DateTime.Now;
                }
            }
        }

        // Get brands
        public List<Brand> GetBrands()
        {
            _brandService.Pagination = new SortingPagingInfo
            {
                SortField = "Name",
                SortDirection = "ascending",
                PageSize = 0
            };
            return _brandService.GetBrands();
        }

        // Get Sub Categories
        public List<SubCategory> GetSubCategories()
        {
            _subCategoryService.Pagination = new SortingPagingInfo
            {
                SortField = "Name",
                SortDirection = "ascending",
                PageSize = 0
            };
            return _subCategoryService.GetSubCategories();
        }

        // Get Units
        public List<Unit> GetUnits()
        {
            _unitService.Pagination = new SortingPagingInfo
            {
                SortField = "Name",
                SortDirection = "ascending",
                PageSize = 0
            };
            return _unitService.GetUnits();
        }

        // Get Product photos
        public ActionResult _ProductPhotos()
        {
            var photos = (List<OnlineStore.Data.Entities.Photo>)Session[CommonConstants.PHOTO_SESSION];
            var productPhotos = (List<OnlineStore.Data.Entities.Photo>)Session[CommonConstants.PRODUCT_PHOTO_SESSION];

            Session[CommonConstants.PRODUCT_PHOTO_SESSION] = photos;

            return PartialView();
        }

        public ActionResult RemoveTemporaryProductPhoto(int? photoId)
        {
            var photos = (List<Photo>)Session[CommonConstants.PRODUCT_PHOTO_SESSION];

            var removedPhoto = photos.FirstOrDefault(p => p.Id == photoId);

            if (removedPhoto != null)
            {
                //photos.Remove(removedPhoto);
                removedPhoto.Status = PhotoStatus.DELETE;
                Session[CommonConstants.PRODUCT_PHOTO_SESSION] = photos;
                Session[CommonConstants.PHOTO_SESSION] = photos;
            }

            return PartialView("~/Areas/Admin/Views/Products/_ProductPhotos.cshtml");
        }

    }
}
