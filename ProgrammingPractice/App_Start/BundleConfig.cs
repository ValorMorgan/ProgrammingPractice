using System.Web;
using System.Web.Optimization;

namespace ProgrammingPractice
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/Library/JQuery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/Library/JQuery/Validate/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/Library/Modernizer/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/Library/BootStrap/bootstrap.js",
                        "~/Scripts/Library/Respond/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                        "~/Scripts/App/Site.js"));

            bundles.Add(new ScriptBundle("~/bundles/multiThread").Include(
                        "~/Scripts/Library/JQuery/Timer/timer.jquery.min.js",
                        "~/Scripts/Library/LiquidFillGauge/liquidFillGauge.js",
                        "~/Scripts/App/MultiThread.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/StyleSheets/bootstrap.css",
                        "~/Content/StyleSheets/site.css"));
        }
    }
}
