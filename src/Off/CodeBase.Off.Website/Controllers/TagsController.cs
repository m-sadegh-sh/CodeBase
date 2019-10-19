namespace CodeBase.Off.Website.Controllers {
    using System.Linq;
    using System.Web.Mvc;

    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Core.Services;
    using CodeBase.Off.Website.AutoMapping;
    using CodeBase.Off.Website.Models;

    public partial class TagsController : ControllerBase {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService) {
            _tagService = tagService;
        }

        [ChildActionOnly]
        public virtual ActionResult Cloud() {
            var tags = _tagService.GetList().
                                   Where(t => t.IsActive).
                                   ToList();

            var model = tags.AllTo<Tag, TagShowModel>().
                             OrderByDescending(tsm => tsm.EntriesCount).
                             ToList();

            return PartialView(Views._Cloud,
                               model);
        }
    }
}