namespace CodeBase.Common.Web.Mvc.Layout {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    using CodeBase.Common.Infrastructure.Application;

    public sealed class LayoutHelpers : ILayoutHelpers {
        private readonly List<string> _titles;
        private readonly List<string> _keywords;
        private readonly List<string> _descriptions;
        private readonly List<string> _styles;
        private readonly List<string> _scripts;
        private readonly List<string> _canonicalUrls;
        private readonly List<Func<IHtmlString>> _breadcrumbs;

        public LayoutHelpers() {
            _titles = new List<string>();
            _keywords = new List<string>();
            _descriptions = new List<string>();
            _styles = new List<string>();
            _scripts = new List<string>();
            _canonicalUrls = new List<string>();
            _breadcrumbs = new List<Func<IHtmlString>>();
        }

        public void AddTitle(string title) {
            AddParts(_titles, title);
        }

        public string GetTitle(string separator, string defaultTitle = null, bool onlyAppendIfEmpty = false) {
            if ((onlyAppendIfEmpty && _titles.IsEmpty()) || defaultTitle.AsNullIfEmpty() != null)
                AddTitle(defaultTitle);

            var result = string.Join(separator, _titles.AsEnumerable().Reverse().ToArray());

            return result.Trim();
        }

        public void AddKeywords(params string[] keywords) {
            if (keywords.IsEmpty()) {
                foreach (var keyword in keywords)
                    AddParts(_keywords, keyword);
            }
        }

        public string GetKeywords(string defaultKeywords = null, bool onlyAppendIfEmpty = false) {
            if ((onlyAppendIfEmpty && _keywords.IsEmpty()) || defaultKeywords.AsNullIfEmpty() != null)
                AddKeywords(defaultKeywords);

            var result = string.Join(", ", _keywords.AsEnumerable().Reverse().ToArray());

            return result.Trim();
        }

        public void AddDescription(string description) {
            AddParts(_descriptions, description);
        }

        public string GetDescription(string defaultDescription = null, bool onlyAppendIfEmpty = false) {
            if ((onlyAppendIfEmpty && _descriptions.IsEmpty()) || defaultDescription.AsNullIfEmpty() != null)
                AddDescription(defaultDescription);

            var result = string.Join(", ", _descriptions.AsEnumerable().Reverse().ToArray());

            return result.Trim();
        }

        public void AddStyle(string relativeFilePath) {
            AddParts(_styles, relativeFilePath);
        }

        public IHtmlString GetStyles() {
            return MvcHtmlString.Create(Generate("<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />", _styles));
        }

        public void AddScript(string relativeFilePath) {
            AddParts(_scripts, relativeFilePath);
        }

        public IHtmlString GetScripts() {
            return MvcHtmlString.Create(Generate("<script src=\"{0}\" type=\"text/javascript\"></script>", _scripts));
        }

        public void AddCanonicalUrl(string canonicalUrl) {
            AddParts(_canonicalUrls, canonicalUrl);
        }

        public IHtmlString GetCanonicalUrls() {
            return MvcHtmlString.Create(Generate("<link rel=\"canonical\" href=\"{0}\" />", _canonicalUrls));
        }

        public void AddBreadcrumb(Func<IHtmlString> breadcrumb) {
            AddParts(_breadcrumbs, breadcrumb);
        }

        public IHtmlString GetBreadcrumbs(string separator) {
            var result = new StringBuilder();

            _breadcrumbs.Reverse();

            foreach (var breadcrumbPart in _breadcrumbs) {
                result.Append("<li>");

                result.Append(breadcrumbPart);

                if (!_breadcrumbs.IsLast(breadcrumbPart))
                    result.AppendFormat("<span class=\"divider\">{0}</span>", separator);

                result.Append("</li>");
            }

            return MvcHtmlString.Create(result.ToString());
        }

        private static void AddParts<T>(IList<T> collection, T part) where T : class {
            if (part == null)
                return;

            collection.Insert(0, part);
        }

        private static string Generate(string pattern, IEnumerable<string> items) {
            var result = new StringBuilder();

            foreach (var item in items) {
                result.AppendFormat(pattern, item);
                result.Append(Environment.NewLine);
            }

            return result.ToString();
        }
    }
}