﻿using System.Web.Optimization;

namespace NetCommunitySolution.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            //VENDOR RESOURCES

            //~/Bundles/vendor/css
            bundles.Add(
                new StyleBundle("~/Bundles/vendor/css")
                    .Include("~/Content/themes/base/all.css", new CssRewriteUrlTransform())
                    .Include("~/Content/bootstrap-cosmo.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/toastr.min.css")
                    .Include("~/Scripts/sweetalert/sweet-alert.css")
                    .Include("~/Content/flags/famfamfam-flags.css", new CssRewriteUrlTransform())
                    .Include("~/Content/font-awesome.min.css", new CssRewriteUrlTransform())
                );

            //~/Bundles/vendor/js/top (These scripts should be included in the head of the page)
            bundles.Add(
                new ScriptBundle("~/Bundles/vendor/js/top")
                    .Include(
                        "~/Abp/Framework/scripts/utils/ie10fix.js",
                        "~/Scripts/modernizr-2.8.3.js"
                    )
                );

            //~/Bundles/vendor/bottom (Included in the bottom for fast page load)
            bundles.Add(
                new ScriptBundle("~/Bundles/vendor/js/bottom")
                    .Include(
                        "~/Scripts/json2.min.js",

                        "~/Scripts/jquery-3.2.1.min.js",
                        "~/Scripts/jquery-ui-1.12.1.min.js",

                        "~/Scripts/bootstrap.min.js",

                        "~/Scripts/moment-with-locales.min.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.blockUI.js",
                        "~/Scripts/toastr.min.js",
                        "~/Scripts/sweetalert/sweet-alert.min.js",
                        "~/Scripts/others/spinjs/spin.js",
                        "~/Scripts/others/spinjs/jquery.spin.js",

                        "~/Abp/Framework/scripts/abp.js",
                        "~/Abp/Framework/scripts/libs/abp.jquery.js",
                        "~/Abp/Framework/scripts/libs/abp.toastr.js",
                        "~/Abp/Framework/scripts/libs/abp.blockUI.js",
                        "~/Abp/Framework/scripts/libs/abp.sweet-alert.js",
                        "~/Abp/Framework/scripts/libs/abp.spin.js"
                    )
                );

            //APPLICATION RESOURCES

            #region home
            bundles.Add(
              new StyleBundle("~/Bundles/css")
                  .Include("~/Content/bootstrap-cosmo.min.css", new CssRewriteUrlTransform())
                  .Include("~/Content/toastr.min.css")
                  .Include("~/Scripts/sweetalert/sweet-alert.css")
                  .Include("~/Content/flags/famfamfam-flags.css", new CssRewriteUrlTransform())
                  .Include("~/Content/font-awesome.min.css", new CssRewriteUrlTransform())
                  .Include("~/Content/style.css", new CssRewriteUrlTransform())
              );
            #endregion

            #region layui

            //~/layui/js
            bundles.Add(
                new ScriptBundle("~/layui/js")
                .Include("~/layui/layui.all.js")
                .Include("~/Scripts/admin.common.js")
                );

            //~/layui/css
            bundles.Add(
                new StyleBundle("~/layui/css")
                    .Include("~/layui/css/layui.css")
                    .Include("~/Content/admin.css")
                );
            #endregion


            #region sytle
            bundles.Add(
              new StyleBundle("~/sytle/css")
                  .Include("~/Content/style.css", new CssRewriteUrlTransform())
              );


            #endregion

            #region Jquery
            
            #endregion

            #region abp && jquery
            bundles.Add(
                new ScriptBundle("~/jquery")
                    .Include(
                        "~/Scripts/json2.min.js",

                        "~/Scripts/jquery-3.2.1.min.js",
                        "~/Scripts/jquery-ui-1.12.1.min.js",

                        "~/Scripts/moment-with-locales.min.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js",
                        
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.blockUI.js",
                        "~/Scripts/toastr.min.js",
                        "~/Scripts/sweetalert/sweet-alert.min.js",
                        "~/Scripts/others/spinjs/spin.js",
                        "~/Scripts/others/spinjs/jquery.spin.js",

                        "~/Abp/Framework/scripts/abp.js",
                        "~/Abp/Framework/scripts/libs/abp.jquery.js",
                        "~/Abp/Framework/scripts/libs/abp.toastr.js",
                        "~/Abp/Framework/scripts/libs/abp.blockUI.js",
                        "~/Abp/Framework/scripts/libs/abp.sweet-alert.js",
                        "~/Abp/Framework/scripts/libs/abp.spin.js"
                    )
                );
            #endregion

            
        }
    }
}