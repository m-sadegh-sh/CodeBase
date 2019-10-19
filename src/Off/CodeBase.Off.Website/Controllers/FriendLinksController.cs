namespace CodeBase.Off.Website.Controllers {
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Core.Services;
    using CodeBase.Off.Website.AutoMapping;
    using CodeBase.Off.Website.Models;

    public partial class FriendLinksController : ControllerBase
    {
        private readonly IFriendLinkService _friendLinkService;

        public FriendLinksController(IFriendLinkService friendLinkService) {
            _friendLinkService = friendLinkService;
        }

        [ChildActionOnly]
        public virtual ViewResultBase List(bool isLight = false)
        {
            var friendLinks = _friendLinkService.GetList().
                                                 Where(fl => fl.IsActive).
                                                 OrderByDescending(fl => fl.Order).
                                                 ToList();

            var model = friendLinks.AllTo<FriendLink, FriendLinkShowModel>();

            return ViewOrPartialView(isLight ? "_List.Light" : "_List",
                                     model);
        }

        [HttpGet]
        public virtual RedirectResult RedirectTo(string slug)
        {
            var friendLink = _friendLinkService.Get(slug);

            if (friendLink == null || !friendLink.IsActive) {
                throw new HttpException((int) HttpStatusCode.NotFound,
                                        "FriendLink couldn't be found.");
            }

            return Redirect(friendLink.Url);
        }
    }
}