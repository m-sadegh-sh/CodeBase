namespace CodeBase.Common.Web.Mvc {
    using System;
    using System.Web;
    using System.Web.Mvc;

    public class AuthAttribute : AuthorizeAttribute {
        private readonly bool _authorize;

        public AuthAttribute() {
            _authorize = true;
        }

        public AuthAttribute(bool authorize) {
            _authorize = authorize;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext) {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            if (!_authorize)
                return true;

            if (_authorize) {
                if (!httpContext.Request.IsAuthenticated)
                    return false;
            }

            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext) {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            filterContext.Result = new EmptyResult();

            filterContext.HttpContext.ExecuteAction("Authentication", "Login");
        }
    }
}