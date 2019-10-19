namespace CodeBase.Off.Core.Services {
    using System.Collections.Generic;

    using CodeBase.Off.Core.Domain;

    public interface ICarouselService {
        bool Exists(int id);
        Carousel Get(int id);
        List<Carousel> GetList();
        int Save(Carousel carousel);
        void Delete(int id);
    }
}