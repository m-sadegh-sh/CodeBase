namespace CodeBase.Off.Website.Controllers {
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Core.Services;
    using CodeBase.Off.Website.AutoMapping;
    using CodeBase.Off.Website.Models;

    public partial class SocialNetworksController : ControllerBase
    {
        private readonly ISocialNetworkService _socialNetworkService;

        public SocialNetworksController(ISocialNetworkService socialNetworkService) {
            _socialNetworkService = socialNetworkService;
        }

        [ChildActionOnly]
        public virtual ViewResultBase List(bool isLight = false)
        {
            var socialNetworks = _socialNetworkService.GetList().
                                                       Where(sn => sn.IsActive).
                                                       OrderByDescending(sn => sn.Order).
                                                       ToList();

            var model = socialNetworks.AllTo<SocialNetwork, SocialNetworkShowModel>();

            return ViewOrPartialView(isLight ? "List.Light" : "List",
                                     model);
        }

        [HttpGet]
        public virtual RedirectResult RedirectTo(string slug)
        {
            var socialNetwork = _socialNetworkService.Get(slug);

            if (socialNetwork == null || !socialNetwork.IsActive) {
                throw new HttpException((int) HttpStatusCode.NotFound,
                                        "SocialNetwork couldn't be found.");
            }

            return Redirect(socialNetwork.Url);
        }
    }
}