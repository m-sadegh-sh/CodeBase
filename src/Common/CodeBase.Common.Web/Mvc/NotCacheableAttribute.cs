namespace CodeBase.Common.Web.Mvc {
    using System;
    using System.Web.Mvc;

    public sealed class NotCacheableAttribute : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            var cache = filterContext.HttpContext.Response.Cache;
            cache.SetProxyMaxAge(new TimeSpan(0));
        }
    }
}