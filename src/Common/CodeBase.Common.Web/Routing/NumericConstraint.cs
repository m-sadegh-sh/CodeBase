namespace CodeBase.Common.Web.Routing {
    using System.Web;
    using System.Web.Routing;

    using CodeBase.Common.Infrastructure.Application;

    public sealed class NumericConstraint : IRouteConstraint {
        private readonly bool _positive;

        public NumericConstraint(bool positive) {
            _positive = positive;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection) {
            var value = values[parameterName].ToStringOrEmpty();

            int number;
            var isValid = int.TryParse(value, out number);

            if (!isValid)
                return false;

            if (_positive && number <= 0)
                return false;

            return true;
        }
    }
}