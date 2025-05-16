using System.Linq.Expressions;

namespace Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> All(bool disableTracking = false);

        bool Exists(Expression<Func<TEntity, bool>> predicate);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

        TEntity Find(params object[] keyValues);

        ValueTask<TEntity> FindAsync(params object[] keyValues);

        ValueTask<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);

        TEntity GetFirst(Expression<Func<TEntity, bool>> predicate, bool disableTracking = true);

        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate, bool disableTracking = true, CancellationToken cancellationToken = default(CancellationToken));

        void Insert(TEntity entity);

        void InsertRange(IEnumerable<TEntity> entities);

        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

        void Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entities);

        TEntity Delete(params object[] keyValues);

        void Delete(TEntity entity);

        Task<TEntity> DeleteAsync(params object[] keyValues);

        Task<TEntity> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);

        void DeleteRange(params TEntity[] entities);

        void DeleteRange(IEnumerable<TEntity> entities);

        int DeleteAll(Expression<Func<TEntity, bool>> predicate = null);

        Task<int> DeleteAllAsync(Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default(CancellationToken));

        IQueryable<TEntity> FromSql(string sqlQuery, params object[] parameters);

        Task<IEnumerable<TEntity>> FromSqlAsync(string sqlQuery, params object[] parameters);

        Task<IEnumerable<TEntity>> FromSqlAsync(string sqlQuery, CancellationToken cancellationToken, params object[] parameters);

        int ExecSql(string query, params object[] parameters);

        int Count(Expression<Func<TEntity, bool>> predicate = null);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);

        T Max<T>(Expression<Func<TEntity, T>> selector, Expression<Func<TEntity, bool>> predicate = null);

        Task<T> MaxAsync<T>(Expression<Func<TEntity, T>> selector, Expression<Func<TEntity, bool>> predicate = null);

        T Min<T>(Expression<Func<TEntity, T>> selector, Expression<Func<TEntity, bool>> predicate = null);

        Task<T> MinAsync<T>(Expression<Func<TEntity, T>> selector, Expression<Func<TEntity, bool>> predicate = null);

        decimal Average(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> predicate = null);

        Task<decimal> AverageAsync(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> predicate = null);

        decimal Sum(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> predicate = null);

        Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> predicate = null);
    }
}
