using Microsoft.Data.SqlClient;
using TMS.Dapper.DAL.Entities;
using TMS.Dapper.DAL.Repositories.Interfaces;

namespace TMS.Dapper.DAL.Repositories
{
    public class ProjectCategoryRepository : GenericRepository<ProjectCategory>, IProjectCategoryRepository
    {
        public ProjectCategoryRepository(SqlConnection connection, SqlTransaction transaction)
            : base(connection, transaction, "ProjectCategories")
        {
        }
    }
}
