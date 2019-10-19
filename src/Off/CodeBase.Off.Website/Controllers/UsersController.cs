namespace CodeBase.Off.Website.Controllers {
    using System.Linq;
    using System.Web.Mvc;

    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Core.Services;
    using CodeBase.Off.Website.AutoMapping;
    using CodeBase.Off.Website.Models;

    public sealed partial class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) {
            _userService = userService;
        }

        [ChildActionOnly]
        public virtual ViewResultBase List(bool isLight = false)
        {
            var users = _userService.GetList().
                                     Where(t => t.IsActive).
                                     ToList();

            var model = users.AllTo<User, UserSummaryModel>().
                              OrderByDescending(usm => usm.EntriesCount).
                              ToList();

            return ViewOrPartialView(isLight ? "List.Light" : "List",
                                     model);
        }
    }
}