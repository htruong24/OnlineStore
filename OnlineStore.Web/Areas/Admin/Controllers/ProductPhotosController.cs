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

namespace OnlineStore.Web.Areas.Admin.Controllers
{
    public class ProductPhotosController : Controller
    {
        private readonly ProductPhotoService _productPhotoService;

        public ProductPhotosController()
        {
            this._productPhotoService = new ProductPhotoService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        private OnlineStoreDbContext db = new OnlineStoreDbContext();

        // GET: Admin/ProductPhotos
        public ActionResult Index()
        {
            var productPhotoes = db.ProductPhotoes.Include(p => p.CreatedBy).Include(p => p.ModifiedBy).Include(p => p.Photo).Include(p => p.Product);
            return View(productPhotoes.ToList());
        }

        // GET: Admin/ProductPhotos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductPhoto productPhoto = db.ProductPhotoes.Find(id);
            if (productPhoto == null)
            {
                return HttpNotFound();
            }
            return View(productPhoto);
        }

        // GET: Admin/ProductPhotos/Create
        public ActionResult Create()
        {
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username");
            ViewBag.ModifiedById = new SelectList(db.Users, "Id", "Username");
            ViewBag.PhotoId = new SelectList(db.Photos, "Id", "Title");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            return View();
        }

        // POST: Admin/ProductPhotos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,PhotoId,Featured,OrderNumber,CreatedOn,CreatedById,ModifiedOn,ModifiedById")] ProductPhoto productPhoto)
        {
            if (ModelState.IsValid)
            {
                db.ProductPhotoes.Add(productPhoto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", productPhoto.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.Users, "Id", "Username", productPhoto.ModifiedById);
            ViewBag.PhotoId = new SelectList(db.Photos, "Id", "Title", productPhoto.PhotoId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productPhoto.ProductId);
            return View(productPhoto);
        }

        // GET: Admin/ProductPhotos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductPhoto productPhoto = db.ProductPhotoes.Find(id);
            if (productPhoto == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", productPhoto.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.Users, "Id", "Username", productPhoto.ModifiedById);
            ViewBag.PhotoId = new SelectList(db.Photos, "Id", "Title", productPhoto.PhotoId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productPhoto.ProductId);
            return View(productPhoto);
        }

        // POST: Admin/ProductPhotos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,PhotoId,Featured,OrderNumber,CreatedOn,CreatedById,ModifiedOn,ModifiedById")] ProductPhoto productPhoto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productPhoto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", productPhoto.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.Users, "Id", "Username", productPhoto.ModifiedById);
            ViewBag.PhotoId = new SelectList(db.Photos, "Id", "Title", productPhoto.PhotoId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productPhoto.ProductId);
            return View(productPhoto);
        }

        // GET: Admin/ProductPhotos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductPhoto productPhoto = db.ProductPhotoes.Find(id);
            if (productPhoto == null)
            {
                return HttpNotFound();
            }
            return View(productPhoto);
        }

        // POST: Admin/ProductPhotos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductPhoto productPhoto = db.ProductPhotoes.Find(id);
            db.ProductPhotoes.Remove(productPhoto);
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
