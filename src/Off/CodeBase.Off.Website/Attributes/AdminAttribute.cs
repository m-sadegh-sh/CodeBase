namespace CodeBase.Off.Website.Attributes {
    using System;
    using System.Web;
    using System.Web.Mvc;

    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Common.Web.Mvc;
    using CodeBase.Off.Core.Domain;

    public sealed class AdminAttribute : AuthAttribute {
        protected override bool AuthorizeCore(HttpContextBase httpContext) {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            if (!base.AuthorizeCore(httpContext))
                return false;

            var currentUser = IoC.Get<User>();

            return currentUser.IsAdmin;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext) {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            filterContext.Result = new EmptyResult();

            filterContext.HttpContext.ExecuteAction("Authentication", "Login");
        }
    }
}