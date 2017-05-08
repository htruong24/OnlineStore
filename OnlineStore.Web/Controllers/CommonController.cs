using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineStore.Common;

namespace OnlineStore.Web.Controllers
{
    public class CommonController : Controller
    {
        public ActionResult ButtonCreate()
        {
            return PartialView();
        }

        public ActionResult ButtonSave(string elementId)
        {
            ViewBag.ElementId = string.IsNullOrEmpty(elementId) ? "btnSumit" : ("btnSumit-" + elementId);
            return PartialView();
        }

        public ActionResult ButtonCommon(string title, string script)
        {
            ViewBag.Title = title;
            ViewBag.Script = script;

            return PartialView();
        }

        public ActionResult ButtonCancel()
        {
            return PartialView();
        }

        public ActionResult ButtonEdit()
        {
            return PartialView();
        }

        public ActionResult ButtonClear()
        {
            return PartialView();
        }

        public ActionResult ButtonDelete()
        {
            return PartialView();
        }

        public ActionResult UploadImage(string avatar)
        {
            ViewBag.Avatar = avatar;
            return PartialView();
        }

        public ActionResult Paging(string totalRows, string currentPage, string pageSize, string controlName)
        {
            var currentPageFilter = int.Parse(currentPage);
            var pageSizeFilter = int.Parse(pageSize);

            ViewBag.ControlName = controlName;

            if (pageSizeFilter == 0)
            {
                ViewBag.TotalPage = 1;
            }
            else
            {
                if (int.Parse(totalRows) % pageSizeFilter == 0)
                {
                    ViewBag.TotalPage = int.Parse(totalRows) / pageSizeFilter;
                }
                else
                {
                    ViewBag.TotalPage = (int.Parse(totalRows) / pageSizeFilter) + 1;
                }
                if (ViewBag.TotalPage < (pageSizeFilter + 1))
                {
                    ViewBag.StartNumber = 1;
                    ViewBag.EndNumber = ViewBag.TotalPage;
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
                    else if (currentPageFilter <= ViewBag.TotalPage - 2)
                    {
                        subtract = 2;
                        plus = 2;
                    }
                    else if (currentPageFilter == ViewBag.TotalPage - 1)
                    {
                        subtract = 3;
                        plus = 1;
                    }
                    else if (currentPageFilter == ViewBag.TotalPage)
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

            return PartialView();
        }
    }
}
