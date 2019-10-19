namespace CodeBase.Off.Website.Controllers {
    using System;
    using System.Linq;
    using System.ServiceModel.Syndication;
    using System.Web.Mvc;

    using CodeBase.Common.Web.Mvc;
    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Core.Services;
    using CodeBase.Off.Website.AutoMapping;

    public sealed class FeedController : ControllerBase {
        private readonly IEntryService _entryService;

        public FeedController(IEntryService entryService) {
            _entryService = entryService;
        }

        [HttpGet]
        public ActionResult Index() {
            var model = _entryService.GetList()
                .OrderByDescending(e => e.CreateDateUtc)
                .Take(10)
                .Select(e => e.To<Entry, SyndicationItem>());

            var feed = new SyndicationFeed(CurrentConfig.Heading,
                                           CurrentConfig.Tagline,
                                           new Uri(Url.Action("Index", "Feed", null, Uri.UriSchemeHttp)),
                                           model);

            return new RssResult(feed);
        }
    }
}