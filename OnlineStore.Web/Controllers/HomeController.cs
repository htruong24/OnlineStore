using OnlineStore.Common;
using OnlineStore.Data.Infrastructure;
using OnlineStore.Data.Interfaces;
using OnlineStore.Services.BLL.Services;
using System.Web.Mvc;

namespace OnlineStore.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly BrandService _brandService;

        public HomeController()
        {
            this._brandService = new BrandService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

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
            _brandService.Pagination = new SortingPagingInfo
            {
                SortField = "OrderNumber",
                SortDirection = "ascending",
                PageSize = 0
            };

            var brands = _brandService.GetBrands();

            return PartialView(brands);
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

        // Brand
        public ActionResult _Brand()
        {
            _brandService.Pagination = new SortingPagingInfo
            {
                SortField = "OrderNumber",
                SortDirection = "ascending",
                PageSize = 0
            };

            var brands = _brandService.GetBrands();

            return PartialView(brands);
        }

        // Recommended Product
        public ActionResult _RecommendedProduct()
        {
            return PartialView();
        }

        // Top Fashion
        public ActionResult _TopFashion()
        {
            return PartialView();
        }

        // Top Food
        public ActionResult _TopFood()
        {
            return PartialView();
        }

        // Top Books
        public ActionResult _TopBooks()
        {
            return PartialView();
        }
    }
}