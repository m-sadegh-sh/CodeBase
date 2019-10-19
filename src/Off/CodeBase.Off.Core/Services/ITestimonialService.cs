namespace CodeBase.Off.Core.Services {
    using System.Collections.Generic;

    using CodeBase.Off.Core.Domain;

    public interface ITestimonialService {
        bool Exists(string slug);
        Testimonial Get(string slug);
        List<Testimonial> GetList();
        void Save(Testimonial testimonial);
        void Delete(string slug);
    }
}