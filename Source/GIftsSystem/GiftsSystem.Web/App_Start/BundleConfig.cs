﻿using System.Web;
using System.Web.Optimization;

namespace GiftsSystem.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScriptBundels(bundles);
            RegisterStyleBundels(bundles);

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }

        private static void RegisterStyleBundels(BundleCollection bundles)
        {
           
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.united.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/kendo/kendo.common-bootstrap.min.css",
                      "~/Content/kendo/kendo.silver.min.css"));
        }

        private static void RegisterScriptBundels(BundleCollection bundles)
        {
            //TODO:Add separate parts for different pages
            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                "~Scripts/kendo/kendo.all.min.js",
                "~Scripts/kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                   //"~/Scripts/jquery-{version}.js",
                   "~Scripts/kendo/jquery.min.js",
                   "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.validate.unobtrusive"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
        }
    }
}
