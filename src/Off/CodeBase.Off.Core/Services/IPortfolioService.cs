namespace CodeBase.Off.Core.Services {
    using System.Collections.Generic;

    using CodeBase.Off.Core.Domain;

    public interface IPortfolioService {
        bool Exists(string slug);
        Portfolio Get(string slug);
        List<Portfolio> GetList();
        void Save(Portfolio portfolio);
        void Delete(string slug);
    }
}