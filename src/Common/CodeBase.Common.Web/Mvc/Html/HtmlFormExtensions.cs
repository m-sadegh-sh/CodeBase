namespace CodeBase.Common.Web.Mvc.Html {
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    public static class HtmlInputExtensions {
        public static MvcHtmlString Then(this HtmlHelper helper) {
            var then = helper.ViewBag.Then as string;

            return helper.Hidden(ThenAttribute.FieldName, then);
        }
    }
}