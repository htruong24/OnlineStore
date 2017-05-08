using System;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OnlineStore.Common;
using OnlineStore.Data.Entities;
using OnlineStore.Services.Models;
using OnlineStore.Data.Infrastructure;
using OnlineStore.Data.Interfaces;
using OnlineStore.Services.BLL;
using OnlineStore.Services.BLL.Services;

namespace OnlineStore.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserService _userService;

        public UserController()
        {
            this._userService =  new UserService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        // GET: User
        public ActionResult Index()
        {
            return PartialView();
        }

        // GET: User/Create
        public ActionResult Create(string id)
        {
            var user = new User();
            if (!string.IsNullOrEmpty(id))
            {
                user = _userService.GetUser(id);
            }
            return PartialView(user);
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            var jsonModel = new JsonModel<bool>
            {
                ErrorCode = "0",
                ErrorMessage = "",
                Result = true
            };
            try
            {
                if (string.IsNullOrEmpty(user.Id))
                    _userService.CreateUser(user);
                else
                    _userService.UpdateUser(user);
            }
            catch (Exception ex)
            {
                jsonModel.ErrorCode = "-1";
                jsonModel.ErrorMessage = ex.ToString();
                jsonModel.Result = false;
            }
            return Json(jsonModel);
        }

        // GET: User/Details/5
        public ActionResult Details(string id)
        {
            var user = _userService.GetUser(id);
            return PartialView(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(string id)
        {
            var user = _userService.GetUser(id);
            return PartialView(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(User user)
        {
            var jsonModel = new JsonModel<bool>
            {
                ErrorCode = "0",
                ErrorMessage = "",
                Result = true,
            };
            if (ModelState.IsValid)
            {
                try
                {
                    _userService.UpdateUser(user);
                }
                catch (Exception ex)
                {
                    jsonModel.ErrorCode = "-1";
                    jsonModel.ErrorMessage = ex.ToString();
                    jsonModel.Result = false;
                }
            }
            return Json(jsonModel);
        }

        // GET: User/Delete/5
        public ActionResult Delete(string id)
        {
            var jsonModel = new JsonModel<bool>
            {
                ErrorCode = "0",
                ErrorMessage = "",
                Result = true
            };
            try
            {
                _userService.DeleteUser(id);
            }
            catch (Exception ex)
            {
                jsonModel.ErrorCode = "-1";
                jsonModel.ErrorMessage = ex.ToString();
                jsonModel.Result = false;
            }
            return Json(jsonModel);
        }

        public ActionResult UserList(SortingPagingInfo info, DefaultFilter filter)
        {
            if (info.SortField == null)
            {
                info = new SortingPagingInfo
                {
                    SortField = "Id",
                    SortDirection = "ascending",
                    PageSize = CommonConstants.PAGE_SIZE,
                    CurrentPage = 1
                };
            }
            _userService.Pagination = info;
            var users = _userService.GetUsers();
            TempData["SortingPagingInfo"] = _userService.Pagination;
            return PartialView(users);
        }
    }
}
