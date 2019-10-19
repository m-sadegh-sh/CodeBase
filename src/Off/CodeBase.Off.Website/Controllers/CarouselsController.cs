namespace CodeBase.Off.Website.Controllers {
    using System.Linq;
    using System.Web.Mvc;

    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Core.Services;
    using CodeBase.Off.Website.AutoMapping;
    using CodeBase.Off.Website.Models;

    public partial class CarouselsController : ControllerBase
    {
        private readonly ICarouselService _carouselService;

        public CarouselsController(ICarouselService carouselService) {
            _carouselService = carouselService;
        }

        [ChildActionOnly]
        public virtual PartialViewResult Carousels()
        {
            var carousels = _carouselService.GetList().
                                             Where(c => c.IsActive).
                                             OrderBy(c => c.Order).
                                             ToList();

            var model = carousels.AllTo<Carousel, CarouselShowModel>();

            return PartialView("_Carousels",
                               model);
        }
    }
}