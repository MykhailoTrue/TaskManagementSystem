using TMS.Dapper.Common.DTOs.ProjectCategories.CRUD;
using TMS.Dapper.Common.DTOs.Projects.CRUD;
using TMS.Dapper.Common.DTOs.Projects.Custom;

namespace TMS.Dapper.BLL.Services.Abstract
{
    public interface IProjectCategoryService
    {
        Task<IEnumerable<ProjectCategoryReadDTO>> GetAllProjectCategoriesAsync();

        Task<ProjectCategoryReadDTO> GetProjectCategoryByIdAsync(int id);
        Task<ProjectCategoryReadDTO> CreateProjectCategoryAsync(ProjectCategoryCreateDTO projectCategory);
        Task<ProjectCategoryReadDTO> UpdateProjectCategoryAsync(int id, ProjectCategoryUpdateDTO projectCategory);
        Task DeleteProjectCategoryAsync(int id);
    }
}
