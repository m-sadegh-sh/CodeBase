namespace CodeBase.Off.Core.Services {
    using System.Collections.Generic;

    using CodeBase.Off.Core.Domain;

    public interface ISocialNetworkService {
        bool Exists(string slug);
        SocialNetwork Get(string slug);
        List<SocialNetwork> GetList();
        void Save(SocialNetwork network);
        void Delete(string slug);
    }
}