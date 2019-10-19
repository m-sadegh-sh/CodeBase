namespace CodeBase.Common.Web.Routing {
    using System;
    using System.Web;
    using System.Web.Routing;

    using CodeBase.Common.Infrastructure.Application;

    public sealed class GuidConstraint : IRouteConstraint {
        public bool Match(HttpContextBase httpContext,
                          Route route,
                          string parameterName,
                          RouteValueDictionary values,
                          RouteDirection routeDirection) {
            var value = values[parameterName].ToStringOrEmpty();
            Guid temp;

            return Guid.TryParse(value,
                                 out temp) && temp != Guid.Empty;
        }
    }
}