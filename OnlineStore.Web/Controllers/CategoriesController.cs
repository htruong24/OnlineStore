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

        public CategoriesController()
        {
            this._categoryService = new CategoryService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        private OnlineStoreDbContext db = new OnlineStoreDbContext();

        // GET: Categories
        public ActionResult Index(int? id)
        {
            var categories = db.Categories.Include(c => c.CreatedBy).Include(c => c.ModifiedBy);
            return View(categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username");
            ViewBag.ModifiedById = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,OrderNumber,CreatedOn,CreatedById,ModifiedOn,ModifiedById")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", category.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.Users, "Id", "Username", category.ModifiedById);
            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", category.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.Users, "Id", "Username", category.ModifiedById);
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,OrderNumber,CreatedOn,CreatedById,ModifiedOn,ModifiedById")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", category.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.Users, "Id", "Username", category.ModifiedById);
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
