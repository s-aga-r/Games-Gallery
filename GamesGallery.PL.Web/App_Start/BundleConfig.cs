using System.Web;
using System.Web.Optimization;

namespace GamesGallery.PL.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/jqueryunobtrusiveval").Include(
                        "~/Scripts/jquery.validate.unobtrusive.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/custom/js").Include(
                        "~/Scripts/custom.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/User").Include(
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/Admin").Include(
                      "~/Content/Site-Admin.css"));

            bundles.Add(new StyleBundle("~/Content/pagedlist").Include(
                      "~/Content/PagedList.css"));

            bundles.Add(new StyleBundle("~/bundles/custom/css").Include(
                        "~/Content/custom.css"));
        }
    }
}
