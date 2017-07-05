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
        private readonly ProductService _productService;

        public HomeController()
        {
            this._brandService = new BrandService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._productService = new ProductService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Policy()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        // -- PARTIAL VIEW --

        // Featured Product
        public ActionResult _FeaturedProduct()
        {
            _productService.Pagination = new SortingPagingInfo
            {
                SortField = "CreatedOn",
                SortDirection = "descending",
                PageSize = 8
            };

            _productService.Filter = new DefaultFilter()
            {
                Featured = true
            };

            var products = _productService.GetProducts();

            return PartialView(products);
        }

        // New Arrival Product
        public ActionResult _NewArrivalProduct()
        {
            _productService.Pagination = new SortingPagingInfo
            {
                SortField = "CreatedOn",
                SortDirection = "descending",
                PageSize = 8
            };

            var products = _productService.GetProducts();

            return PartialView(products);
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
        public ActionResult _SpecialOffer(int? subCategoryId)
        {
            _productService.Pagination = new SortingPagingInfo
            {
                SortField = "CreatedOn",
                SortDirection = "descending",
                PageSize = 8
            };

            _productService.Filter = new DefaultFilter()
            {
                SubCategoryId = subCategoryId == null ? 0 : subCategoryId.Value
            };

            var products = _productService.GetProducts();

            return PartialView(products);
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
            _productService.Pagination = new SortingPagingInfo
            {
                SortField = "NumberOfClicks",
                SortDirection = "descending",
                PageSize = 8
            };

            var products = _productService.GetProducts();

            return PartialView(products);
        }

        // Top Fashion
        public ActionResult _TopFashion(int categoryId)
        {
            _productService.Pagination = new SortingPagingInfo
            {
                SortField = "CreatedOn",
                SortDirection = "descending",
                PageSize = 8
            };

            _productService.Filter = new DefaultFilter()
            {
                CategoryId = categoryId
            };

            var products = _productService.GetProducts();

            return PartialView(products);
        }

        // Top Food
        public ActionResult _TopFood(int categoryId)
        {
            _productService.Pagination = new SortingPagingInfo
            {
                SortField = "CreatedOn",
                SortDirection = "descending",
                PageSize = 8
            };

            _productService.Filter = new DefaultFilter()
            {
                CategoryId = categoryId
            };

            var products = _productService.GetProducts();

            return PartialView(products);
        }

        // Top Books
        public ActionResult _TopBooks(int categoryId)
        {
            _productService.Pagination = new SortingPagingInfo
            {
                SortField = "CreatedOn",
                SortDirection = "descending",
                PageSize = 8
            };

            _productService.Filter = new DefaultFilter()
            {
                CategoryId = categoryId
            };

            var products = _productService.GetProducts();

            return PartialView(products);
        }
    }
}