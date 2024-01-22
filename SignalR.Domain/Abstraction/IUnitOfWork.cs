namespace SignalR.Infrastructure.Reposatory
{
    public interface IUnitOfWork
    {
        Task<int> Complete();
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : Entity;
    }
}