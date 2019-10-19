namespace CodeBase.Common.Web.Mvc {
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    public static class HtmlHelperExtensions {
        public static MvcForm BeginForm(this HtmlHelper helper, FormMethod method, object htmlAttributes) {
            var actionName = helper.ViewContext.RouteData.GetRequiredString("Action");
            var controllerName = helper.ViewContext.RouteData.GetRequiredString("Controller");

            return helper.BeginForm(actionName, controllerName, method, htmlAttributes);
        }

        public static MvcHtmlString Action(this HtmlHelper helper) {
            var actionName = helper.ViewContext.RouteData.GetRequiredString("Action");

            return helper.Action(actionName);
        }
    }
}