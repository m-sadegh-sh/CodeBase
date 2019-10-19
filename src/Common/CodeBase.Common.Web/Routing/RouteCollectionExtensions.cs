namespace CodeBase.Common.Web.Routing {
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    using MVC.Utilities.Routing;

    public static class RouteCollectionExtensions {
        public static void MapRouteLowerCase(this RouteCollection routes, string name, string url, object defaults) {
            routes.MapRouteLowerCase(name, url, defaults, null);
        }

        public static void MapRouteLowerCase(this RouteCollection routes, string name, string url, object defaults, object constraints) {
            if (routes == null)
                throw new ArgumentNullException("routes");

            if (url == null)
                throw new ArgumentNullException("url");

            var route = new LowerCaseRoute(url, new MvcRouteHandler()) {
                Defaults = RouteValueDictionaryConvertor.Convert(defaults),
                Constraints = RouteValueDictionaryConvertor.Convert(constraints)
            };

            if (String.IsNullOrEmpty(name))
                routes.Add(route);
            else
                routes.Add(name, route);
        }
    }
}