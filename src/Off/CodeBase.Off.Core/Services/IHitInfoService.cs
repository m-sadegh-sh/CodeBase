namespace CodeBase.Off.Core.Services {
    using System.Collections.Generic;

    using CodeBase.Off.Core.Domain;

    public interface IHitInfoService {
        bool Exists(int id);

        bool IsResolvable(int hitId,
                          string requestIp);

        HitInfo Get(int id);
        IList<HitInfo> GetList();
        void Save(HitInfo info);
        void Delete(int id);
    }
}