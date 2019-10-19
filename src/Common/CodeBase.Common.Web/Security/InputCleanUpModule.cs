namespace CodeBase.Common.Web.Security {
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Web;

    public class InputCleanUpModule : IHttpModule {
        private static readonly Regex _cleanAllTags = new Regex("<[^>]+>", RegexOptions.Compiled);

        private static readonly IList<string> _ignoreList = new List<string>();

        private static readonly PropertyInfo _readonlyProperty = typeof (NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);

        private HttpApplication _context;

        public void Init(HttpApplication context) {
            _context = context;

            _context.BeginRequest += CleanUpInput;
        }

        private static void CleanUpInput(object sender, EventArgs e) {
            var request = ((HttpApplication) sender).Request;

            CleanUpAndEncodeQueryStrings(request.QueryString);

            if (request.HttpMethod == "POST")
                CleanUpAndEncodeFormFields(request.Form);

            CleanUpAndEncodeCookies(request.Cookies);
        }

        private static void CleanUpAndEncodeQueryStrings(NameValueCollection queryStrings) {
            _readonlyProperty.SetValue(queryStrings, false, null);

            foreach (var key in queryStrings.AllKeys) {
                var origData = queryStrings[key];

                if (string.IsNullOrWhiteSpace(origData))
                    continue;

                origData = origData.Trim();

                var modifiedData = HttpUtility.HtmlEncode(_cleanAllTags.Replace(origData, string.Empty));

                if (origData != modifiedData)
                    queryStrings[key] = modifiedData;
            }

            _readonlyProperty.SetValue(queryStrings, true, null);
        }

        private static void CleanUpAndEncodeFormFields(NameValueCollection formFields) {
            _readonlyProperty.SetValue(formFields, false, null);

            foreach (var key in formFields.AllKeys) {
                var origData = formFields[key];

                if (string.IsNullOrWhiteSpace(origData))
                    continue;

                origData = origData.Trim();

                if (_ignoreList.Contains(key))
                    continue;

                var modifiedData = origData.ToSafeHtml();

                if (origData != modifiedData)
                    formFields[key] = modifiedData;
            }

            _readonlyProperty.SetValue(formFields, true, null);
        }

        private static void CleanUpAndEncodeCookies(HttpCookieCollection cookies) {
            foreach (var key in cookies.AllKeys) {
                var cookie = cookies[key];

                if (cookie == null)
                    continue;

                foreach (var cookieKey in cookie.Values.AllKeys) {
                    var origData = cookie.Values[cookieKey];

                    if (string.IsNullOrWhiteSpace(origData))
                        continue;

                    origData = origData.Trim();

                    var modifiedData = HttpUtility.HtmlEncode(_cleanAllTags.Replace(origData, string.Empty));

                    if (origData != modifiedData)
                        cookie.Values[cookieKey] = modifiedData;
                }
            }
        }

        public void Dispose() {}
    }
}