namespace CodeBase.Common.Web.Mvc.Html {
    using System;
    using System.Web;
    using System.Web.Mvc;

    public static class HtmlExtensions {
        public static IHtmlString Concat(this HtmlHelper helper, params IHtmlString[] values) {
            if (helper == null)
                throw new ArgumentNullException("helper");

            if (values == null)
                throw new ArgumentNullException("values");

            var concat = string.Join<IHtmlString>("", values);

            return MvcHtmlString.Create(concat);
        }

        public static IHtmlString Blank(this HtmlHelper helper) {
            if (helper == null)
                throw new ArgumentNullException("helper");

            return MvcHtmlString.Empty;
        }

        public static IHtmlString ToLower(this IHtmlString html) {
            if (html == null)
                throw new ArgumentNullException("html");

            return MvcHtmlString.Create(html.ToHtmlString().ToLower());
        }

        public static IHtmlString If(this HtmlHelper helper, bool condition, string output) {
            if (helper == null)
                throw new ArgumentNullException("helper");

            if (condition)
                return MvcHtmlString.Create(output);

            return null;
        }

        public static IHtmlString If(this HtmlHelper helper, bool condition, IHtmlString html) {
            if (helper == null)
                throw new ArgumentNullException("helper");

            if (condition)
                return html;

            return null;
        }
    }
}