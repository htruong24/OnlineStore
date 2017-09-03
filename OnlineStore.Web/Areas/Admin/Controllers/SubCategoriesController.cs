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
    public class SubCategoriesController : BaseController
    {
        private readonly SubCategoryService _subCategoryService;
        private readonly CategoryService _categoryService;

        public SubCategoriesController()
        {
            this._subCategoryService = new SubCategoryService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._categoryService = new CategoryService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        // GET: Admin/SubCategories
        public ActionResult Index()
        {
            return View();
        }

        // GET: List of sub categories
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

            _subCategoryService.Pagination = info;
            _subCategoryService.Filter = filter;
            var subCategories = _subCategoryService.GetSubCategories();
            TempData["SortingPagingInfo"] = _subCategoryService.Pagination;

            return PartialView(subCategories);
        }

        // GET: Admin/SubCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var subCategory = _subCategoryService.GetSubCategory(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }

        // GET: Admin/SubCategories/Create
        public ActionResult Create()
        {
            ViewBag.Categories = GetCategories();
            return View();
        }

        // POST: Admin/SubCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(subCategory);
                _subCategoryService.CreateSubCategory(subCategory);
                return RedirectToAction("Index");
            }

            return View(subCategory);
        }

        // GET: Admin/SubCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var subCategory = _subCategoryService.GetSubCategory(id);
            ViewBag.Categories = GetCategories();
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }

        // POST: Admin/SubCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(subCategory);
                _subCategoryService.UpdateSubCategory(subCategory);
                return RedirectToAction("Index");
            }
            
            return View(subCategory);
        }

        // GET: Admin/SubCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var subCategory = _subCategoryService.GetSubCategory(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }

        // POST: Admin/SubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _subCategoryService.DeleteSubCategory(id);
            return RedirectToAction("Index");
        }

        // Update: CreatedOn, CreatedBy, ModifiedOn, ModifiedBy
        public void UpdateDefaultProperties(SubCategory subCategory)
        {
            var user = Session[CommonConstants.USER_SESSION] as User;
            // Create
            if (user != null)
            {
                if (subCategory.Id == 0)
                {
                    subCategory.CreatedById = user.Id;
                    subCategory.CreatedOn = DateTime.Now;
                    subCategory.ModifiedById = user.Id;
                    subCategory.ModifiedOn = DateTime.Now;
                }
                // Update
                else
                {
                    subCategory.ModifiedById = user.Id;
                    subCategory.ModifiedOn = DateTime.Now;
                }
            }
        }

        // Get categories
        public List<Category> GetCategories()
        {
            _categoryService.Pagination = new SortingPagingInfo
            {
                SortField = "Name",
                SortDirection = "ascending",
                PageSize = 0
            };
            return _categoryService.GetCategories();
        }
    }
}
