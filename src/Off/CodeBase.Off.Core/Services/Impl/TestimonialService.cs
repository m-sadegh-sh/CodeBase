namespace CodeBase.Off.Core.Services.Impl {
    using System.Collections.Generic;
    using System.Linq;

    using CodeBase.Common.Infrastructure.Storage;
    using CodeBase.Off.Core.Domain;

    public sealed class TestimonialService : ITestimonialService {
        private readonly IRepository _repository;

        public TestimonialService(IRepository repository) {
            _repository = repository;
        }

        public bool Exists(string slug) {
            if (string.IsNullOrEmpty(slug))
                return false;

            return _repository.Exists<Testimonial>(slug);
        }

        public Testimonial Get(string slug) {
            if (string.IsNullOrEmpty(slug))
                return null;

            return _repository.Single<Testimonial>(slug);
        }

        public List<Testimonial> GetList() {
            return _repository.All<Testimonial>().ToList();
        }

        public void Save(Testimonial testimonial) {
            testimonial.Slug = testimonial.Slug.ToLowerInvariant();

            _repository.Save(testimonial);
        }

        public void Delete(string slug) {
            if (string.IsNullOrEmpty(slug))
                return;

            _repository.Delete<Testimonial>(slug);
        }
    }
}