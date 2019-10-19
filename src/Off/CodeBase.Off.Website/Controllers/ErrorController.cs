namespace CodeBase.Off.Website.Controllers {
    using System.Web;
    using System.Web.Mvc;

    using CodeBase.Off.Core.Services.Impl;
    using CodeBase.Off.Website.Models;
    using CodeBase.Off.Website.Properties;

    public partial class ErrorController : ControllerBase {
        public virtual ActionResult Index(string catchAll,
                                          HttpException httpException) {
            var model = new ErrorModel();

            LogService.Error(httpException);

            var httpCode = httpException.GetHttpCode();

            switch (httpCode) {
                case 403:
                    Response.StatusCode = 403;
                    model.Heading = Resources.Messages_Error_403;
                    model.Message = Resources.Messages_Error_403Description;
                    break;
                case 404:
                    Response.StatusCode = 404;
                    model.Heading = Resources.Messages_Error_404;
                    model.Message = Resources.Messages_Error_404Description;
                    break;
                case 500:
                    Response.StatusCode = 500;
                    model.Heading = Resources.Messages_Error_500;
                    model.Message = Resources.Messages_Error_500Description;
                    break;
                case 503:
                    Response.StatusCode = 503;
                    model.Heading = Resources.Messages_Error_503;
                    model.Message = string.Format(Resources.Messages_Error_503Description,
                                                  CurrentConfig.CloseReason);
                    break;
            }

            Response.TrySkipIisCustomErrors = true;

            if (IsPartialNeeded) {
                return Json(model,
                            JsonRequestBehavior.AllowGet);
            }

            return View(Views.Index,
                        model);
        }
    }
}