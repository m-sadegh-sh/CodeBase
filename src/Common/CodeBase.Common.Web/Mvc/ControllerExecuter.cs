namespace CodeBase.Common.Web.Mvc {
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using CodeBase.Common.Web.Routing;

    public static class ControllerExecuter {
        public static void ExecuteAction(this HttpContext httpContext, string controllerName, string actionName) {
            ExecuteAction(httpContext, controllerName, actionName, null);
        }

        public static void ExecuteAction(this HttpContext httpContext, string controllerName, string actionName, object additionalValues) {
            ExecuteAction(new HttpContextWrapper(httpContext), controllerName, actionName, additionalValues);
        }

        public static void ExecuteAction(this HttpContextBase httpContext, string controllerName, string actionName) {
            ExecuteAction(httpContext, controllerName, actionName, null);
        }

        public static void ExecuteAction(this HttpContextBase httpContext, string controllerName, string actionName, object additionalValues) {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            if (controllerName == null)
                throw new ArgumentNullException("controllerName");

            if (actionName == null)
                throw new ArgumentNullException("actionName");

            var routeValues = RouteParameter.Add("Controller", controllerName)
                .Append("Action", actionName);

            var additionalRouteValues = RouteValueDictionaryConvertor.Convert(additionalValues);
            if (additionalRouteValues.Keys.Any()) {
                foreach (var additionalRouteValue in additionalRouteValues)
                    routeValues.Add(additionalRouteValue.Key, additionalRouteValue.Value);
            }

            var routeData = new RouteData();
            routeData.Values.MergeWith(routeValues);

            var requestContext = new RequestContext(httpContext, routeData);

            var controller = ControllerBuilder.Current.GetControllerFactory()
                .CreateController(requestContext, controllerName);

            controller.Execute(requestContext);
        }
    }
}