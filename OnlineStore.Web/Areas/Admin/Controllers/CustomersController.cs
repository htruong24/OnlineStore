using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    public class CustomersController : BaseController
    {
        private readonly CustomerService _customerService;

        public CustomersController()
        {
            this._customerService = new CustomerService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        // GET: Admin/Customers
        public ActionResult Index()
        {
            return View();
        }

        // GET: List of modules
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
            var modules = _customerService.GetCustomers();
            TempData["SortingPagingInfo"] = _customerService.Pagination;

            return PartialView(modules);
        }

        // GET: Admin/Customers/Details/5
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

        // GET: Admin/Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(customer);
                _customerService.CreateCustomer(customer);
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Admin/Customers/Edit/5
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

        // POST: Admin/Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(customer);
                _customerService.UpdateCustomer(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Admin/Customers/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Admin/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _customerService.DeleteCustomer(id);
            return RedirectToAction("Index");
        }

        // Update: CreatedOn, CreatedBy, ModifiedOn, ModifiedBy
        public void UpdateDefaultProperties(Customer customer)
        {
            var user = Session[CommonConstants.USER_SESSION] as User;
            // Create
            if (user != null)
            {
                if (customer.Id == 0)
                {
                    customer.CreatedById = user.Id;
                    customer.CreatedOn = DateTime.Now;
                    customer.ModifiedById = user.Id;
                    customer.ModifiedOn = DateTime.Now;
                }
                // Update
                else
                {
                    customer.ModifiedById = user.Id;
                    customer.ModifiedOn = DateTime.Now;
                }
            }
        }

        // Get customer
        [HttpGet]
        public JsonResult GetCustomer(int customerId)
        {
            var customer = _customerService.GetCustomer(customerId);
            var address = customer.ShippingAddresses.ToList();
            return Json(new
            {
                Name = customer.Name,
                Telephone = customer.Telephone,
                Email = customer.Email,
                Address = address.Count() > 0 ? address[0].Value : "",
                PostalCode = address.Count() > 0 ? address[0].PostalCode : "",
                CityId = address.Count() > 0 ? address[0].CityId : null,
                Note = address.Count() > 0 ? address[0].Note : "",
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
