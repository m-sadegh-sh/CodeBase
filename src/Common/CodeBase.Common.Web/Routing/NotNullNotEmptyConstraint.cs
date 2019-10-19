namespace CodeBase.Common.Web.Routing {
    using System.Web;
    using System.Web.Routing;

    using CodeBase.Common.Infrastructure.Application;

    public sealed class NotNullNotEmptyConstraint : IRouteConstraint {
        public bool Match(HttpContextBase httpContext,
                          Route route,
                          string parameterName,
                          RouteValueDictionary values,
                          RouteDirection routeDirection) {
            var value = values[parameterName].ToStringOrEmpty();

            return !string.IsNullOrWhiteSpace(value);
        }
    }
}