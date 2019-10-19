namespace CodeBase.Common.Web.Routing {
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class RouteUrlProvider {
        public static UrlHelper Url(this HttpContext context) {
            return Url(new HttpContextWrapper(context));
        }

        public static UrlHelper Url(this HttpContextBase context) {
            var requestContext = new RequestContext(context, new RouteData());

            var urlHelper = new UrlHelper(requestContext, RouteTable.Routes);

            return urlHelper;
        }

        public static string GetUrl(this string controllerName, string actionName, object routeValues = null) {
            var urlHelper = HttpContext.Current.Url();

            var url = urlHelper.Action(controllerName, actionName, routeValues);

            return url;
        }

        public static string GetUrl(this string routeName, object routeValues = null) {
            var urlHelper = HttpContext.Current.Url();

            var url = urlHelper.RouteUrl(routeName, routeValues);

            return url;
        }
    }
}