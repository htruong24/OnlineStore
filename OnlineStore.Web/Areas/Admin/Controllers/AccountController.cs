using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineStore.Common;
using OnlineStore.Data.Infrastructure;
using OnlineStore.Data.Interfaces;
using OnlineStore.Services.BLL.Services;
using OnlineStore.Services.Models;

namespace OnlineStore.Web.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        private readonly AccountService _accountService;
        public AccountController()
        {
            this._accountService = new AccountService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        // GET: Admin/Account
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
            {
                ViewBag.Error = "Bạn chưa nhập đủ thông tin.";
            }
            else
            {
                var user = _accountService.GetUser(model.UserName, Encryptor.MD5Hash(model.Password));
                if (user != null)
                {
                    Session[CommonConstants.USER_SESSION] = user;
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng.";
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Session.Remove(CommonConstants.USER_SESSION);
            return RedirectToAction("Login", "Account");
        }
    }
}