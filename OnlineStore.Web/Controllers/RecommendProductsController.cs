﻿using System;
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
    public class RecommendProductsController : Controller
    {
        private readonly RecommendProductService _recommendProductService;

        private OnlineStoreDbContext db = new OnlineStoreDbContext();

        public RecommendProductsController()
        {
            this._recommendProductService = new RecommendProductService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        // GET: RecommendProducts
        public ActionResult Index()
        {
            return View(db.RecommendProducts.ToList());
        }

        // GET: RecommendProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecommendProduct recommendProduct = db.RecommendProducts.Find(id);
            if (recommendProduct == null)
            {
                return HttpNotFound();
            }
            return View(recommendProduct);
        }

        // GET: RecommendProducts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecommendProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,NumberOfClicks")] RecommendProduct recommendProduct)
        {
            if (ModelState.IsValid)
            {
                db.RecommendProducts.Add(recommendProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recommendProduct);
        }

        // GET: RecommendProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecommendProduct recommendProduct = db.RecommendProducts.Find(id);
            if (recommendProduct == null)
            {
                return HttpNotFound();
            }
            return View(recommendProduct);
        }

        // POST: RecommendProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,NumberOfClicks")] RecommendProduct recommendProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recommendProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recommendProduct);
        }

        // GET: RecommendProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecommendProduct recommendProduct = db.RecommendProducts.Find(id);
            if (recommendProduct == null)
            {
                return HttpNotFound();
            }
            return View(recommendProduct);
        }

        // POST: RecommendProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RecommendProduct recommendProduct = db.RecommendProducts.Find(id);
            db.RecommendProducts.Remove(recommendProduct);
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

        // Update recommend product
        [HttpPost]
        public ActionResult UpdateRecommendProduct(int productId)
        {
            var jsonModel = new JsonModel<bool>
            {
                ErrorCode = "0",
                ErrorMessage = "",
                Result = true
            };

            var recommendProduct = _recommendProductService.GetRecommendProduct(productId);

            if (recommendProduct != null)
            {
                recommendProduct.NumberOfClicks = recommendProduct.NumberOfClicks + 1;
                _recommendProductService.UpdateRecommendProduct(recommendProduct);
            }
            else
            {
                recommendProduct = new RecommendProduct
                {
                    ProductId = productId,
                    NumberOfClicks = 1
                };
                _recommendProductService.CreateRecommendProduct(recommendProduct);
            }
            return Json(jsonModel);
        }
    }
}
