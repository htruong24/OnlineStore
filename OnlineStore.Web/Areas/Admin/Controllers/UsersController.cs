using System;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using OnlineStore.Common;
using OnlineStore.Data.Entities;
using OnlineStore.Data.Infrastructure;
using OnlineStore.Data.Interfaces;
using OnlineStore.Services.BLL.Services;

namespace OnlineStore.Web.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService _userService;

        public UsersController()
        {
            this._userService = new UserService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        // GET: Admin/Users
        public ActionResult Index()
        {
            return View();
        }

        // GET: List of users
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

            _userService.Pagination = info;
            _userService.Filter = filter;
            var users = _userService.GetUsers();
            TempData["SortingPagingInfo"] = _userService.Pagination;

            return PartialView(users);
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _userService.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(user);
                _userService.CreateUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _userService.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(user);
                _userService.UpdateUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _userService.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            _userService.DeleteUser(id);
            return RedirectToAction("Index");
        }

        // Update: CreatedOn, CreatedBy, ModifiedOn, ModifiedBy
        public void UpdateDefaultProperties(User userModel)
        {
            var user = Session[CommonConstants.USER_SESSION] as User;
            // Create
            if (user != null)
            {
                if (string.IsNullOrEmpty(userModel.Id))
                {
                    userModel.CreatedById = user.Id;
                    userModel.CreatedOn = DateTime.Now;
                    userModel.ModifiedById = user.Id;
                    userModel.ModifiedOn = DateTime.Now;
                }
                // Update
                else
                {
                    userModel.ModifiedById = user.Id;
                    userModel.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
