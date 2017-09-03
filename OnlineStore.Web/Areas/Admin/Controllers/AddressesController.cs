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
    public class AddressesController : Controller
    {
        private readonly CityService _cityService;
        private readonly CustomerService _customerService;
        private readonly AddressService _addressService;

        public AddressesController()
        {
            this._cityService = new CityService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._customerService = new CustomerService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._addressService = new AddressService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
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

            _customerService.Pagination = info;
            _customerService.Filter = filter;
            var customers = _customerService.GetCustomers();
            TempData["SortingPagingInfo"] = _customerService.Pagination;

            return PartialView(customers);
        }

        // GET: Admin/Addresses/Details/5
        public ActionResult Details(int? id)
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
        public ActionResult Create(Address address)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(address);
                _addressService.CreateAddress(address);
                return RedirectToAction("Index");
            }
            return View(address);
        }

        // GET: Admin/Addresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var address = _addressService.GetAddress(id);
            ViewBag.Cities = GetCities();
            ViewBag.Customers = GetCustomers();
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Admin/Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Address address)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(address);
                _addressService.UpdateAddress(address);
                return RedirectToAction("Index");
            }
            return View(address);
        }

        // GET: Admin/Addresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var address = _addressService.GetAddress(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Admin/Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _addressService.DeleteAddress(id);
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
        public void UpdateDefaultProperties(Address address)
        {
            var user = Session[CommonConstants.USER_SESSION] as User;
            // Create
            if (user != null)
            {
                if (address.Id == 0)
                {
                    address.CreatedById = user.Id;
                    address.CreatedOn = DateTime.Now;
                    address.ModifiedById = user.Id;
                    address.ModifiedOn = DateTime.Now;
                }
                // Update
                else
                {
                    address.ModifiedById = user.Id;
                    address.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
