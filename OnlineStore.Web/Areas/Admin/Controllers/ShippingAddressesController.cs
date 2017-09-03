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
using OnlineStore.Common;

namespace OnlineStore.Web.Areas.Admin.Controllers
{
    public class ShippingAddressesController : Controller
    {
        private readonly CityService _cityService;
        private readonly CustomerService _customerService;
        private readonly ShippingAddressService _shippingAddressService;

        public ShippingAddressesController()
        {
            this._cityService = new CityService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._customerService = new CustomerService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._shippingAddressService = new ShippingAddressService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }


        // GET: Admin/Addresses
        public ActionResult Index()
        {
            return View();
        }

        // GET: List of customer
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

            _shippingAddressService.Pagination = info;
            _shippingAddressService.Filter = filter;
            var addresses = _shippingAddressService.GetShippingAddresses();
            TempData["SortingPagingInfo"] = _shippingAddressService.Pagination;

            return PartialView(addresses);
        }

        // GET: Admin/Addresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var shippingAddress = _shippingAddressService.GetShippingAddress(id);
            if (shippingAddress == null)
            {
                return HttpNotFound();
            }
            return View(shippingAddress);
        }

        // GET: Admin/Addresses/Create
        public ActionResult Create()
        {
            ViewBag.Cities = GetCities();
            ViewBag.Customers = GetCustomers();
            return View();
        }

        // POST: Admin/Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShippingAddress shippingAddress)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(shippingAddress);
                shippingAddress.Customer = _customerService.GetCustomer(shippingAddress.CustomerId);
                _shippingAddressService.CreateShippingAddress(shippingAddress);
                return RedirectToAction("Index");
            }
            return View(shippingAddress);
        }

        // GET: Admin/Addresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var shippingAddress = _shippingAddressService.GetShippingAddress(id);
            ViewBag.Cities = GetCities();
            ViewBag.Customers = GetCustomers();
            if (shippingAddress == null)
            {
                return HttpNotFound();
            }
            return View(shippingAddress);
        }

        // POST: Admin/Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ShippingAddress shippingAddress)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(shippingAddress);
                _shippingAddressService.UpdateShippingAddress(shippingAddress);
                return RedirectToAction("Index");
            }
            return View(shippingAddress);
        }

        // GET: Admin/Addresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var shippingAddress = _shippingAddressService.GetShippingAddress(id);
            if (shippingAddress == null)
            {
                return HttpNotFound();
            }
            return View(shippingAddress);
        }

        // POST: Admin/Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _shippingAddressService.DeleteShippingAddress(id);
            return RedirectToAction("Index");
        }

        // Get countries
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

        // Get countries
        public List<Customer> GetCustomers()
        {
            _customerService.Pagination = new SortingPagingInfo
            {
                SortField = "Name",
                SortDirection = "ascending",
                PageSize = 0
            };
            return _customerService.GetCustomers();
        }

        // Update: CreatedOn, CreatedBy, ModifiedOn, ModifiedBy
        public void UpdateDefaultProperties(ShippingAddress shippingAddress)
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
