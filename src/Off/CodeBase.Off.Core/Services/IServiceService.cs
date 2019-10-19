namespace CodeBase.Off.Core.Services {
    using System.Collections.Generic;

    using CodeBase.Off.Core.Domain;

    public interface IServiceService {
        bool Exists(string slug);
        Service Get(string slug);
        List<Service> GetList();
        void Save(Service service);
        void Delete(string slug);
    }
}