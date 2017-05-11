using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Web.Areas.Admin.Controllers
{
    public class CommonController : Controller
    {
        // GET: Admin/Common
        public ActionResult Index()
        {
            return View();
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