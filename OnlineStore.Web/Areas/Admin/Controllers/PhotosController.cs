using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
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
        public ActionResult Create(Photo photo, HttpPostedFileBase uploadedPhoto)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(photo);
                
                // Save photo
                if (uploadedPhoto != null && uploadedPhoto.ContentLength > 0)
                {
                    photo.Extension = Path.GetExtension(uploadedPhoto.FileName);
                    photo.FileSize = uploadedPhoto.ContentLength;

                    // Save file to directory
                    var path = HttpContext.Server.MapPath("~") + ConfigurationManager.AppSettings[PhotoDirectories.PRODUCT];
                    var fileName = GetFileName(uploadedPhoto.FileName, path, 0);
                    uploadedPhoto.SaveAs(path + fileName);

                    // Update properties
                    photo.Url = ConfigurationManager.AppSettings[PhotoDirectories.PRODUCT] + fileName;
                    photo.ThumbnailUrl = ConfigurationManager.AppSettings[PhotoDirectories.PRODUCT] + fileName;
                }

                _photoService.CreatePhoto(photo);
                return RedirectToAction("Index");
            }
           
            return View(photo);
        }

        // GET: Admin/Photos/Create_Multiple
        public ActionResult Create_Multiple()
        {
            return View();
        }

        // POST: Admin/Photos/Create_Multiple
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Multiple(HttpPostedFileBase[] uploadedPhotos)
        {
            if(uploadedPhotos != null && uploadedPhotos.Length > 0)
            {
                var insertPhotos = new List<Photo>();
                foreach(var uploadedPhoto in uploadedPhotos)
                {
                    // Save photo
                    if (uploadedPhoto.ContentLength > 0)
                    {
                        var photo = new Photo();

                        UpdateDefaultProperties(photo);

                        photo.Title = Path.GetFileNameWithoutExtension(uploadedPhoto.FileName);
                        photo.Description = "";
                        photo.Extension = Path.GetExtension(uploadedPhoto.FileName);
                        photo.FileSize = uploadedPhoto.ContentLength;

                        // Save file to directory
                        var path = HttpContext.Server.MapPath("~") + ConfigurationManager.AppSettings[PhotoDirectories.PRODUCT];
                        var fileName = GetFileName(uploadedPhoto.FileName, path, 0);
                        uploadedPhoto.SaveAs(path + fileName);

                        // Update properties
                        photo.Url = ConfigurationManager.AppSettings[PhotoDirectories.PRODUCT] + fileName;
                        photo.ThumbnailUrl = ConfigurationManager.AppSettings[PhotoDirectories.PRODUCT] + fileName;

                        insertPhotos.Add(photo);
                    }
                }
                _photoService.CreateMultiplePhotos(insertPhotos);

                return RedirectToAction("Index");
            }
            return View();
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
        public ActionResult DeleteConfirmed(int id, string Url)
        {
            _photoService.DeletePhoto(id);

            // Remove photo
            if (!string.IsNullOrEmpty(Url))
            {
                // Remove photo from directory
                string path = HttpContext.Server.MapPath("~") + Url;
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

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

        // Get new file name if duplicated
        public string GetFileName(string fileName, string filePath, int copyNumber)
        {
            if (System.IO.File.Exists(filePath + fileName))
            {
                ++copyNumber;
                fileName = fileName.Replace(".", "(" + copyNumber + ").");
                return GetFileName(fileName, filePath, copyNumber);
            }
            return fileName;
        }

        // GET: Admin/_SelectedPhotos  => Selected Photos
        public ActionResult _SelectedPhotos()
        {
            var photos = new List<Photo>();
            //var photo = new Photo
            //{
            //    Title = fileName,
            //    Extension = Path.GetExtension(path + uploadPhoto.FileName),
            //    FileSize = uploadPhoto.ContentLength
            //};
            //photos.Add(photo);
            Session[CommonConstants.PHOTO_SESSION] = photos;

            return PartialView();

        }

        // GET: List of selected photos
        public ActionResult _PhotoList(SortingPagingInfo info, DefaultFilter filter)
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

        // GET: Admin/_TemporaryPhotos  => Temporary Photos
        public ActionResult _TemporaryPhotos()
        {
            return PartialView();
        }

        public ActionResult AddTemporaryPhoto(int? photoId)
        {
            var photos = (List<Photo>)Session[CommonConstants.PHOTO_SESSION];

            if(photoId != null && photoId != 0)
            {
                var photo = _photoService.GetPhoto(photoId);
                photos.Add(photo);
                Session[CommonConstants.PHOTO_SESSION] = photos;
            }

            return PartialView("~/Areas/Admin/Views/Photos/_TemporaryPhotos.cshtml");
        }

        public ActionResult RemoveTemporaryPhoto(int? photoId)
        {
            var photos = (List<Photo>)Session[CommonConstants.PHOTO_SESSION];

            var removedPhoto = photos.FirstOrDefault(p => p.Id == photoId);

            if (removedPhoto != null)
            {
                photos.Remove(removedPhoto);
                Session[CommonConstants.PHOTO_SESSION] = photos;
            }

            return PartialView("~/Areas/Admin/Views/Photos/_TemporaryPhotos.cshtml");
        }

        // ---------------- POPUP ----------------
        // GET: Admin/Photos/Create
        public ActionResult Create_Popup()
        {
            return PartialView();
        }

        // POST: Admin/Photos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Popup(Photo photo, HttpPostedFileBase uploadedPhoto)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(photo);

                // Save photo
                if (uploadedPhoto != null && uploadedPhoto.ContentLength > 0)
                {
                    photo.Extension = Path.GetExtension(uploadedPhoto.FileName);
                    photo.FileSize = uploadedPhoto.ContentLength;

                    // Save file to directory
                    var path = HttpContext.Server.MapPath("~") + ConfigurationManager.AppSettings[PhotoDirectories.PRODUCT];
                    var fileName = GetFileName(uploadedPhoto.FileName, path, 0);
                    uploadedPhoto.SaveAs(path + fileName);

                    // Update properties
                    photo.Url = ConfigurationManager.AppSettings[PhotoDirectories.PRODUCT] + fileName;
                    photo.ThumbnailUrl = ConfigurationManager.AppSettings[PhotoDirectories.PRODUCT] + fileName;
                }

                _photoService.CreatePhoto(photo);
                return RedirectToAction("Index");
            }

            return PartialView(photo);
        }

        // GET: Admin/Photos/Create_Multiple
        public ActionResult Create_Multiple_Popup()
        {
            return PartialView();
        }

        // POST: Admin/Photos/Create_Multiple
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Multiple_Popup(HttpPostedFileBase[] uploadedPhotos)
        {
            if (uploadedPhotos != null && uploadedPhotos.Length > 0)
            {
                var insertPhotos = new List<Photo>();
                foreach (var uploadedPhoto in uploadedPhotos)
                {
                    // Save photo
                    if (uploadedPhoto.ContentLength > 0)
                    {
                        var photo = new Photo();

                        UpdateDefaultProperties(photo);

                        photo.Title = Path.GetFileNameWithoutExtension(uploadedPhoto.FileName);
                        photo.Description = "";
                        photo.Extension = Path.GetExtension(uploadedPhoto.FileName);
                        photo.FileSize = uploadedPhoto.ContentLength;

                        // Save file to directory
                        var path = HttpContext.Server.MapPath("~") + ConfigurationManager.AppSettings[PhotoDirectories.PRODUCT];
                        var fileName = GetFileName(uploadedPhoto.FileName, path, 0);
                        uploadedPhoto.SaveAs(path + fileName);

                        // Update properties
                        photo.Url = ConfigurationManager.AppSettings[PhotoDirectories.PRODUCT] + fileName;
                        photo.ThumbnailUrl = ConfigurationManager.AppSettings[PhotoDirectories.PRODUCT] + fileName;

                        insertPhotos.Add(photo);
                    }
                }
                _photoService.CreateMultiplePhotos(insertPhotos);

                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
