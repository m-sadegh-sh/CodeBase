namespace CodeBase.Off.Website.Controllers {
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Core.Services;
    using CodeBase.Off.Website.AutoMapping;
    using CodeBase.Off.Website.Models;
    using CodeBase.Off.Website.Properties;

    using PagedList;

    public sealed class SearchController : ControllerBase {
        private readonly IEntryService _entryService;

        public SearchController(IEntryService entryService) {
            _entryService = entryService;
        }

        [HttpGet]
        public ActionResult Index(string q, int page) {
            var nonPaged = _entryService.GetList().Where(e => e.Title.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0)
                .Select(e => e.To<Entry, SearchResultModel>()).ToList();

            var model = nonPaged.ToPagedList(page, CurrentConfig.PageSize);

            if (model.Count == 0 && page > 1) {
                WarningNotification(Resources.Messages_InvalidParameter, false);

                model = nonPaged.ToPagedList(1, CurrentConfig.PageSize);
            }

            return View(model);
        }
    }
}