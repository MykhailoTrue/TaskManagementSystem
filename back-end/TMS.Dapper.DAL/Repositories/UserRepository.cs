using Dapper;
using Microsoft.Data.SqlClient;
using TMS.Dapper.DAL.Entities;
using TMS.Dapper.DAL.Repositories.Interfaces;

namespace TMS.Dapper.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(SqlConnection connection, SqlTransaction transaction)
            : base(connection, transaction, "Users")
        {
            
        }

        public async Task<User?> GetUserWithWorkspacesMultipleQueryAsync(int userId)
        {
            var query = "SELECT * FROM dbo.[Users] WHERE Id=@Id; " +
                "SELECT * FROM dbo.[Workspaces] WHERE AuthorId=@Id; ";

            using (var multi = _connection.QueryMultiple(
                query,
                param: new {@Id = userId}, 
                transaction: _transaction))
            {
                var user = await multi.ReadSingleOrDefaultAsync<User>(); 
                if (user is not null)
                {
                    user.Workspaces = (await multi.ReadAsync<Workspace>()).ToList();
                }

                return user;
            }
        }

        public async Task<IEnumerable<User>> GetUsersWithProjectsAsync()
        {
            var query = @"SELECT * FROM dbo.[Users] u 
                            LEFT JOIN dbo.[Workspaces] w ON u.Id = w.AuthorId 
                            LEFT JOIN dbo.[Projects] p ON p.WorkspaceId = w.Id 
                            LEFT JOIN dbo.[ProjectCategories] pc ON p.ProjectCategoryId = pc.Id ";

            Dictionary<int, User> userDict = new();
            Dictionary<int, Workspace> workspaceDict = new();
            var users = await _connection.QueryAsync<User, Workspace, Project, ProjectCategory, User>(
                query, (u, w, p, c) =>
                {
                    if (!userDict.TryGetValue(u.Id, out var currentUser))
                    {
                        currentUser = u;
                        userDict[u.Id] = currentUser;
                    }

                    if (w is null || p is null)
                    {
                        return currentUser;
                    }

                    if (!workspaceDict.TryGetValue(w.Id, out var currentWorkspace))
                    {
                        currentWorkspace = w;
                        currentWorkspace.Author = currentUser;

                        workspaceDict[w.Id] = currentWorkspace;

                        currentUser.Workspaces.Add(currentWorkspace);
                    }

                    p.ProjectCategory = c;
                    p.Workspace = currentWorkspace;

                    currentWorkspace.Projects.Add(p);

                    return currentUser;
                },
                splitOn: "Id",
                transaction: _transaction);

            return users.Distinct().ToList();
        }

        public async Task<IEnumerable<User>> GetUsersByEmail(string email)
        {
            var query = "SELECT * FROM dbo.[Users] WHERE Email=@Email; ";

            var users = await _connection.QueryAsync<User>(
                query,
                param: new
                {
                    Email = email,
                },
                transaction: _transaction);

            return users;
        }
    }
}
