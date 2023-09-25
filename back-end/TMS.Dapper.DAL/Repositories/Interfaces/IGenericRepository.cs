using TaskManagementSystem.Ado.Dall.Entities.Abstract;

namespace TaskManagementSystem.Ado.Dall.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<int> InsertAsync(T t);
        Task<int> InsertRangeAsync(IEnumerable<T> list);
        Task UpdateAsync(T t);
        Task DeleteAsync(int id);

    }
}
