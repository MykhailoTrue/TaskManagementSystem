using TMS.Dapper.DAL.Entities;

namespace TMS.Dapper.DAL.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<IEnumerable<Workspace>> GetUsersWithWorkspaces();
        Task<User> GetUserWithWorkspacesMultipleQueryAsync(int userId);
        Task<IEnumerable<User>> GetUserWithWorkspacesMultipleMappingAsync();

    }
}
