namespace CodeBase.Common.Web.Mvc.Layout {
    using System;
    using System.Web;

    public interface ILayoutHelpers {
        void AddTitle(string title);
        string GetTitle(string separator, string defaultTitle = null, bool onlyAppendIfEmpty = false);
        void AddKeywords(params string[] keywords);
        string GetKeywords(string defaultKeywords = null, bool onlyAppendIfEmpty = false);
        void AddDescription(string description);
        string GetDescription(string defaultDescription = null, bool onlyAppendIfEmpty = false);
        void AddStyle(string style);
        IHtmlString GetStyles();
        void AddScript(string script);
        IHtmlString GetScripts();
        void AddCanonicalUrl(string canonicalUrl);
        IHtmlString GetCanonicalUrls();
        void AddBreadcrumb(Func<IHtmlString> breadcrumb);
        IHtmlString GetBreadcrumbs(string separator);
    }
}