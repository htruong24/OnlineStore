using OnlineStore.Common;
using OnlineStore.Data.Entities;
using OnlineStore.Data.Infrastructure;
using OnlineStore.Data.Interfaces;
using OnlineStore.Services.BLL.Services;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OnlineStore.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly BrandService _brandService;
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly SubCategoryService _subCategoryService;

        public HomeController()
        {
            this._brandService = new BrandService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._productService = new ProductService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._categoryService = new CategoryService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
            this._subCategoryService = new SubCategoryService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
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

        // Recently Preview Products
        public ActionResult _RecentlyPreviewProducts()
        {
            List<Product> products = Session[CommonConstants.RECENTLY_PREVIEW_PRODUCT] == null ? new List<Product>() : (List<Product>)Session[CommonConstants.RECENTLY_PREVIEW_PRODUCT];

            return PartialView(products);
        }

        // Special Offer
        public ActionResult _RelatedProducts(int? subCategoryId)
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

        // Breadcrumb
        public ActionResult _Breadcrumb(string type, int? id, string title)
        {
            var breadcrumbHtml = "<li><a href='/'>Trang chủ</a></li>";

            if(type == "Product")
            {
                var product = _productService.GetProduct(id);
                breadcrumbHtml += "<li><a href='/danh-muc/" + product.SubCategory.Category.MetaTitle + "-" + product.SubCategory.CategoryId + "'>" + product.SubCategory.Category.Name + "</a></li>";
                breadcrumbHtml += "<li><a href='/danh-muc-con/" + product.SubCategory.MetaTitle + "-" + product.SubCategory.Id + "'>" + product.SubCategory.Name + "</a></li>";
                breadcrumbHtml += "<li class='active'>" + product.Name + "</li>";
            }
            else if(type == "Category")
            {
                var category = _categoryService.GetCategory(id);
                breadcrumbHtml += "<li class='active'>" + category.Name + "</li>";
            }
            else if(type == "SubCategory")
            {
                var subCategory = _subCategoryService.GetSubCategory(id);
                breadcrumbHtml += "<li><a href='/danh-muc/" + subCategory.Category.MetaTitle + "-" + subCategory.Category.Id + "'>" + subCategory.Category.Name + "</a></li>";
                breadcrumbHtml += "<li class='active'>" + subCategory.Name + "</li>";
            }
            else
            {
                breadcrumbHtml += "<li class='active'>" + title + "</li>";
            }

            ViewBag.BreadcrumbHtml = breadcrumbHtml;
               
            return PartialView();
        }

        // ---- MAIN SEARCH --
        public ActionResult _SearchResultList(SortingPagingInfo info, DefaultFilter filter)
        {
            if (info.SortField == null)
            {
                info = new SortingPagingInfo
                {
                    SortField = "Title",
                    SortDirection = "ascending",
                    PageSize = CommonConstants.PAGE_SIZE,
                    CurrentPage = 1
                };
            }

            _productService.Pagination = info;
            _productService.Filter = filter;
            var products = _productService.GetProducts();
            TempData["SortingPagingInfo"] = _productService.Pagination;

            return PartialView(products);
        }

        public ActionResult _SearchResult(DefaultFilter filter)
        {
            return PartialView(filter);
        }

        public ActionResult _Paging(string totalRows, string currentPage, string pageSize, string controlName)
        {
            var currentPageFilter = int.Parse(currentPage);
            var pageSizeFilter = int.Parse(pageSize);

            ViewBag.ControlName = controlName;

            if (pageSizeFilter == 0)
            {
                ViewBag.TotalPages = 1;
            }
            else
            {
                if (int.Parse(totalRows) % pageSizeFilter == 0)
                {
                    ViewBag.TotalPages = int.Parse(totalRows) / pageSizeFilter;
                }
                else
                {
                    ViewBag.TotalPages = (int.Parse(totalRows) / pageSizeFilter) + 1;
                }
                if (ViewBag.TotalPages < (pageSizeFilter + 1))
                {
                    ViewBag.StartNumber = 1;
                    ViewBag.EndNumber = ViewBag.TotalPages;
                }
                else
                {
                    int subtract = 0;
                    int plus = 0;
                    if (currentPageFilter == 1)
                    {
                        subtract = 0;
                        plus = 4;
                    }
                    else if (currentPageFilter == 2)
                    {
                        subtract = 1;
                        plus = 3;
                    }
                    else if (currentPageFilter <= ViewBag.TotalPages - 2)
                    {
                        subtract = 2;
                        plus = 2;
                    }
                    else if (currentPageFilter == ViewBag.TotalPages - 1)
                    {
                        subtract = 3;
                        plus = 1;
                    }
                    else if (currentPageFilter == ViewBag.TotalPages)
                    {
                        subtract = 4;
                        plus = 0;
                    }
                    ViewBag.StartNumber = currentPageFilter - subtract;
                    ViewBag.EndNumber = currentPageFilter + plus;
                }
            }

            ViewBag.CurrentPage = currentPageFilter;
            ViewBag.TotalRows = totalRows;
            ViewBag.PageSize = pageSize;
            ViewBag.FromRecord = currentPageFilter * pageSizeFilter - pageSizeFilter + 1;
            ViewBag.ToRecord = currentPageFilter * pageSizeFilter;

            return PartialView();
        }

    }
}