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
    public class StocksController : Controller
    {
        private readonly StockService _stockService;

        public StocksController()
        {
            this._stockService = new StockService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        // GET: Admin/Stocks
        public ActionResult Index()
        {
            return View();
        }

        // GET: List of stocks
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

            _stockService.Pagination = info;
            _stockService.Filter = filter;
            var stocks = _stockService.GetStocks();
            TempData["SortingPagingInfo"] = _stockService.Pagination;

            return PartialView(stocks);
        }

        // GET: Admin/Stocks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var stock = _stockService.GetStock(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // GET: Admin/Stocks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Stock stock)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(stock);
                _stockService.CreateStock(stock);
                return RedirectToAction("Index");
            }

            return View(stock);
        }

        // GET: Admin/Stocks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var stock = _stockService.GetStock(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: Admin/Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Stock stock)
        {
            if (ModelState.IsValid)
            {
                UpdateDefaultProperties(stock);
                _stockService.UpdateStock(stock);
                return RedirectToAction("Index");
            }
            return View(stock);
        }

        // GET: Admin/Stocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var stock = _stockService.GetStock(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: Admin/Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _stockService.DeleteStock(id);
            return RedirectToAction("Index");
        }

        // Update: CreatedOn, CreatedBy, ModifiedOn, ModifiedBy
        public void UpdateDefaultProperties(Stock stock)
        {
            var user = Session[CommonConstants.USER_SESSION] as User;
            // Create
            if (user != null)
            {
                if (stock.Id == 0)
                {
                    stock.CreatedBy = user.Id;
                    stock.CreatedOn = DateTime.Now;
                    stock.ModifiedBy = user.Id;
                    stock.ModifiedOn = DateTime.Now;
                }
                // Update
                else
                {
                    stock.ModifiedBy = user.Id;
                    stock.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
