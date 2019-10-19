namespace CodeBase.Off.Website.Controllers {
    using System.Web.Mvc;

    public sealed class HomeController : ControllerBase {
        [HttpGet]
        public ViewResult Index() {
            return View();
        }
    }
}