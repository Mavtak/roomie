using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace Roomie.Web.Website
{
    public static class BundleConfig
    {
        private const string bundlesPath = "~/content/";
        public const string StyleBundlePath = bundlesPath + "styles.css";
        public const string ScriptBundlePath = bundlesPath + "scripts.css";

        public static void RegisterBundes(BundleCollection bundles)
        {
            bundleStyles(bundles);
            bundleScripts(bundles);
            bundles.IgnoreList.Clear();
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
            var html = Styles.Render(StyleBundlePath).ToString().Replace("\n", "");
            var result = new HtmlString(html);

            return result;
        }

        public static IHtmlString RenderScripts(this HtmlHelper htmlHelper)
        {
            var html = Scripts.Render(ScriptBundlePath).ToString().Replace("\n", "");
            var result = new HtmlString(html);

            return result;
        }
    }
}