using System.Web;
using System.Web.Optimization;

namespace ZillowSearch
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.1.0.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/home").Include(
                        "~/Scripts/home.js").Include(
                        "~/Scripts/handlebars.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css").Include(
                      "~/Content/forms.css"));
            bundles.Add(new StyleBundle("~/Content/home").Include(
                      "~/Content/home.css"));
        }
    }
}
