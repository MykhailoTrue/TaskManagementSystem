using TMS.EF.NTier.Common.DTO.ProjectColumns;

namespace TMS.EF.NTier.BLL.Services.Interfaces
{
    public interface IProjectColumnService
    {
        Task<IEnumerable<ProjectColumnReadDTO>> GetAllProjectColumnsAsync();
        Task<ProjectColumnReadDTO> GetProjectColumnByIdAsync(int id);
        Task<ProjectColumnWithIssuesDTO> GetProjectColumnWithDetailsAsync(int id);
        Task<ProjectColumnReadDTO> CreateProjectColumnAsync(ProjectColumnCreateDTO projectColumn);
        Task<ProjectColumnReadDTO> UpdateProjectColumnAsync(int id, ProjectColumnUpdateDTO projectColumn);
        Task DeleteProjectColumnAsync(int id);
    }
}
