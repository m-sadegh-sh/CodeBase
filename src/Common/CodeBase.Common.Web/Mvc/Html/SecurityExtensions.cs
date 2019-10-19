namespace CodeBase.Common.Web.Mvc.Html {
    using System;
    using System.Web;
    using System.Web.Mvc;

    using CodeBase.Common.Web.Security;

    public static class SecurityExtensions {
        public static IHtmlString Safe(this HtmlHelper helper, string unsafeHtml) {
            if (helper == null)
                throw new ArgumentNullException("helper");

            if (unsafeHtml == null)
                throw new ArgumentNullException("unsafeHtml");

            var safeHtml = unsafeHtml.ToSafeHtml();

            return MvcHtmlString.Create(safeHtml);
        }
    }
}