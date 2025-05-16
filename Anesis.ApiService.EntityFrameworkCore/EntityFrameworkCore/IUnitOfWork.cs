namespace Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore
{
    public interface IUnitOfWork : IRepositoryFactory
    {
        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
