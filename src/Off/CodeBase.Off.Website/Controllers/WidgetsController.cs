namespace CodeBase.Off.Website.Controllers {
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using CodeBase.Common.Infrastructure.Application;
    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Core.Services;

    public class WidgetsController : ControllerBase {
        private readonly IFriendLinkService _friendLinkService;
        private readonly ISocialNetworkService _socialNetworkService;
        private readonly ITestimonialService _testimonialService;
        private readonly ICarouselService _carouselService;

        public WidgetsController(IFriendLinkService friendLinkService,
                                 ISocialNetworkService socialNetworkService,
                                 ITestimonialService testimonialService,
                                 ICarouselService carouselService) {
            _friendLinkService = friendLinkService;
            _socialNetworkService = socialNetworkService;
            _testimonialService = testimonialService;
            _carouselService = carouselService;
        }

        [ChildActionOnly]
        public PartialViewResult FriendLinks() {
            var model = _friendLinkService.GetList();

            return PartialView("_FriendLinks", model);
        }

        [HttpGet]
        public RedirectResult RedirectToFriendLink(string slug) {
            var friendLink = _friendLinkService.Get(slug);

            if (friendLink == null)
                throw new HttpException((int) HttpStatusCode.NotFound, "FriendLink couldn't be not found.");

            return Redirect(friendLink.Url);
        }

        [ChildActionOnly]
        public PartialViewResult SocialNetworks() {
            var model = _socialNetworkService.GetList();

            return PartialView("_SocialNetworks", model);
        }

        [HttpGet]
        public RedirectResult RedirectToSocialNetwork(string slug) {
            var socialNetwork = _socialNetworkService.Get(slug);

            if (socialNetwork == null)
                throw new HttpException((int) HttpStatusCode.NotFound, "SocialNetwork couldn't be not found.");

            return Redirect(socialNetwork.Url);
        }

        [ChildActionOnly]
        public PartialViewResult Testimonials() {
            var model = _testimonialService.GetList();

            return PartialView("_Testimonials", model);
        }

        [HttpGet]
        public RedirectResult RedirectToTestimonial(string slug) {
            var testimonial = _testimonialService.Get(slug);

            if (testimonial == null)
                throw new HttpException((int) HttpStatusCode.NotFound, "Testimonial couldn't be not found.");

            return Redirect(testimonial.Url);
        }

        [ChildActionOnly]
        public PartialViewResult Carousels() {
            var model = _carouselService.GetList()
                .Where(c => c.IsActive)
                .OrderBy(c => c.Order)
                .ToList();

            return PartialView("_Carousels", model);
        }

        [ChildActionOnly]
        public PartialViewResult GoogleAnalytics() {
            var config = IoC.Get<Config>();

            var analyticsId = config.Seo.GoogleAnalyticsId;

            if (analyticsId.AsNullIfEmpty() != null)
                return PartialView("_GoogleAnalytics", analyticsId);

            return null;
        }
    }
}