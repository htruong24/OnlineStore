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
using OnlineStore.Services.Models;
using OnlineStore.Common;

namespace OnlineStore.Web.Areas.Admin.Controllers
{
    public class ProductPhotosController : BaseController
    {
        private readonly ProductPhotoService _productPhotoService;
        private readonly PhotoService _photoService;

        public ProductPhotosController()
        {
            this._productPhotoService = new ProductPhotoService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._photoService = new PhotoService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
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

        // Product Photos
        public ActionResult AddTemporaryProductPhoto(int? photoId)
        {
            var jsonModel = new JsonModel<bool>
            {
                ErrorCode = "-1",
                ErrorMessage = "Ảnh đã được chọn",
                Result = false
            };

            var temporaryProductPhotos = (List<ProductPhoto>)Session[CommonConstants.TEMPORARY_PRODUCT_PHOTO_SESSION];

            var existedPhoto = temporaryProductPhotos.Find(x => x.Photo.Id == photoId);
            if (existedPhoto != null && existedPhoto.Status != PhotoStatus.DELETE)
                return Json(jsonModel);

            if (photoId != null && photoId != 0)
            {
                var photo = _photoService.GetPhoto(photoId);

                var productPhoto = new ProductPhoto();
                productPhoto.Status = PhotoStatus.NEW;
                productPhoto.PhotoId = photo.Id;
                productPhoto.Photo = photo;
                temporaryProductPhotos.Add(productPhoto);

                Session[CommonConstants.TEMPORARY_PRODUCT_PHOTO_SESSION] = temporaryProductPhotos;
            }

            return PartialView("~/Areas/Admin/Views/ProductPhotos/_TemporaryProductPhotos.cshtml");
        }

        public ActionResult RemoveTemporaryProductPhoto(int? photoId)
        {
            var temporaryProductPhotos = (List<ProductPhoto>)Session[CommonConstants.TEMPORARY_PRODUCT_PHOTO_SESSION];

            var removedProductPhoto = temporaryProductPhotos.FirstOrDefault(p => p.Photo.Id == photoId);

            if (removedProductPhoto != null)
            {
                //photos.Remove(removedPhoto);
                removedProductPhoto.Status = PhotoStatus.DELETE;
                Session[CommonConstants.TEMPORARY_PRODUCT_PHOTO_SESSION] = temporaryProductPhotos;
            }

            return PartialView("~/Areas/Admin/Views/ProductPhotos/_TemporaryProductPhotos.cshtml");
        }

        public ActionResult _TemporaryProductPhotos()
        {
            return PartialView();
        }

        // Get Product photos

        public ActionResult _ProductPhotos()
        {
            var productPhotos = (List<ProductPhoto>)Session[CommonConstants.TEMPORARY_PRODUCT_PHOTO_SESSION];

            var tempProductPhotos = new List<ProductPhoto>();
            tempProductPhotos.AddRange(productPhotos);

            Session[CommonConstants.PRODUCT_PHOTO_SESSION] = tempProductPhotos;

            return PartialView();
        }

        public ActionResult _ProductPhotos_Details(int? productId)
        {
            var productPhotos = _productPhotoService.GetProductPhotos(productId);

            ViewBag.ProductPhotos = productPhotos;

            return PartialView();
        }

        public ActionResult _ProductPhotos_Edit(int? productId)
        {
            var productPhotos = _productPhotoService.GetProductPhotos(productId);

            Session[CommonConstants.PRODUCT_PHOTO_SESSION] = productPhotos;

            Session[CommonConstants.TEMPORARY_PRODUCT_PHOTO_SESSION] = productPhotos;

            ViewBag.ProductPhotos = productPhotos;

            return PartialView();
        }

        public ActionResult SetFeaturedProductPhoto(int? photoId)
        {
            var productPhotos = (List<ProductPhoto>)Session[CommonConstants.PRODUCT_PHOTO_SESSION];

            foreach(var productPhoto in productPhotos)
            {
                productPhoto.Featured = false;
            }

            var removedProductPhoto = productPhotos.FirstOrDefault(p => p.Id == photoId);

            if (removedProductPhoto != null)
            {
                //photos.Remove(removedPhoto);
                removedProductPhoto.Featured = true;

                var tempPhotos = new List<ProductPhoto>();
                tempPhotos.AddRange(productPhotos);

                Session[CommonConstants.PRODUCT_PHOTO_SESSION] = tempPhotos;
                Session[CommonConstants.TEMPORARY_PRODUCT_PHOTO_SESSION] = tempPhotos;
            }

            return PartialView("~/Areas/Admin/Views/ProductPhotos/_ProductPhotos.cshtml");
        }

        public ActionResult RemoveProductPhoto(int? photoId)
        {
            var productPhotos = (List<ProductPhoto>)Session[CommonConstants.PRODUCT_PHOTO_SESSION];

            var removedProductPhoto = productPhotos.FirstOrDefault(p => p.Id == photoId);

            if (removedProductPhoto != null)
            {
                //photos.Remove(removedPhoto);
                removedProductPhoto.Status = PhotoStatus.DELETE;

                var tempPhotos = new List<ProductPhoto>();
                tempPhotos.AddRange(productPhotos);

                Session[CommonConstants.PRODUCT_PHOTO_SESSION] = tempPhotos;
                Session[CommonConstants.TEMPORARY_PRODUCT_PHOTO_SESSION] = tempPhotos;
            }

            return PartialView("~/Areas/Admin/Views/ProductPhotos/_ProductPhotos.cshtml");
        }
    }
}
