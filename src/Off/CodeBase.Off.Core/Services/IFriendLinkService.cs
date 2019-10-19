namespace CodeBase.Off.Core.Services {
    using System.Collections.Generic;

    using CodeBase.Off.Core.Domain;

    public interface IFriendLinkService {
        bool Exists(string slug);
        FriendLink Get(string slug);
        List<FriendLink> GetList();
        void Save(FriendLink link);
        void Delete(string slug);
    }
}