namespace CodeBase.Off.Website.Controllers {
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using CodeBase.Off.Core.Services;

    public sealed class ServicesController : ControllerBase {
        private readonly IServiceService _serviceService;

        public ServicesController(IServiceService serviceService) {
            _serviceService = serviceService;
        }

        [HttpGet]
        public ViewResult Index() {
            var model = _serviceService.GetList();

            return View(model);
        }

        [HttpGet]
        public ActionResult Show(string slug) {
            var model = _serviceService.Get(slug);

            if (model == null)
                throw new HttpException((int) HttpStatusCode.NotFound, "Service couldn't be not found.");

            return View(model);
        }
    }
}