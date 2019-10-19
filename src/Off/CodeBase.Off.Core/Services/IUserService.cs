namespace CodeBase.Off.Core.Services {
    using System.Collections.Generic;

    using CodeBase.Off.Core.Domain;

    public interface IUserService {
        User Current { get; }
        bool Exists(int id);
        bool Exists(string userName);
        User Get(int id);
        User Get(string userName);
        List<User> GetList();
        int Save(User user);
        void Delete(int id);
        void Delete(string userName);
    }
}