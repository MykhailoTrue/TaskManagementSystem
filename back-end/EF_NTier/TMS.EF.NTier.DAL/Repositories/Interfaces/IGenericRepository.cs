using System.Linq.Expressions;

namespace TMS.EF.NTier.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T?> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
