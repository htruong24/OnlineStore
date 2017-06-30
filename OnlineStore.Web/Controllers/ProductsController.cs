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

namespace OnlineStore.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;

        private OnlineStoreDbContext db = new OnlineStoreDbContext();

        public ProductsController()
        {
            this._productService = new ProductService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.CreatedBy).Include(p => p.ModifiedBy);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Product product = db.Products.Find(id);
            //if (product == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(product);
            return View();
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username");
            ViewBag.ModifiedById = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ShortDescrition,Description,Price,Active,Color,StatusId,SubCategoryId,BrandId,UnitId,SupplierId,CreatedOn,CreatedById,ModifiedOn,ModifiedById")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", product.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.Users, "Id", "Username", product.ModifiedById);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", product.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.Users, "Id", "Username", product.ModifiedById);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ShortDescrition,Description,Price,Active,Color,StatusId,SubCategoryId,BrandId,UnitId,SupplierId,CreatedOn,CreatedById,ModifiedOn,ModifiedById")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", product.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.Users, "Id", "Username", product.ModifiedById);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
