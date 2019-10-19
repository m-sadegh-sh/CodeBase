namespace CodeBase.Off.Website.Controllers {
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using CodeBase.Off.Core.Services;

    public sealed class PortfolioController : ControllerBase {
        private readonly IPortfolioService _portfolioService;

        public PortfolioController(IPortfolioService portfolioService) {
            _portfolioService = portfolioService;
        }

        [HttpGet]
        public ViewResult Index() {
            var model = _portfolioService.GetList();

            return View(model);
        }

        [HttpGet]
        public ActionResult Show(string slug) {
            var model = _portfolioService.Get(slug);

            if (model == null)
                throw new HttpException((int) HttpStatusCode.NotFound, "Portfolio couldn't be not found.");

            return View(model);
        }
    }
}