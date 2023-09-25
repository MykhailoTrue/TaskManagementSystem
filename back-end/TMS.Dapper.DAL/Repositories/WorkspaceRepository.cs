using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using TaskManagementSystem.Ado.Dall.Entities;
using TaskManagementSystem.Ado.Dall.Repositories.Interfaces;

namespace TaskManagementSystem.Ado.Dall.Repositories
{
    internal class WorkspaceRepository : GenericRepository<Workspace>, IWorkspaceRepository
    {
        public WorkspaceRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) 
            : base(sqlConnection, dbTransaction, "Workspaces")
        {
        }

        public async Task<IEnumerable<Workspace>> GetWorkspacesByUserAsync(int UserId)
        {
            string sql = @"SELECT * FROM Workspaces WHERE UserId = @UserId";

            var results = await _sqlConnection.QueryAsync<Workspace>(sql,
                param: new { UserId = UserId },
                transaction: _dbTransaction);
            return results;
        }

        public Task<IEnumerable<Workspace>> GetWorkspacesCreatedAfterAsync(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
