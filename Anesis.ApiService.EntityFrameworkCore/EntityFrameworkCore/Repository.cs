using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using System.Transactions;

namespace Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;

        protected readonly DbSet<TEntity> _entities;

        protected readonly IUnitOfWork _unitOfWork;

        public Repository(IUnitOfWork unitOfWork, DbContext context)
        {
            _context = context ?? throw new ArgumentNullException("context");
            _entities = _context.Set<TEntity>();
            _unitOfWork = unitOfWork;
        }

        public IQueryable<TEntity> All(bool disableTracking = false)
        {
            if (!disableTracking)
            {
                return _entities;
            }

            return EntityFrameworkQueryableExtensions.AsNoTracking(_entities);
        }

        protected virtual void ChangeEntityState(TEntity entity, EntityState state)
        {
            _context.Entry(entity).State = state;
        }

        protected virtual void SetEntityStateToModifiedIfApplicable(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry = _context.Entry(entity);
            if (entityEntry.State < EntityState.Added || !_context.ChangeTracker.AutoDetectChangesEnabled)
            {
                entityEntry.State = EntityState.Modified;
            }
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return All(disableTracking: true).Any(predicate);
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await EntityFrameworkQueryableExtensions.AnyAsync(All(disableTracking: true), predicate, cancellationToken);
        }

        public virtual TEntity Find(params object[] keyValues)
        {
            return _entities.Find(keyValues);
        }

        public virtual ValueTask<TEntity> FindAsync(params object[] keyValues)
        {
            return _entities.FindAsync(keyValues);
        }

        public virtual ValueTask<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return _entities.FindAsync(keyValues, cancellationToken);
        }

        public virtual TEntity GetFirst(Expression<Func<TEntity, bool>> predicate, bool disableTracking = true)
        {
            return All(disableTracking).FirstOrDefault(predicate);
        }

        public virtual async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate, bool disableTracking = true, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(All(disableTracking), predicate, cancellationToken);
        }

        public virtual void Insert(TEntity entity)
        {
            _entities.Add(entity);
        }

        public virtual async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _entities.AddAsync(entity, cancellationToken);
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
        }

        public virtual Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _entities.AddRangeAsync(entities, cancellationToken);
        }

        public virtual void Update(TEntity entity)
        {
            _entities.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            _entities.UpdateRange(entities);
        }

        public virtual TEntity Delete(params object[] keyValues)
        {
            TEntity val = Find(keyValues);
            if (val != null)
            {
                Delete(val);
            }

            return val;
        }

        public virtual void Delete(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public virtual async Task<TEntity> DeleteAsync(params object[] keyValues)
        {
            return await DeleteAsync(default(CancellationToken), keyValues);
        }

        public virtual async Task<TEntity> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            TEntity val = await FindAsync(cancellationToken, keyValues);
            if (val != null)
            {
                Delete(val);
            }

            return val;
        }

        public virtual void DeleteRange(params TEntity[] entities)
        {
            _entities.RemoveRange(entities);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }

        public virtual int DeleteAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            int num = 0;
            using TransactionScope transactionScope = new TransactionScope();
            IQueryable<TEntity> source = _entities.AsQueryable();
            if (predicate != null)
            {
                source = source.Where(predicate);
            }

            foreach (IEnumerable<TEntity> item in source.ToList())
            {
                _entities.RemoveRange(item.ToList());
                num += _unitOfWork.SaveChanges();
            }

            transactionScope.Complete();
            return num;
        }

        public virtual async Task<int> DeleteAllAsync(Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            int deletedCount = 0;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                IQueryable<TEntity> source = _entities.AsQueryable();
                if (predicate != null)
                {
                    source = source.Where(predicate);
                }

                foreach (IEnumerable<TEntity> item in (await EntityFrameworkQueryableExtensions.ToListAsync(source, cancellationToken)))
                {
                    _entities.RemoveRange(item.ToList());
                    int num = deletedCount;
                    deletedCount = num + await _unitOfWork.SaveChangesAsync(cancellationToken);
                }

                transaction.Complete();
            }

            return deletedCount;
        }

        public virtual IQueryable<TEntity> FromSql(string sqlQuery, params object[] parameters)
        {
            return _entities.FromSqlRaw(sqlQuery, parameters);
        }

        public virtual async Task<IEnumerable<TEntity>> FromSqlAsync(string sqlQuery, params object[] parameters)
        {
            return await EntityFrameworkQueryableExtensions.ToListAsync(_entities.FromSqlRaw(sqlQuery, parameters));
        }

        public virtual async Task<IEnumerable<TEntity>> FromSqlAsync(string sqlQuery, CancellationToken cancellationToken, params object[] parameters)
        {
            return await EntityFrameworkQueryableExtensions.ToListAsync(_entities.FromSqlRaw(sqlQuery, parameters), cancellationToken);
        }

        public int ExecSql(string query, params object[] parameters)
        {
            return _context.Database.ExecuteSqlRaw(query, parameters);
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return All(disableTracking: true).Count(predicate);
            }

            return All(disableTracking: true).Count();
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return (predicate != null) ? (await EntityFrameworkQueryableExtensions.CountAsync(All(disableTracking: true), predicate)) : (await EntityFrameworkQueryableExtensions.CountAsync(All(disableTracking: true)));
        }

        public virtual T Max<T>(Expression<Func<TEntity, T>> selector, Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return All(disableTracking: true).Where(predicate).Max(selector);
            }

            return All(disableTracking: true).Max(selector);
        }

        public virtual async Task<T> MaxAsync<T>(Expression<Func<TEntity, T>> selector, Expression<Func<TEntity, bool>> predicate = null)
        {
            return (predicate != null) ? (await EntityFrameworkQueryableExtensions.MaxAsync(All(disableTracking: true).Where(predicate), selector)) : (await EntityFrameworkQueryableExtensions.MaxAsync(All(disableTracking: true), selector));
        }

        public virtual T Min<T>(Expression<Func<TEntity, T>> selector, Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return All(disableTracking: true).Where(predicate).Min(selector);
            }

            return All(disableTracking: true).Min(selector);
        }

        public virtual async Task<T> MinAsync<T>(Expression<Func<TEntity, T>> selector, Expression<Func<TEntity, bool>> predicate = null)
        {
            return (predicate != null) ? (await EntityFrameworkQueryableExtensions.MinAsync(All(disableTracking: true).Where(predicate), selector)) : (await EntityFrameworkQueryableExtensions.MinAsync(All(disableTracking: true), selector));
        }

        public virtual decimal Average(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return All(disableTracking: true).Where(predicate).Average(selector);
            }

            return All(disableTracking: true).Average(selector);
        }

        public virtual async Task<decimal> AverageAsync(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> predicate = null)
        {
            return (predicate != null) ? (await EntityFrameworkQueryableExtensions.AverageAsync(All(disableTracking: true).Where(predicate), selector)) : (await EntityFrameworkQueryableExtensions.AverageAsync(All(disableTracking: true), selector));
        }

        public virtual decimal Sum(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return All(disableTracking: true).Where(predicate).Sum(selector);
            }

            return All(disableTracking: true).Sum(selector);
        }

        public virtual async Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> predicate = null)
        {
            return (predicate != null) ? (await EntityFrameworkQueryableExtensions.SumAsync(All(disableTracking: true).Where(predicate), selector)) : (await EntityFrameworkQueryableExtensions.SumAsync(All(disableTracking: true), selector));
        }
    }
}
