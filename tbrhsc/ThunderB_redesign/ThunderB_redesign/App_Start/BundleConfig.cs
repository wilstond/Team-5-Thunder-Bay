﻿using System.Web.Optimization;

namespace ThunderB_redesign
{
    public class BundleConfig
    {
        
        public static void RegisterBundles(BundleCollection bundles)
        {

            //turning off minification and optimization until we find a better solution
            BundleTable.EnableOptimizations = false;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/Scripts").Include("~/Scripts/main.js", "~/Scripts/plugins.js"));

            bundles.Add(new StyleBundle("~/Content/css")
                        .Include("~/Content/css/normalize.css")
                        .Include("~/Content/css/site.css")
                        .Include("~/Content/css/font-awesome.css")
                        .Include("~/Content/css/Site_Donation.css")
                        .Include("~/Content/css/Site_GiftShop.css")
                        .Include("~/Content/css/Site_Newsletter.css")
                        .Include("~/Content/css/job.less"));

            
            bundles.Add(new StyleBundle("~/Fonts").Include(
                "~/Fonts/SourceSansPro-Black.ttf",
                "~/Fonts/SourceSansPro-BlackItalic.ttf",
                "~/Fonts/SourceSansPro-Bold.ttf",
                "~/Fonts/SourceSansPro-BoldItalic.ttf",
                "~/Fonts/SourceSansPro-ExtraLight.ttf",
                "~/Fonts/SourceSansPro-ExtraLightItalic.ttf",
                "~/Fonts/SourceSansPro-Italic.ttf",
                "~/Fonts/SourceSansPro-Light.ttf",
                "~/Fonts/SourceSansPro-LightItalic.ttf",
                "~/Fonts/SourceSansPro-Regular.ttf",
                "~/Fonts/SourceSansPro-Semibold.ttf",
                "~/Fonts/SourceSansPro-SemiboldItalic.ttf"));

     /*       bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));*/


            bundles.Add(new StyleBundle("~/Areas/admin/Content/AdminCss").Include("~/Areas/admin/Content/AdminCss/style.css")
                .Include("~/Content/css/font-awesome.css")
                .Include("~/Content/css/Site_Donation.css")
                .Include("~/Content/css/Site_GiftShop.css")
                .Include("~/Content/css/Site_Newsletter.css")
                .Include("~/Areas/admin/Content/AdminCss/jobAdmin.less"));

            bundles.Add(new ScriptBundle("~/Areas/admin/Content/AdminJS").Include("~/Areas/admin/Content/AdminJS/admin.js"));

       

        }
    }
}