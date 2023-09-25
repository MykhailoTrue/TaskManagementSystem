using TaskManagementSystem.Ado.Dall.Entities;

namespace TaskManagementSystem.Ado.Dall.Repositories.Interfaces
{
    public interface IWorkspaceRepository : IGenericRepository<Workspace>
    {
        Task<IEnumerable<Workspace>> GetWorkspacesByUserAsync(int UserId);
        Task<IEnumerable<Workspace>> GetWorkspacesCreatedAfterAsync(DateTime date);
    }
}
