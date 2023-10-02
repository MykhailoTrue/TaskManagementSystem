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
                    user.Workspaces = (await multi.ReadAsync<Workspace>()).ToList();
                }

                return user;
            }
        }

        public async Task<IEnumerable<User>> GetUsersWithProjectsAsync()
        {
            var query = @"SELECT * FROM dbo.[Users] u 
                            INNER JOIN dbo.[Workspaces] w ON u.Id = w.AuthorId 
                            INNER JOIN dbo.[Projects] p ON p.WorkspaceId = w.Id 
                            LEFT JOIN dbo.[ProjectCategories] pc ON p.ProjectCategoryId = pc.Id ";

            using (var connection = _context.CreateConnection())
            {
                Dictionary<int, User> userDict = new();
                Dictionary<int, Workspace> workspaceDict = new();
                var users = await connection.QueryAsync<User, Workspace, Project, ProjectCategory, User>(
                    query, (u, w, p, c) =>
                    {
                        if (!userDict.TryGetValue(u.Id, out var currentUser))
                        {
                            currentUser = u;
                            userDict[u.Id] = currentUser;
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
                    splitOn: "Id");

                return users.Distinct().ToList();
            }
        }
    }
}
