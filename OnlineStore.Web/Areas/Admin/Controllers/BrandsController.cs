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
    public class BrandsController : Controller
    {
        private readonly BrandService _brandService;

        public BrandsController()
        {
            this._brandService = new BrandService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        // GET: Admin/Brands
        public ActionResult Index()
        {
            return View();
        }

        // GET: List of brand
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

            _brandService.Pagination = info;
            _brandService.Filter = filter;
            var brands = _brandService.GetBrands();
            TempData["SortingPagingInfo"] = _brandService.Pagination;

            return PartialView(brands);
        }

        // GET: Admin/Brands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var brand = _brandService.GetBrand(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // GET: Admin/Brands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(brand);
                _brandService.CreateBrand(brand);
                return RedirectToAction("Index");
            }

            return View(brand);
        }

        // GET: Admin/Brands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var brand = _brandService.GetBrand(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Admin/Brands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Brand brand)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(brand);
                _brandService.UpdateBrand(brand);
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        // GET: Admin/Brands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var brand = _brandService.GetBrand(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Admin/Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _brandService.DeleteBrand(id);
            return RedirectToAction("Index");
        }

        // Update: CreatedOn, CreatedBy, ModifiedOn, ModifiedBy
        public void UpdateDefaultProperties(Brand brand)
        {
            var user = Session[CommonConstants.USER_SESSION] as User;
            // Create
            if (user != null)
            {
                if (brand.Id == 0)
                {
                    brand.CreatedBy = user.Id;
                    brand.CreatedOn = DateTime.Now;
                    brand.ModifiedBy = user.Id;
                    brand.ModifiedOn = DateTime.Now;
                }
                // Update
                else
                {
                    brand.ModifiedBy = user.Id;
                    brand.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
