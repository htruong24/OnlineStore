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
    public class CitiesController : Controller
    {
        private readonly CityService _cityService;
        private readonly CountryService _countryService;

        public CitiesController()
        {
            this._cityService = new CityService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._countryService = new CountryService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        // GET: Admin/Cities
        public ActionResult Index()
        {
            return View();
        }

        // GET: List of cities
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

            _cityService.Pagination = info;
            _cityService.Filter = filter;
            var cities = _cityService.GetCities();
            TempData["SortingPagingInfo"] = _cityService.Pagination;

            return PartialView(cities);
        }

        // GET: Admin/Cities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var city = _cityService.GetCity(id);
            ;
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // GET: Admin/Cities/Create
        public ActionResult Create()
        {
            ViewBag.Countries = GetCountries();
            return View();
        }

        // POST: Admin/Cities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(City city)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(city);
                _cityService.CreateCity(city);
                return RedirectToAction("Index");
            }
            return View(city);
        }

        // GET: Admin/Cities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var city = _cityService.GetCity(id);
            ViewBag.Countries = GetCountries();
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: Admin/Cities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(City city)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(city);
                _cityService.UpdateCity(city);
                return RedirectToAction("Index");
            }
            return View(city);
        }

        // GET: Admin/Cities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var city = _cityService.GetCity(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: Admin/Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _cityService.DeleteCity(id);
            return RedirectToAction("Index");
        }

        // Get countries
        public List<Country> GetCountries()
        {
            _countryService.Pagination = new SortingPagingInfo
            {
                SortField = "Name",
                SortDirection = "ascending",
                PageSize = 0
            };
            return _countryService.GetCountries();
        }

        // Update: CreatedOn, CreatedBy, ModifiedOn, ModifiedBy
        public void UpdateDefaultProperties(City city)
        {
            var user = Session[CommonConstants.USER_SESSION] as User;
            // Create
            if (user != null)
            {
                if (city.Id == 0)
                {
                    city.CreatedById = user.Id;
                    city.CreatedOn = DateTime.Now;
                    city.ModifiedById = user.Id;
                    city.ModifiedOn = DateTime.Now;
                }
                // Update
                else
                {
                    city.ModifiedById = user.Id;
                    city.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
