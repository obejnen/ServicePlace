﻿using System.Web;
using System.Web.Optimization;

namespace ServicePlace.Website
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/order.css",
                      "~/Content/account.css",
                      "~/Content/provider.css",
                      "~/Content/category.css",
                      "~/Content/order-response.css",
                      "~/Content/notification.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/dropzonescripts").Include(
                "~/Scripts/dropzone/dropzone.js"));

            bundles.Add(new StyleBundle("~/Content/dropzonecss").Include(
                "~/Scripts/dropzone/basic.css",
                "~/Scripts/dropzone/dropzone.css",
                "~/Content/imagedropzone.css"));

            bundles.Add(new ScriptBundle("~/Scripts/javascripts").Include(
                "~/Scripts/site.js",
                "~/Scripts/notification.js"));

            bundles.Add(new ScriptBundle("~/Scripts/profilescripts")
                .Include("~/Scripts/profile.js"));

            bundles.Add(new ScriptBundle("~/Scripts/adminscripts")
                .Include("~/Scripts/admin.js"));
        }
    }
}
