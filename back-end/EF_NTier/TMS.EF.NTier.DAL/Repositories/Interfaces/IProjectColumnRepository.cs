using TMS.EF.NTier.DAL.Entities;

namespace TMS.EF.NTier.DAL.Repositories.Interfaces
{
    public interface IProjectColumnRepository : IGenericRepository<ProjectColumn>
    {
        Task<IEnumerable<ProjectColumn>> GetAllProjectColumnsAsync();
        Task<ProjectColumn?> GetProjectColumnByIdAsync(int id);
        Task<ProjectColumn?> GetProjectColumnWithDetailsAsync(int id);
        void CreateProjectColumn(ProjectColumn projectColumn);
        void UpdateProjectColumn(ProjectColumn projectColumn);
        void DeleteProjectColumn(ProjectColumn projectColumn);
    }
}
