using System;
using System.Web.Optimization;

namespace Sorak.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            bundles.Add(
               new ScriptBundle("~/scripts/vendor")
                 .Include("~/scripts/jquery-{version}.js")
                 .Include("~/scripts/es5-sham.js")
                 .Include("~/scripts/es5-shim.js")
                 .Include("~/scripts/knockout-{version}.debug.js")
                 .Include("~/scripts/toastr.js")
                 .Include("~/scripts/Q.js")
                 .Include("~/scripts/breeze.min.js")
                 .Include("~/scripts/bootstrap.js")
                 .Include("~/scripts/moment.js")
                 .Include("~/scripts/jquery-ui-1.10.3.min.js")
                 .Include("~/scripts/jquery.maskedinput-1.2.2.js")
                 .Include("~/scripts/knockout.validation.js")
               );

            bundles.Add(
              new StyleBundle("~/Content/css")
                .Include("~/Content/main.css")
                .Include("~/Content/ie10mobile.css")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/bootstrap-responsive.css")
                .Include("~/Content/durandal.css")
                .Include("~/Content/toastr.css")
                
              );
        }

        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            if (ignoreList == null)
            {
                throw new ArgumentNullException("ignoreList");
            }

            ignoreList.Ignore("*.intellisense.js");
            ignoreList.Ignore("*-vsdoc.js");

            //ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            //ignoreList.Ignore("*.min.js", OptimizationMode.WhenDisabled);
            //ignoreList.Ignore("*.min.css", OptimizationMode.WhenDisabled);
        }
    }
}