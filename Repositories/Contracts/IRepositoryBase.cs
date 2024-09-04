using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        bool Any(Expression<Func<T, bool>> expression);
        Task<T> CreateAsync(T entity);
        void Update(T entity);
    }
}
