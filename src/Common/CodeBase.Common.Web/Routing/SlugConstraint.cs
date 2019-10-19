namespace CodeBase.Common.Web.Routing {
    using System.Web;
    using System.Web.Routing;

    using CodeBase.Common.Infrastructure.Application;

    using MVC.Utilities.Routing;

    public sealed class SlugConstraint : IRouteConstraint {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection) {
            var value = values[parameterName].ToStringOrEmpty();

            var slug = value.ToSlug();

            return string.CompareOrdinal(value, slug) == 0;
        }
    }
}