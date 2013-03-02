using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Linq;
using Microsoft.Ajax.Utilities;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website
{
    public static class BundleConfig
    {
        private const string bundlesPath = "~/content/";
        public const string StyleBundlePath = bundlesPath + "styles.css";
        public const string ScriptBundlePath = bundlesPath + "scripts.js";

        public static void RegisterBundes(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            bundleStyles(bundles);
            bundleScripts(bundles);
            
            BundleTable.EnableOptimizations = true;
        }

        private static void bundleStyles(BundleCollection bundles)
        {
            var stylesDirectory = "~/content/Styles/";

            var bundle = new StyleBundle(StyleBundlePath)
                .Include(stylesDirectory + "structure/site.css")
                .Include(stylesDirectory + "structure/navigation.css")
                .Include(stylesDirectory + "structure/content.css")
                .Include(stylesDirectory + "structure/widget.css")
                .Include(stylesDirectory + "structure/botMessage.css")

                .Include(stylesDirectory + "color/site.css")
                .Include(stylesDirectory + "color/navigation.css")
                .Include(stylesDirectory + "color/widget.css")
                .Include(stylesDirectory + "color/content.css")
                ;

            bundles.Add(bundle);
        }

        private static void bundleScripts(BundleCollection bundles)
        {
            var scriptDirectory = "~/content/Scripts/";

            var bundle = new ScriptBundle(ScriptBundlePath)
                .Include(scriptDirectory + "Libraries/jquery-1.5.1.min.js")
                .Include(scriptDirectory + "Libraries/jquery.unobtrusive-ajax.min.js")
                .Include(scriptDirectory + "Libraries/modernizr-1.7.min.js")
                .Include(scriptDirectory + "PageReady.js")
                .Include(scriptDirectory + "AjaxFunctions.js")
                ;

            bundles.Add(bundle);
        }

        public static IHtmlString RenderStyles(this HtmlHelper htmlHelper)
        {
            return RenderBundle(htmlHelper, StyleBundlePath, false);
        }

        public static IHtmlString RenderScripts(this HtmlHelper htmlHelper)
        {
            return RenderBundle(htmlHelper, ScriptBundlePath, true);
        }

        private static IHtmlString RenderBundle(this HtmlHelper htmlHelper, string bundlePath, bool javaScript)
        {
            var httpContext = htmlHelper.ViewContext.HttpContext;
            var request = httpContext.Request;
            var response = httpContext.Response;

            var cacheCookie = httpContext.GetCacheCookie();

            var path = Styles.Url(bundlePath).ToString();
            var isCached = cacheCookie.IsFileCached(path);

            HtmlString result = null;

            if (!isCached)
            {
                cacheCookie.SetFile(path);

                var html = new StringBuilder();

                html.Append(javaScript ? "<script type=\"text/javascript\">" : "<style type=\"text/css\">");
                var bundleResponse = httpContext.Cache["System.Web.Optimization.Bundle:" + bundlePath] as BundleResponse;
                //TODO: log if bundleResponse is null
                if (bundleResponse != null)
                {
                    html.Append(bundleResponse.Content);
                    html.Append(javaScript ? "</script>" : "</style>");

                    cacheCookie.AddAFileForDownload(path);

                    result = new HtmlString(html.ToString());
                }

                cacheCookie.Save(response);
            }

            if(result == null)
            {
                var html = javaScript ? Scripts.Render(bundlePath).ToString() : Styles.Render(bundlePath).ToString();

                html = html.Replace("\n", "");

                result = new HtmlString(html);
            }

            return result;
        }
    }
}