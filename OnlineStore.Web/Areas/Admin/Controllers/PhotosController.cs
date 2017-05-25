using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
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
    public class PhotosController : Controller
    {
        private readonly PhotoService _photoService;

        public PhotosController()
        {
            this._photoService = new PhotoService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        private OnlineStoreDbContext db = new OnlineStoreDbContext();

        // Upload photos
        public ActionResult AddProductPhotos(int productId, string pathFile = "PathToIncomeDocument")
        {
            try
            {
                var uploadPhoto = Request.Files.Count > 0 ? Request.Files[0] : null;

                if (uploadPhoto != null && uploadPhoto.ContentLength > 0)
                {
                    // Save file to directory
                    var path = HttpContext.Server.MapPath("~") + ConfigurationManager.AppSettings[PhotoDirectories.PRODUCT];
                    var fileName = productId + "_" + uploadPhoto.FileName;
                    uploadPhoto.SaveAs(path + fileName);

                    // Save item to session
                    var photos = new List<Photo>();
                    var photo = new Photo
                    {
                        ProductId = productId,
                        Tite = fileName,
                        Extension = Path.GetExtension(path + uploadPhoto.FileName),
                        FileSize = uploadPhoto.ContentLength
                    };
                    photos.Add(photo);
                    Session[CommonConstants.PHOTO_SESSION] = photos;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(new
            {
                IsSuccess = false
            });
        }

        public ActionResult RemoveProductPhotos(int productId, string fileName, int fileSize)
        {
            var success = false;

            var photos = (List<Photo>) Session[CommonConstants.PHOTO_SESSION];

            if (photos != null && photos.Any())
            {
                fileName = productId + "_" + fileName;

                var removedPhoto = photos.FirstOrDefault(d =>
                    d.ProductId == productId
                    && d.Tite == fileName);

                if (removedPhoto != null)
                {
                    // Remove photo from directory
                    string path = HttpContext.Server.MapPath("~") +
                                  ConfigurationManager.AppSettings[PhotoDirectories.PRODUCT] + fileName;
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    // Remove file from session
                    photos.Remove(removedPhoto);
                    Session[CommonConstants.PHOTO_SESSION] = photos;

                    success = true;
                }
            }
            return Json(new {success}, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/Photos
        public ActionResult Index()
        {
            var photos = db.Photos.Include(p => p.CreatedBy).Include(p => p.ModifiedBy);
            return View(photos.ToList());
        }

        // GET: Admin/Photos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // GET: Admin/Photos/Create
        public ActionResult Create()
        {
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username");
            ViewBag.ModifiedById = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        // POST: Admin/Photos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,Url,ThumbnailUrl,Tite,Description,IsFeatured,CreatedOn,CreatedById,ModifiedOn,ModifiedById")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Photos.Add(photo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", photo.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.Users, "Id", "Username", photo.ModifiedById);
            return View(photo);
        }

        // GET: Admin/Photos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", photo.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.Users, "Id", "Username", photo.ModifiedById);
            return View(photo);
        }

        // POST: Admin/Photos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,Url,ThumbnailUrl,Tite,Description,IsFeatured,CreatedOn,CreatedById,ModifiedOn,ModifiedById")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", photo.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.Users, "Id", "Username", photo.ModifiedById);
            return View(photo);
        }

        // GET: Admin/Photos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Admin/Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = db.Photos.Find(id);
            db.Photos.Remove(photo);
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
