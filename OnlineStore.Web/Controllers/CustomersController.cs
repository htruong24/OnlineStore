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
using OnlineStore.Services.BLL.Services;
using OnlineStore.Data.Interfaces;
using OnlineStore.Common;

namespace OnlineStore.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomerService _customerService;
        private readonly OrderService _orderService;
        private readonly CityService _cityService;
        private readonly ShippingAddressService _shippingAddressService;

        public CustomersController()
        {
            this._cityService = new CityService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._customerService = new CustomerService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._orderService = new OrderService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._shippingAddressService = new ShippingAddressService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        private OnlineStoreDbContext db = new OnlineStoreDbContext();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.CreatedBy).Include(c => c.ModifiedBy);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Password = Encryptor.MD5Hash(customer.Password);
                UpdateDefaultProperties(customer);
                _customerService.CreateCustomer(customer);
                Session[CommonConstants.CUSTOMER_SESSION] = customer;
                return RedirectToAction("Account", "Customers");
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Account()
        {
            var customer = Session[CommonConstants.CUSTOMER_SESSION];
            return View(customer);
        }

        // Login
        public ActionResult Login()
        {
            var customer = Session[CommonConstants.CUSTOMER_SESSION];
            return View(customer);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Customer customer, string returnUrl)
        {
            if (string.IsNullOrEmpty(customer.Email) || string.IsNullOrEmpty(customer.Password))
            {
                ViewBag.Error = "Bạn chưa nhập đủ thông tin.";
            }
            else
            {
                var returnCustomer = _customerService.GetCustomer(customer.Email, Encryptor.MD5Hash(customer.Password));
                if (returnCustomer != null)
                {
                    Session[CommonConstants.CUSTOMER_SESSION] = returnCustomer;
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Account", "Customers");
                }
                ViewBag.Error = "Email hoặc mật khẩu không đúng.";
            }
            return View(customer);
        }

        public ActionResult Logout()
        {
            Session.Remove(CommonConstants.CUSTOMER_SESSION);
            return RedirectToAction("Login", "Customers");
        }

        public ActionResult _Menu()
        {
            var customer = Session[CommonConstants.CUSTOMER_SESSION] as Customer;
            return PartialView(customer);
        }

        public ActionResult ChangeEmail()
        {
            var customer = Session[CommonConstants.CUSTOMER_SESSION] as Customer;
            return View(customer);
        }

        [HttpPost]
        public ActionResult ChangeEmail(Customer customer)
        {
            var currentCustomer = Session[CommonConstants.CUSTOMER_SESSION] as Customer;
            if (currentCustomer.Email == customer.CurrentEmail)
            {
                var newEmail = customer.NewEmail;
                customer = currentCustomer;
                customer.Email = newEmail;
                UpdateDefaultProperties(customer);
                _customerService.UpdateCustomer(customer);
                Session[CommonConstants.CUSTOMER_SESSION] = customer;
                return RedirectToAction("Account", "Customers");
            }
            return View(customer);
        }

        public ActionResult ChangePassword()
        {
            var customer = Session[CommonConstants.CUSTOMER_SESSION] as Customer;
            return View(customer);
        }

        [HttpPost]
        public ActionResult ChangePassword(Customer customer)
        {
            var currentCustomer = Session[CommonConstants.CUSTOMER_SESSION] as Customer;
            if (currentCustomer.Password == Encryptor.MD5Hash(customer.CurrentPassword))
            {
                var newPassword = customer.NewPassword;
                customer = currentCustomer;
                customer.Password = Encryptor.MD5Hash(newPassword);
                UpdateDefaultProperties(customer);
                _customerService.UpdateCustomer(customer);
                Session[CommonConstants.CUSTOMER_SESSION] = customer;
                return RedirectToAction("Account", "Customers");
            }
            return View(customer);
        }

        public ActionResult Orders()
        {
            return View();
        }

        // GET: List of orders
        public ActionResult _OrdersList(SortingPagingInfo info, DefaultFilter filter)
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

            var customer = Session[CommonConstants.CUSTOMER_SESSION] as Customer;
            _orderService.Pagination = info;
            _orderService.Filter = filter;
            filter.CustomerId = customer.Id;
            var orders = _orderService.GetOrders();
            TempData["SortingPagingInfo"] = _orderService.Pagination;

            return PartialView(orders);
        }

        public ActionResult _OrderDetails()
        {
            return PartialView();
        }

        public ActionResult Address()
        {
            ViewBag.Cities = GetCities();
            var customer = (Customer)Session[CommonConstants.CUSTOMER_SESSION];
            var shippingAddress = _shippingAddressService.GetShippingAddressByCustomerId(customer.Id);

            return View(shippingAddress);
        }

        [HttpPost]
        public ActionResult Address(ShippingAddress shippingAddress)
        {
            var customer = (Customer)Session[CommonConstants.CUSTOMER_SESSION];
            ModelState.Remove("CustomerId");
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                shippingAddress.CustomerId = customer.Id;
                UpdateDefaultShippingAddressProperties(shippingAddress);
                if (shippingAddress.Id == 0)
                {
                    _shippingAddressService.CreateShippingAddress(shippingAddress);
                }
                else
                {
                    _shippingAddressService.UpdateShippingAddress(shippingAddress);
                }
                Session[CommonConstants.CUSTOMER_SESSION] = _customerService.GetCustomer(customer.Id);
                return RedirectToAction("Address");
            }
            return View(shippingAddress);
        }

        // Get cities
        public List<City> GetCities()
        {
            _cityService.Pagination = new SortingPagingInfo
            {
                SortField = "Name",
                SortDirection = "ascending",
                PageSize = 0
            };
            return _cityService.GetCities();
        }

        public ActionResult CreateAddress()
        {
            return View();
        }

        public ActionResult CreateAddress(ShippingAddress address)
        {
            return View();
        }

        public ActionResult UpdateAddress()
        {
            return View();
        }

        public ActionResult UpdateAddress(ShippingAddress address)
        {
            return View();
        }

        public ActionResult PromotionInformation()
        {
            return View();
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = _customerService.GetCustomer(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(customer);
                _customerService.UpdateCustomer(customer);
                Session[CommonConstants.CUSTOMER_SESSION] = customer;
                return RedirectToAction("Account");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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

        // Update: CreatedOn, CreatedBy, ModifiedOn, ModifiedBy
        public void UpdateDefaultProperties(Customer customer)
        {
            if (customer.Id == 0)
            {
                customer.CreatedById = "1";
                customer.CreatedOn = DateTime.Now;
                customer.ModifiedById = "1";
                customer.ModifiedOn = DateTime.Now;
            }
            // Update
            else
            {
                customer.ModifiedById = "1";
                customer.ModifiedOn = DateTime.Now;
            }
        }

        // Update: CreatedOn, CreatedBy, ModifiedOn, ModifiedBy
        public void UpdateDefaultShippingAddressProperties(ShippingAddress shippingAddress)
        {
            var user = Session[CommonConstants.USER_SESSION] as User;
            // Create
            if (user != null)
            {
                if (shippingAddress.Id == 0)
                {
                    shippingAddress.CreatedById = user.Id;
                    shippingAddress.CreatedOn = DateTime.Now;
                    shippingAddress.ModifiedById = user.Id;
                    shippingAddress.ModifiedOn = DateTime.Now;
                }
                // Update
                else
                {
                    shippingAddress.ModifiedById = user.Id;
                    shippingAddress.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}

