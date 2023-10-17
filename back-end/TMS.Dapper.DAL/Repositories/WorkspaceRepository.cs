using Microsoft.Data.SqlClient;
using TMS.Dapper.DAL.Entities;
using TMS.Dapper.DAL.Repositories.Interfaces;

namespace TMS.Dapper.DAL.Repositories
{
    public class WorkspaceRepository : GenericRepository<Workspace>, IWorkspaceRepository
    {
        public WorkspaceRepository(SqlConnection connection, SqlTransaction transaction)
            : base(connection, transaction, "Workspaces")
        {
        }
    }
}
