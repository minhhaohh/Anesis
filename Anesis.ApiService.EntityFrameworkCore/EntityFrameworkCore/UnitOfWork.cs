using Microsoft.EntityFrameworkCore;

namespace Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore
{
    public class UnitOfWork : IUnitOfWork, IRepositoryFactory
    {
        protected readonly DbContext _context;

        protected Dictionary<Type, object> _cachedRepositories;

        public UnitOfWork(DbContext dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException("dbContext");
            _cachedRepositories = new Dictionary<Type, object>();
        }

        public virtual int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            Type typeFromHandle = typeof(TEntity);
            if (!_cachedRepositories.ContainsKey(typeFromHandle))
            {
                _cachedRepositories[typeFromHandle] = new Repository<TEntity>(this, _context);
            }

            return (IRepository<TEntity>)_cachedRepositories[typeFromHandle];
        }
    }
}
