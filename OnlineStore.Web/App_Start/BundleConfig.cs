using System.Web;
using System.Web.Optimization;

namespace OnlineStore.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/script").Include(
                      "~/Scripts/jquery-2.2.3.min.js",
                      "~/Scripts/jquery.validate.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/toastr/toastr.min.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/jquery-ui-1.11.0/jquery-ui.min.js",
                      "~/Scripts/jquery-ui-timepicker-addon/jquery-ui-timepicker-addon.js",
                      "~/Scripts/chosen_v1.4.2/chosen.jquery.js",
                      "~/Scripts/hash/jquery.ba-hashchange.min.js",
                      "~/Scripts/jquery.blockUI-2.6.6/jquery.blockUI-2.6.6.js",
                      "~/Scripts/common.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootboxjs/bootbox.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/css").Include(
                      "~/Scripts/jquery-ui-1.11.0/themes/custom-theme/jquery-ui-1.10.3.custom.css",
                      "~/Content/bootstrap.css",
                      "~/Scripts/toastr/toastr.min.css",
                      "~/Scripts/font-awesome-4.6.3/css/font-awesome.css",
                      "~/Content/site.css",
                      "~/Scripts/jquery-ui-timepicker-addon/jquery-ui-timepicker-addon.css",
                      "~/Scripts/chosen_v1.4.2/chosen.css"));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                    "~/Scripts/main.js"));

            BundleTable.EnableOptimizations = false;
        }
    }
}

