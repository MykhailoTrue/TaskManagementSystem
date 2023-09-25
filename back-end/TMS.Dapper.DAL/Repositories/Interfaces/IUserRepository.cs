using TaskManagementSystem.Ado.Dall.Entities;

namespace TaskManagementSystem.Ado.Dall.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<IEnumerable<User>> GetTopTenUsersAsync();
    }
}
