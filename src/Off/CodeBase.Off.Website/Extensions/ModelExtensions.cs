namespace CodeBase.Off.Website.Extensions {
    using System.Collections.Generic;
    using System.Linq;

    using CodeBase.Common.Infrastructure.Application;
    using CodeBase.Off.Website.Models;

    using Utilities.Web.ExtensionMethods;

    public static class ModelExtensions {
        public static string ToMetaKeywords(this IList<TagShowModel> tagModels) {
            return string.Join(",",
                               tagModels.Select(t => t.Title));
        }

        public static string ToMetaDescription(this string value,
                                               bool stripHtml = false) {
            if (stripHtml)
                value = value.StripHTML();

            value = value.EnsureLength(500);

            return value;
        }
    }
}