using System.Data;
using TMS.Dapper.DAL.Entities;
using TMS.Dapper.DAL.Repositories.Interfaces;

namespace TMS.Dapper.DAL.Repositories
{
    public class ProjectCategoryRepository : GenericRepository<ProjectCategory>, IProjectCategoryRepository
    {
        public ProjectCategoryRepository(IDbConnection connection, IDbTransaction transaction)
            : base(connection, transaction, "ProjectCategories")
        {
        }
    }
}
