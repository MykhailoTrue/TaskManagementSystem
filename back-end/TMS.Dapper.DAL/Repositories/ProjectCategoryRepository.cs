using TMS.Dapper.DAL.Context;
using TMS.Dapper.DAL.Entities;
using TMS.Dapper.DAL.Repositories.Interfaces;

namespace TMS.Dapper.DAL.Repositories
{
    public class ProjectCategoryRepository : GenericRepository<ProjectCategory>, IProjectCategoryRepository
    {
        public ProjectCategoryRepository(DapperContext context) : base(context, "ProjectCategories")
        {
        }
    }
}
