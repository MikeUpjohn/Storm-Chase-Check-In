using System.Web;
using System.Web.Optimization;

namespace StormChaseCheckIn
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/js/jquery-3.2.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/js/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/css/bootstrap.min.css",
                      "~/css/developer.css"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/js/developer.js"
                ));

            BundleTable.EnableOptimizations = false;
        }
    }
}
