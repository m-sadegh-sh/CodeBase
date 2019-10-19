namespace CodeBase.Off.Core.Services.Impl {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CodeBase.Common.Infrastructure.Application;
    using CodeBase.Common.Infrastructure.Storage;
    using CodeBase.Off.Core.Domain;

    public sealed class CarouselService : ICarouselService {
        private readonly IRepository _repository;

        public CarouselService(IRepository repository) {
            _repository = repository;
        }

        public bool Exists(int id) {
            if (id < 1)
                return false;

            return _repository.Exists<Carousel>(id);
        }

        public Carousel Get(int id) {
            if (id < 1)
                return null;

            if (!Exists(id))
                return null;

            return _repository.Single<Carousel>(id);
        }

        public List<Carousel> GetList() {
            return _repository.All<Carousel>().ToList();
        }

        public int Save(Carousel carousel) {
            if (carousel.Id == default(int))
                carousel.Id = DateTime.UtcNow.Timestamp();

            _repository.Save(carousel);

            return carousel.Id;
        }

        public void Delete(int id) {
            if (id < 1)
                return;

            if (!Exists(id))
                return;

            _repository.Delete<Carousel>(id);
        }
    }
}