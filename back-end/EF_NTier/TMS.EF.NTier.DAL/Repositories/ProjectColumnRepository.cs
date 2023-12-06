using Microsoft.EntityFrameworkCore;
using TMS.EF.NTier.DAL.Context;
using TMS.EF.NTier.DAL.Entities;
using TMS.EF.NTier.DAL.Repositories.Interfaces;

namespace TMS.EF.NTier.DAL.Repositories
{
    public class ProjectColumnRepository : GenericRepository<ProjectColumn>, IProjectColumnRepository
    {
        public ProjectColumnRepository(TaskManagementSystemDbContext context) 
            : base(context)
        {
            
        }

        public async Task<IEnumerable<ProjectColumn>> GetAllProjectColumnsAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<ProjectColumn?> GetProjectColumnByIdAsync(int id)
        {
            return await FindByCondition(pc => pc.Id.Equals(id))
                .FirstOrDefaultAsync();
        }

        public async Task<ProjectColumn?> GetProjectColumnWithDetailsAsync(int id)
        {
            return await FindByCondition(pc => pc.Id.Equals(id))
                .Include(pc => pc.Issues) 
                .FirstOrDefaultAsync();
        }

        public void CreateProjectColumn(ProjectColumn projectColumn)
        {
            Create(projectColumn);
        }

        public void UpdateProjectColumn(ProjectColumn projectColumn)
        {
            Update(projectColumn);
        }

        public void DeleteProjectColumn(ProjectColumn projectColumn)
        {
            Delete(projectColumn);
        }
    }
}
