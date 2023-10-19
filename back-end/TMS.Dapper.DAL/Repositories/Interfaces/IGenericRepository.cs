using TMS.Dapper.DAL.Entities.Abstract;

namespace TMS.Dapper.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<int> CreateAsync(T entity);
        Task<int> CreateRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task<int> DeleteAsync(int id);
    }
}
