namespace CodeBase.Common.Infrastructure.Storage {
    using System.Linq;

    public interface IRepository {
        bool Exists<TEntity>(params object[] keys);
        TEntity Single<TEntity>(params object[] keys);
        IQueryable<TEntity> All<TEntity>();
        void Save<TEntity>(TEntity item);
        void Delete<TEntity>(params object[] keys);
        void DeleteAll<TEntity>();
    }
}