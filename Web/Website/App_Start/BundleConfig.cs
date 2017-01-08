using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website
{
    public static class BundleConfig
    {
        private const string bundlesPath = "~/content/";
        public const string StyleBundlePath = bundlesPath + "styles.css";

        private const string ResourceStrategyKey = "ResourceStrategy";
        private const string AlwaysInline = "inline";
        private const string AlwaysExternal = "external";
        private const string BeSmart = "smart";

        public static void RegisterBundes(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            bundleStyles(bundles);
            
            //BundleTable.EnableOptimizations = true;
        }

        private static void bundleStyles(BundleCollection bundles)
        {
            var stylesDirectory = "~/content/Styles/";

            var bundle = new StyleBundle(StyleBundlePath)
                .Include(stylesDirectory + "structure/site.css")
                .Include(stylesDirectory + "structure/content.css")
                .Include(stylesDirectory + "structure/widget.css")
                .Include(stylesDirectory + "structure/botMessage.css")
                .Include(stylesDirectory + "structure/roomieBot.css")
                .Include(stylesDirectory + "structure/icons.css")

                .Include(stylesDirectory + "color/site.css")
                .Include(stylesDirectory + "color/widget.css")
                .Include(stylesDirectory + "color/content.css")
                .Include(stylesDirectory + "color/roomieBot.css")
                .Include(stylesDirectory + "color/icons.css")
                ;

            bundles.Add(bundle);
        }

        public static IHtmlString RenderStyles(this HtmlHelper htmlHelper)
        {
            return RenderBundle(htmlHelper, StyleBundlePath, false);
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

            var resourceStrategy = (request[ResourceStrategyKey] ?? BeSmart).ToLower();

            if (resourceStrategy.Equals(AlwaysInline)
                || (resourceStrategy.Equals(BeSmart) && !isCached))
            {
                if (resourceStrategy.Equals(BeSmart))
                {
                    cacheCookie.SetFile(path);
                }

                result = GetInlineMarkup(htmlHelper, bundlePath, javaScript);

                if (resourceStrategy.Equals(BeSmart))
                {
                    if (result != null)
                    {
                        cacheCookie.AddAFileForDownload(path);
                    }

                    cacheCookie.Save(response);
                }
            }

            if(result == null)
            {
                var html = javaScript ? Scripts.Render(bundlePath).ToString() : Styles.Render(bundlePath).ToString();

                html = html.Replace("\n", "");

                result = new HtmlString(html);
            }

            return result;
        }

        private static HtmlString GetInlineMarkup(this HtmlHelper htmlHelper, string bundlePath, bool javaScript)
        {
            var httpContext = htmlHelper.ViewContext.HttpContext;
            var html = new StringBuilder();
            HtmlString result;

            html.Append(javaScript ? "<script type=\"text/javascript\">" : "<style type=\"text/css\">");
            var bundleResponse = httpContext.Cache["System.Web.Optimization.Bundle:" + bundlePath] as BundleResponse;
            //TODO: log if bundleResponse is null
            if (bundleResponse != null)
            {
                html.Append(bundleResponse.Content);
                html.Append(javaScript ? "</script>" : "</style>");

                result = new HtmlString(html.ToString());
            }
            else
            {
                result = null;
            }

            return result;
        }
    }
}