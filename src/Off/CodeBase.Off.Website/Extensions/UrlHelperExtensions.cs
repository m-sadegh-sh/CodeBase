namespace CodeBase.Off.Website.Extensions {
    using System.Web.Mvc;

    using CodeBase.Off.Core.Web.Mvc;

    public static class UrlHelperExtensions {
        public static string MemberImageUrl(this UrlHelper helper, int userId, bool thumb) {
            return helper.Image("Team-Members/" + userId + (thumb ? "-thumb" : "") + ".png");
        }

        public static string MemberResumeUrl(this UrlHelper helper, int userId) {
            return helper.Cdn(false, "Resumes/", userId.ToString(), ".pdf");
        }

        public static string PortfolioImageUrl(this UrlHelper helper, string slug, bool thumb) {
            return helper.Image("Portfolios/" + slug + (thumb ? "-thumb" : "") + ".png");
        }

        public static string ServiceIconUrl(this UrlHelper helper, string slug, bool thumb) {
            return helper.Image("Services/" + slug + (thumb ? "-thumb" : "") + ".png");
        }

        public static string CarouselImageUrl(this UrlHelper helper, int id) {
            return helper.Image("Carousels/" + id + ".png");
        }
    }
}