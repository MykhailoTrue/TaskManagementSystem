using TMS.Dapper.Common.DTOs.Projects.CRUD;
using TMS.Dapper.Common.DTOs.Projects.Custom;

namespace TMS.Dapper.BLL.Services.Abstract
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectReadDTO>> GetAllProjectsAsync();

        Task<ProjectReadDTO> GetProjectByIdAsync(int id);
        Task<ProjectReadDTO> CreateProjectAsync(ProjectCreateDTO project);
        Task<ProjectReadDTO> UpdateProjectAsync(int id, ProjectUpdateDTO project);
        Task DeleteProjectAsync(int id);

        Task<ProjectWithMembersDTO> GetProjectWithMembersAsync(int id);
    }
}
