using TMS.Dapper.Common.DTOs.Workspaces.CRUD;
using TMS.Dapper.Common.DTOs.Workspaces.Custom;
using TMS.Dapper.DAL.Entities;

namespace TMS.Dapper.BLL.Services.Abstract
{
    public interface IWorkspaceService
    {
        Task<IEnumerable<WorkspaceReadDTO>> GetAllWorkspacesAsync();

        Task<WorkspaceReadDTO> GetWorkspaceByIdAsync(int id);
        Task<WorkspaceReadDTO> CreateWorkspaceAsync(WorkspaceCreateDTO workspace);
        Task<WorkspaceReadDTO> UpdateWorkspaceAsync(int id, WorkspaceUpdateDTO workspace);
        Task DeleteWorkspaceAsync(int id);
        Task<WorkspaceWithProjectsDTO> GetWorkspaceWithProjectsAsync(int workspaceId);
    }
}
