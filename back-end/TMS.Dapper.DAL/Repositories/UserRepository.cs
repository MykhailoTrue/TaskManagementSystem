using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using TaskManagementSystem.Ado.Dall.Entities;
using TaskManagementSystem.Ado.Dall.Repositories.Interfaces;

namespace TaskManagementSystem.Ado.Dall.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction, string tableName) 
            : base(sqlConnection, dbTransaction, "Users")
        {
        }

        public async Task<IEnumerable<User>> GetTopTenUsersAsync()
        {
            string sql = @"SELECT TOP 10 * FROM Users";
            var results = await _sqlConnection.QueryAsync<User>(sql,
                transaction: _dbTransaction);
            return results;
        }
    }
}
