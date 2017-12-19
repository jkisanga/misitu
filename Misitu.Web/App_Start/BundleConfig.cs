using System.Web.Optimization;

namespace Misitu.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            //VENDOR RESOURCES

            //~/Bundles/template/css
            bundles.Add(
            new StyleBundle("~/Bundles/template/login/css")
                    .Include("~/Content/themes/macadmin/style/bootstrap.css", new CssRewriteUrlTransform())
                    .Include("~/Content/themes/macadmin/style/font-awesome.css", new CssRewriteUrlTransform())                   
                    .Include("~/Content/themes/macadmin/style/style.css", new CssRewriteUrlTransform())
                      .Include("~/Scripts/sweetalert/sweet-alert.css", new CssRewriteUrlTransform())
                    .Include("~/Content/themes/macadmin/style/bootstrap-responsive.css", new CssRewriteUrlTransform())
            );

            bundles.Add(
                new StyleBundle("~/Bundles/template/css")                   
                        .Include("~/Content/themes/macadmin/style/bootstrap.css", new CssRewriteUrlTransform())
                        .Include("~/Content/themes/macadmin/style/font-awesome.css", new CssRewriteUrlTransform())
                        .Include("~/Content/themes/macadmin/style/jquery-ui.css", new CssRewriteUrlTransform())
                        .Include("~/Content/themes/macadmin/style/rateit.css", new CssRewriteUrlTransform())
                        .Include("~/Content/themes/macadmin/style/bootstrap-datetimepicker.min.css", new CssRewriteUrlTransform())
                        .Include("~/Content/themes/macadmin/style/uniform.default.css", new CssRewriteUrlTransform())
                        .Include("~/Content/themes/macadmin/style/bootstrap-switch.css", new CssRewriteUrlTransform())
                        .Include("~/Content/themes/macadmin/style/style.css", new CssRewriteUrlTransform())
                        .Include("~/Content/themes/macadmin/style/widgets.css", new CssRewriteUrlTransform())
                          .Include("~/Scripts/sweetalert/sweet-alert.css", new CssRewriteUrlTransform())
                        .Include("~/Content/themes/macadmin/style/jquery.dataTables.css", new CssRewriteUrlTransform())
                        .Include("~/Content/themes/macadmin/style/dataTables.bootstrap.css", new CssRewriteUrlTransform())
                );

           //MAIN JS
            bundles.Add(
                new ScriptBundle("~/Bundles/template/js")
                    .Include(
                    //JS
                        "~/Content/themes/macadmin/js/jquery.js",
                        "~/Content/themes/macadmin/js/jquery.serialize-object.js",
                         "~/Content/themes/macadmin/js/bootstrap.js",
                         "~/Content/themes/macadmin/js/jquery-ui-1.9.2.custom.min.js",
                         "~/Content/themes/macadmin/js/jquery.rateit.min.js",
                         "~/Content/themes/macadmin/js/jquery.dataTables.js",
                         "~/Content/themes/macadmin/js/dataTables.bootstrap.js",
                       //Charts
                       "~/Content/themes/macadmin/js/excanvas.min.js",
                       "~/Content/themes/macadmin/js/jquery.flot.js",
                       "~/Content/themes/macadmin/js/jquery.flot.resize.js",
                       "~/Content/themes/macadmin/js/jquery.flot.pie.js",
                       "~/Content/themes/macadmin/js/jquery.flot.stack.js",
                       //Jquery notification
                       "~/Content/themes/macadmin/js/jquery.noty.js",
                        "~/Content/themes/macadmin/js/themes/default.js",
                        "~/Content/themes/macadmin/js/layouts/bottom.js",
                        "~/Content/themes/macadmin/js/layouts/topRight.js",
                        "~/Content/themes/macadmin/js/layouts/top.js",
                        //Others
                        "~/Content/themes/macadmin/js/sparklines.js",
                        "~/Content/themes/macadmin/js/bootstrap-datetimepicker.min.js",
                        "~/Content/themes/macadmin/js/jquery.uniform.min.js",
                        "~/Content/themes/macadmin/js/bootstrap-switch.min.js",
                         "~/Content/themes/macadmin/js/filter.js"
                      

                    )
                   
                );

     
            bundles.Add(
              new ScriptBundle("~/Bundles/template/login/js")
                  .Include(
                       "~/Content/themes/macadmin/js/jquery.js",
                      "~/Content/themes/macadmin/js/bootstrap.js"
                  )
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

                        //"~/Scripts/jquery-2.2.0.min.js",
                        //"~/Scripts/jquery-ui-1.11.4.min.js",

                        //"~/Scripts/bootstrap.min.js",

                        "~/Scripts/moment-with-locales.min.js",
                        //"~/Scripts/jquery.validate.min.js",
                        //"~/Scripts/jquery.blockUI.js",
                        "~/Scripts/toastr.min.js",
                        "~/Scripts/sweetalert/sweet-alert.min.js",
                        //"~/Scripts/others/spinjs/spin.js",
                        //"~/Scripts/others/spinjs/jquery.spin.js",

                        "~/Abp/Framework/scripts/abp.js",
                        "~/Abp/Framework/scripts/libs/abp.jquery.js",
                        "~/Abp/Framework/scripts/libs/abp.toastr.js",
                        "~/Abp/Framework/scripts/libs/abp.blockUI.js",
                        "~/Abp/Framework/scripts/libs/abp.spin.js",
                        "~/Abp/Framework/scripts/libs/abp.sweet-alert.js",

                        "~/Scripts/jquery.signalR-2.2.1.min.js"
                    )
                );


            //APPLICATION RESOURCES

            //~/Bundles/css
            bundles.Add(
                new StyleBundle("~/Bundles/css")
                    .Include("~/css/main.css")
                );

            //~/Bundles/js
            bundles.Add(
                new ScriptBundle("~/Bundles/js")
                    .Include("~/js/main.js",
                    "~/Content/themes/macadmin/js/custom.js",
                     "~/Content/themes/macadmin/js/charts.js")
                );

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
        }
    }
}