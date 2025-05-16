namespace Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore
{
    public interface IRepositoryFactory
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}
