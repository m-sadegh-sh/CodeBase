namespace CodeBase.Off.Website.Controllers {
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Core.Services;
    using CodeBase.Off.Website.AutoMapping;
    using CodeBase.Off.Website.Models;

    public sealed partial class BlogController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITeamMemberService _teamMemberService;
        private readonly IEntryService _entryService;

        public BlogController(IUserService userService, ITeamMemberService teamMemberService, IEntryService entryService) {
            _userService = userService;
            _teamMemberService = teamMemberService;
            _entryService = entryService;
        }

        [HttpGet]
        public virtual ViewResult Index()
        {
            var model = _entryService.GetList()
                .OrderByDescending(e => e.CreateDateUtc)
                .Select(e => e.To<Entry, EntrySummaryModel>());

            return View(model);
        }

        [ChildActionOnly]
        public virtual PartialViewResult RecentEntries()
        {
            var model = _entryService.GetList()
                .OrderByDescending(e => e.CreateDateUtc)
                .Take(3)
                .Select(e => e.To<Entry, EntrySummaryModel>())
                .ToList();

            return PartialView("_RecentEntries", model);
        }

        [HttpGet]
        public virtual ViewResult ByAuthor(string slug)
        {
            User user;
            var teamMember = _teamMemberService.Get(slug);

            if (teamMember == null)
                user = _userService.Get(slug);
            else
                user = _userService.Get(teamMember.UserId);

            if (user == null)
                throw new HttpException((int) HttpStatusCode.NotFound, "User couldn't be found.");

            var model = _entryService.GetList().Where(e => e.AuthorId == user.Id)
                .OrderByDescending(e => e.CreateDateUtc)
                .Select(e => e.To<Entry, EntrySummaryModel>());

            return View(model);
        }

        [HttpGet]
        public virtual ViewResult ByDate(int year, int? month, int? day)
        {
            var query = _entryService.GetList().AsQueryable().Where(e => e.CreateDateUtc.Year == year);

            if (month.HasValue)
                query = query.Where(e => e.CreateDateUtc.Month == month);

            if (day.HasValue)
                query = query.Where(e => e.CreateDateUtc.Day == day);

            var model = query.OrderByDescending(e => e.CreateDateUtc)
                .Select(e => e.To<Entry, EntrySummaryModel>());

            return View(model);
        }
    }
}