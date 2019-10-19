namespace CodeBase.Off.Website.Controllers {
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using CodeBase.Common.Infrastructure.Application;
    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Core.Services;
    using CodeBase.Off.Website.AutoMapping;
    using CodeBase.Off.Website.Extensions;
    using CodeBase.Off.Website.Helpers;
    using CodeBase.Off.Website.Models;

    using IOFile = System.IO.File;

    public partial class AboutController : ControllerBase {
        private readonly IUserService _userService;
        private readonly ITeamMemberService _teamMemberService;

        public AboutController(IUserService userService,
                               ITeamMemberService teamMemberService) {
            _userService = userService;
            _teamMemberService = teamMemberService;
        }

        [HttpGet]
        public virtual ActionResult Index() {
            return View(Views.Index);
        }

        [ChildActionOnly]
        public virtual ActionResult Members(string userName,
                                            string emptyTemplate) {
            var members = _teamMemberService.GetList().
                                             Where(tm => tm.IsActive).
                                             OrderByDescending(tm => tm.Order).
                                             ToList();

            if (!string.IsNullOrEmpty(userName)) {
                var user = _userService.Get(userName);

                if (user == null) {
                    throw new HttpException((int) HttpStatusCode.NotFound,
                                            "User couldn't be found.");
                }

                members = members.Where(tm => tm.UserId != user.Id).
                                  ToList();
            }

            ViewData[ViewDataKeys.EmptyTemplate] = emptyTemplate;

            var model = members.AllTo<TeamMember, TeamMemberSummaryModel>();

            return PartialView(Views._Members,
                               model);
        }

        [HttpGet]
        public virtual ActionResult Show(string userName) {
            var user = _userService.Get(userName);

            if (user == null) {
                throw new HttpException((int) HttpStatusCode.NotFound,
                                        "User couldn't be found.");
            }

            var member = _teamMemberService.Get(user.Id);

            if (member == null) {
                throw new HttpException((int) HttpStatusCode.NotFound,
                                        "TeamMember couldn't be found.");
            }

            var model = member.To<TeamMember, TeamMemberShowModel>();

            return View(Views.Show,
                        model);
        }

        [HttpPost]
        [ActionName("Show")]
        public virtual ActionResult ShowPost(string userName) {
            var user = _userService.Get(userName);

            if (user == null) {
                throw new HttpException((int) HttpStatusCode.NotFound,
                                        "User couldn't be found.");
            }

            var member = _teamMemberService.Get(user.Id);

            if (member == null) {
                throw new HttpException((int) HttpStatusCode.NotFound,
                                        "TeamMember couldn't be found.");
            }

            var resumeFilePath = Url.MemberResume(member.UserId).
                                     ToFullPath();

            if (!IOFile.Exists(resumeFilePath)) {
                throw new HttpException((int) HttpStatusCode.NotFound,
                                        "Resume file couldn't be found.");
            }

            var model = member.To<TeamMember, TeamMemberSummaryModel>();

            return Pdf(resumeFilePath,
                       model.FullName);
        }
    }
}