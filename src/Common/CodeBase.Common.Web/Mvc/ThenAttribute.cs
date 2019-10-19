namespace CodeBase.Common.Web.Mvc {
    using System.Web;
    using System.Web.Mvc;

    using CodeBase.Common.Infrastructure.Application;
    using CodeBase.Common.Web.Routing;

    public sealed class ThenAttribute : ActionFilterAttribute {
        public const string DefaultRouteName = "Home";
        public const string ParameterName = "then";
        public const string FieldName = "." + ParameterName;
        private readonly string[] _parameterNames = new[] {ParameterName, "url"};

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            var then = filterContext.RequestContext.HttpContext.Request.QueryString[FieldName];

            filterContext.Controller.ViewBag.Then = FormatOrRenewParameter(filterContext, then);
        }

        public bool ReferrerAllowed { get; set; }

        public string RouteName { get; set; }
        public object RouteValues { get; set; }

        private string FormatOrRenewParameter(ControllerContext controllerContext, string then) {
            var httpContext = controllerContext.RequestContext.HttpContext;
            var request = httpContext.Request;
            var server = httpContext.Server;

            if (string.IsNullOrWhiteSpace(then)) {
                if (ReferrerAllowed && request.UrlReferrer != null) {
                    var reffererUrl = request.UrlReferrer.ToString();

                    if (httpContext.Url().IsLocalUrl(reffererUrl))
                        then = reffererUrl;
                }

                if (then.AsNullIfEmpty() == null) {
                    if (RouteName.AsNullIfEmpty() == null) {
                        RouteName = DefaultRouteName;
                        RouteValues = null;
                    }

                    var defaultUrl = RouteName.GetUrl(RouteValues);

                    if (httpContext.Url().IsLocalUrl(defaultUrl))
                        then = defaultUrl;
                }
            }

            then = FilterMultipleThensIfExists(server, then);

            return then;
        }

        private string FilterMultipleThensIfExists(HttpServerUtilityBase server, string then) {
            then = server.UrlDecode(then);

            foreach (var parameterName in _parameterNames) {
                var indexOfThen = then.IndexOf(parameterName + "=");

                while (indexOfThen > 0) {
                    indexOfThen += parameterName.Length + 1;
                    then = then.Substring(indexOfThen, then.Length - indexOfThen);

                    indexOfThen = then.IndexOf(parameterName + "=");
                }
            }

            then = server.UrlEncode(then);

            return then;
        }
    }
}