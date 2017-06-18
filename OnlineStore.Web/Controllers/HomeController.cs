using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // -- PARTIAL VIEW --

        // Featured Product
        public ActionResult _FeaturedProduct()
        {
            return PartialView();
        }

        // New Arrival Product
        public ActionResult _NewArrivalProduct()
        {
            return PartialView();
        }

        // Top Categories
        public ActionResult _TopCategories()
        {
            return PartialView();
        }

        // Popular Brands
        public ActionResult _PopularBrands()
        {
            return PartialView();
        }

        // Random - Best Seller - Hot Sale
        public ActionResult _Random_BestSeller_HotSale()
        {
            return PartialView();
        }

        // Clients Say
        public ActionResult _ClientsSay()
        {
            return PartialView();
        }

        // Hot Deals
        public ActionResult _HotDeals()
        {
            return PartialView();
        }

        // Special Offer
        public ActionResult _SpecialOffer()
        {
            return PartialView();
        }

        // Main Slider
        public ActionResult _MainSlider()
        {
            return PartialView();
        }

        // Latest News
        public ActionResult _LatestNews()
        {
            return PartialView();
        }
    }
}