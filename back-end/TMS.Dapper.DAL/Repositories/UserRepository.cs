using Dapper;
using System.Data;
using TMS.Dapper.DAL.Context;
using TMS.Dapper.DAL.Entities;
using TMS.Dapper.DAL.Repositories.Interfaces;

namespace TMS.Dapper.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DapperContext context)
            : base(context, "Users")
        {
            
        }

        public async Task<IEnumerable<Workspace>> GetUsersWithWorkspaces()
        {
            var query = @"SELECT u.Id, u.FirstName, u.LastName, u.Email, u.BirthDate, w.Id, w.Title, w.Description, w.CreatedAt, w.UpdatedAt, w.AuthorId
                        FROM Users u
                        INNER JOIN Workspaces w ON u.Id = w.AuthorId;";

            using (var connection = _context.CreateConnection())
            {
                var users = await connection.QueryAsync<Workspace, User, Workspace>(query,
                    (w, u) =>
                    {
                        w.Author = u;
                        return w;
                    },
                    splitOn: "AuthorId");
                return users;
            }
        }

        public async Task<User> GetUserWithWorkspacesMultipleQueryAsync(int userId)
        {
            var query = "SELECT * FROM dbo.[Users] WHERE Id=@Id; " +
                "SELECT * FROM dbo.[Workspaces] WHERE AuthorId=@Id; ";

            using var connection = _context.CreateConnection();
            using (var multi = connection.QueryMultiple(query, new {@Id = userId}))
            {
                var user = await multi.ReadSingleOrDefaultAsync<User>(); 
                if (user is not null)
                {
                    user.Workspaces = await multi.ReadAsync<Workspace>();
                }

                return user;
            }
        }

        public async Task<IEnumerable<User>> GetUserWithWorkspacesMultipleMappingAsync()
        {
            var query = "SELECT * from dbo.[Users] as u " +
                "INNER JOIN dbo.[Workspaces] as w ON u.Id = w.AuthorId";

            using (var connection = _context.CreateConnection())
            {
                var userDict = new Dictionary<int, User>();
                var users = await connection.QueryAsync<User, Workspace, User>(
                    query, (u, w) =>
                    {
                        return u;
                    });

                return users;

                var userss = await connection.QueryAsync(,)
            }
        }
    }
}
