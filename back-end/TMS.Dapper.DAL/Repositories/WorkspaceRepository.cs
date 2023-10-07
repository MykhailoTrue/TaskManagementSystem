using System.Data;
using TMS.Dapper.DAL.Entities;
using TMS.Dapper.DAL.Repositories.Interfaces;

namespace TMS.Dapper.DAL.Repositories
{
    public class WorkspaceRepository : GenericRepository<Workspace>, IWorkspaceRepository
    {
        public WorkspaceRepository(IDbConnection connection, IDbTransaction transaction)
            : base(connection, transaction, "Workspaces")
        {
        }
    }
}
