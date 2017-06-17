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

namespace OnlineStore.Web.Controllers
{
    public class SubCategoriesController : Controller
    {
        private readonly SubCategoryService _subCategoryService;

        public SubCategoriesController()
        {
            this._subCategoryService = new SubCategoryService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        private OnlineStoreDbContext db = new OnlineStoreDbContext();

        // GET: SubCategories
        public ActionResult Index(int? id)
        {
            var subCategories = db.SubCategories.Include(s => s.Category).Include(s => s.CreatedBy).Include(s => s.ModifiedBy);
            return View(subCategories.ToList());
        }

        // GET: SubCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }

        // GET: SubCategories/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username");
            ViewBag.ModifiedById = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        // POST: SubCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,OrderNumber,CategoryId,CreatedOn,CreatedById,ModifiedOn,ModifiedById")] SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                db.SubCategories.Add(subCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", subCategory.CategoryId);
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", subCategory.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.Users, "Id", "Username", subCategory.ModifiedById);
            return View(subCategory);
        }

        // GET: SubCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", subCategory.CategoryId);
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", subCategory.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.Users, "Id", "Username", subCategory.ModifiedById);
            return View(subCategory);
        }

        // POST: SubCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,OrderNumber,CategoryId,CreatedOn,CreatedById,ModifiedOn,ModifiedById")] SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", subCategory.CategoryId);
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", subCategory.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.Users, "Id", "Username", subCategory.ModifiedById);
            return View(subCategory);
        }

        // GET: SubCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }

        // POST: SubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubCategory subCategory = db.SubCategories.Find(id);
            db.SubCategories.Remove(subCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
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
