namespace CodeBase.Common.Web.Mvc {
    using System.Web.Mvc;

    public static class UrlHelperExtensions {
        public static string Action(this UrlHelper helper) {
            var actionName = helper.RequestContext.RouteData.GetRequiredString("Action");

            return helper.Action(actionName);
        }
    }
}