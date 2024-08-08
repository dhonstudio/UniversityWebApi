using Domain.Common;
using System.Linq.Expressions;

namespace Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        public Task<List<T>> GetAll();
        public IQueryable<T> GetAllSieveModel();
        public Task<T?> GetById(object id);
        public Task<List<T>> Get(Expression<Func<T, bool>> predicate);
        public Task Add(T entity);
        public Task Update(T entity);
        public Task Remove(T entity);
    }
}
