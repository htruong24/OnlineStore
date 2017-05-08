using System.Web.Mvc;

namespace OnlineStore.Web.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string ContentRoot(this UrlHelper helper, string fileName)
        {
            return helper.Content("~/Content/" + fileName);
        }
        public static string Image(this UrlHelper helper, string fileName)
        {
            return helper.Content("~/Content/Images/" + fileName);
        }
        public static string Stylesheet(this UrlHelper helper, string fileName)
        {
            return helper.Content("~/Content/" + fileName);
        }
        public static string Content(this UrlHelper helper, string path)
        {
            return helper.Content("~/Content/" + path);
        }
        public static string Script(this UrlHelper helper, string fileName)
        {
            return helper.Content("~/Content/Scripts/" + fileName);
        }
        public static string Content(this UrlHelper helper, string module, string fileName)
        {
            return helper.Content(string.Format("~/Areas/{0}/Content/{1}", module, fileName));
        }
        public static string Image(this UrlHelper helper, string module, string fileName)
        {
            return helper.Content(string.Format("~/Areas/{0}/Content/Images/{1}", module, fileName));
        }
        public static string Stylesheet(this UrlHelper helper, string module, string fileName)
        {
            return helper.Content(string.Format("~/Areas/{0}/Content/{1}", module, fileName));
        }
        public static string Script(this UrlHelper helper, string module, string fileName)
        {
            return helper.Content(string.Format("~/Areas/{0}/Content/Scripts/{1}", module, fileName));
        }
    }
}
