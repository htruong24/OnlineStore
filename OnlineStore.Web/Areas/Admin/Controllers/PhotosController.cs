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

        // GET: Admin/Photos
        public ActionResult Index()
        {
            return View();
        }

        // GET: List of photos
        public ActionResult _List(SortingPagingInfo info, DefaultFilter filter)
        {
            if (info.SortField == null)
            {
                info = new SortingPagingInfo
                {
                    SortField = "Title",
                    SortDirection = "ascending",
                    PageSize = CommonConstants.PAGE_SIZE,
                    CurrentPage = 1
                };
            }

            _photoService.Pagination = info;
            _photoService.Filter = filter;
            var photos = _photoService.GetCategories();
            TempData["SortingPagingInfo"] = _photoService.Pagination;

            return PartialView(photos);
        }

        // GET: Admin/Photos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var photo = _photoService.GetPhoto(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // GET: Admin/Photos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Photos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Photo photo, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(photo);
                _photoService.CreatePhoto(photo);
                return RedirectToAction("Index");
            }
           
            return View(photo);
        }

        // GET: Admin/Photos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var photo = _photoService.GetPhoto(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Admin/Photos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Photo photo)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(photo);
                _photoService.UpdatePhoto(photo);
                return RedirectToAction("Index");
            }
            return View(photo);
        }

        // GET: Admin/Photos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var photo = _photoService.GetPhoto(id);
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
            _photoService.DeletePhoto(id);
            return RedirectToAction("Index");
        }

        // Update: CreatedOn, CreatedBy, ModifiedOn, ModifiedBy
        public void UpdateDefaultProperties(Photo photo)
        {
            var user = Session[CommonConstants.USER_SESSION] as User;
            // Create
            if (user != null)
            {
                if (photo.Id == 0)
                {
                    photo.CreatedById = user.Id;
                    photo.CreatedOn = DateTime.Now;
                    photo.ModifiedById = user.Id;
                    photo.ModifiedOn = DateTime.Now;
                }
                // Update
                else
                {
                    photo.ModifiedById = user.Id;
                    photo.ModifiedOn = DateTime.Now;
                }
            }
        }

        // Upload photos
        public ActionResult AddProductPhotos(int productId)
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
                        Title = fileName,
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

            var photos = (List<Photo>)Session[CommonConstants.PHOTO_SESSION];

            if (photos != null && photos.Any())
            {
                fileName = productId + "_" + fileName;

                var removedPhoto = photos.FirstOrDefault(d =>
                     d.Title == fileName);

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
            return Json(new { success }, JsonRequestBehavior.AllowGet);
        }
    }
}
