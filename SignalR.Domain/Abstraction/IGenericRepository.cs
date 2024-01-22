using SignalR.Domain.Specifications;
using System.Linq.Expressions;

namespace SignalR.Infrastructure.Reposatory
{
    public interface IGenericRepository<T> where T : Entity
    {
        Task Add(T entity);
        Task AddRange(List<T> entities);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entity);
        T? GetById(string id);
        T? GetById(long id);
        T? GetEntityWithSpec(BaseSpecification<T> specifications);
        (IQueryable<T> data, int count) GetWithSpec(BaseSpecification<T> specifications);
        Task<bool> IsExist(Expression<Func<T, bool>> filter);
        Task<bool> Save();
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
    }
}