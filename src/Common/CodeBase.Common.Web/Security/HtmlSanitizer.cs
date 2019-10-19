namespace CodeBase.Common.Web.Security {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class HtmlSanitizer {
        private static readonly Regex _htmlTagExpression = new Regex(@"(?'tag_start'</?)(?'tag'\w+)((\s+(?'attr'(?'attr_name'\w+)(\s*=\s*(?:"".*?""|'.*?'|[^'"">\s]+)))?)+\s*|\s*)(?'tag_end'/?>)", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Dictionary<string, List<string>> _validHtmlTags = new Dictionary<string, List<string>> {
            {"p", new List<string>()},
            {"br", new List<string>()},
            {"strong", new List<string>()},
            {"b", new List<string>()},
            {"em", new List<string>()},
            {"i", new List<string>()},
            {"u", new List<string>()},
            {"strike", new List<string>()},
            {"ol", new List<string>()},
            {"ul", new List<string>()},
            {"li", new List<string>()}, {
                "a", new List<string> {
                    "href"
                }
            }, {
                "img", new List<string> {
                    "src",
                    "height",
                    "width",
                    "alt"
                }
            }, {
                "q", new List<string> {
                    "cite"
                }
            },
            {"cite", new List<string>()},
            {"abbr", new List<string>()},
            {"acronym", new List<string>()},
            {"del", new List<string>()},
            {"ins", new List<string>()}
        };

        public static string ToSafeHtml(this string text) {
            return text.RemoveInvalidHtmlTags();
        }

        public static string RemoveInvalidHtmlTags(this string text) {
            return _htmlTagExpression.Replace(text, m => {
                if (!_validHtmlTags.ContainsKey(m.Groups["tag"].Value))
                    return String.Empty;

                var generatedTag = new StringBuilder(m.Length);

                var tagStart = m.Groups["tag_start"];
                var tagEnd = m.Groups["tag_end"];
                var tag = m.Groups["tag"];
                var tagAttributes = m.Groups["attr"];

                generatedTag.Append(tagStart.Success ? tagStart.Value : "<");
                generatedTag.Append(tag.Value);

                foreach (Capture attr in tagAttributes.Captures) {
                    var indexOfEquals = attr.Value.IndexOf('=');

                    if (indexOfEquals < 1)
                        continue;

                    var attrName = attr.Value.Substring(0, indexOfEquals);

                    if (!_validHtmlTags[tag.Value].Contains(attrName))
                        continue;
                    generatedTag.Append(' ');
                    generatedTag.Append(attr.Value);
                }

                if (tagStart.Success && tagStart.Value == "<" && tag.Value.Equals("a", StringComparison.OrdinalIgnoreCase))
                    generatedTag.Append(" rel=\"nofollow\"");

                generatedTag.Append(tagEnd.Success ? tagEnd.Value : ">");

                return generatedTag.ToString();
            });
        }
    }
}