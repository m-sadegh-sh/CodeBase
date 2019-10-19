namespace CodeBase.Off.Website.Controllers {
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Core.Services;
    using CodeBase.Off.Website.Attributes;
    using CodeBase.Off.Website.AutoMapping;
    using CodeBase.Off.Website.Models;
    using CodeBase.Off.Website.Properties;

    using FarsiLibrary.Utils;

    public partial class EntryController : ControllerBase
    {
        private readonly IEntryService _entryService;

        public EntryController(IEntryService entryService) {
            _entryService = entryService;
        }

        [Admin]
        [HttpGet]
        public virtual ActionResult Add()
        {
            var model = new EntryEditModel();

            return View("Edit",
                        model);
        }

        [Admin]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual ActionResult Add(EntryEditModel model)
        {
            if (!ModelState.IsValid) {
                return View("Edit",
                            model);
            }

            if (_entryService.Exists(model.Slug)) {
                ModelState.AddModelError("Slug",
                                         Resources.Messages_DuplicateSlug);
                return View("Edit",
                            model);
            }

            var entry = model.To<EntryEditModel, Entry>();

            _entryService.Save(entry);

            return RedirectToAction("Show",
                                    "Entry",
                                    new {
                                            entry.Slug
                                    });
        }

        [Admin]
        [HttpGet]
        public virtual ActionResult Edit(string slug)
        {
            var entry = _entryService.Get(slug);

            if (entry == null) {
                throw new HttpException(404,
                                        "Entry not found");
            }

            var model = entry.To<Entry, EntryEditModel>();

            return View(model);
        }

        [Admin]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual ActionResult Edit(EntryEditModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var entry = _entryService.Get(model.Slug);

            if (entry == null) {
                throw new HttpException(404,
                                        "Entry not found");
            }

            model.To(entry);

            _entryService.Save(entry);

            return RedirectToAction("Show",
                                    "Entry",
                                    new {
                                            entry.Slug
                                    });
        }
    }
}