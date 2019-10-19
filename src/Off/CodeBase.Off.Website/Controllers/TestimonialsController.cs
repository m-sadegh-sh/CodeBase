namespace CodeBase.Off.Website.Controllers {
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Core.Services;
    using CodeBase.Off.Website.AutoMapping;
    using CodeBase.Off.Website.Models;

    public partial class TestimonialsController : ControllerBase
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialsController(ITestimonialService testimonialService) {
            _testimonialService = testimonialService;
        }

        [ChildActionOnly]
        public virtual ViewResultBase List(bool isLight = false)
        {
            var testimonials = _testimonialService.GetList().
                                                   Where(t => t.IsActive).
                                                   OrderByDescending(t => t.Order).
                                                   ToList();

            var model = testimonials.AllTo<Testimonial, TestimonialShowModel>();

            return ViewOrPartialView(isLight ? "List.Light" : "List",
                                     model);
        }

        [HttpGet]
        public virtual RedirectResult RedirectTo(string slug)
        {
            var testimonial = _testimonialService.Get(slug);

            if (testimonial == null || !testimonial.IsActive) {
                throw new HttpException((int) HttpStatusCode.NotFound,
                                        "Testimonial couldn't be found.");
            }

            return Redirect(testimonial.Url);
        }
    }
}