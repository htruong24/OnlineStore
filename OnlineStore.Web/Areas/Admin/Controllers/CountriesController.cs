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
    public class CountriesController : Controller
    {
        private readonly CountryService _countryService;

        public CountriesController()
        {
            this._countryService = new CountryService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        // GET: Admin/Countries
        public ActionResult Index()
        {
            return View();
        }

        // GET: List of countries
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

            _countryService.Pagination = info;
            _countryService.Filter = filter;
            var units = _countryService.GetCountries();
            TempData["SortingPagingInfo"] = _countryService.Pagination;

            return PartialView(units);
        }

        // GET: Admin/Countries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var country = _countryService.GetCountry(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // GET: Admin/Countries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Country country)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(country);
                _countryService.CreateCountry(country);
                return RedirectToAction("Index");
            }
            return View(country);
        }

        // GET: Admin/Countries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var country = _countryService.GetCountry(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: Admin/Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Country country)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(country);
                _countryService.UpdateCountry(country);
                return RedirectToAction("Index");
            }
            return View(country);
        }

        // GET: Admin/Countries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var country = _countryService.GetCountry(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: Admin/Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _countryService.DeleteCountry(id);
            return RedirectToAction("Index");
        }

        // Update: CreatedOn, CreatedBy, ModifiedOn, ModifiedBy
        public void UpdateDefaultProperties(Country country)
        {
            var user = Session[CommonConstants.USER_SESSION] as User;
            // Create
            if (user != null)
            {
                if (country.Id == 0)
                {
                    country.CreatedById = user.Id;
                    country.CreatedOn = DateTime.Now;
                    country.ModifiedById = user.Id;
                    country.ModifiedOn = DateTime.Now;
                }
                // Update
                else
                {
                    country.ModifiedById = user.Id;
                    country.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
