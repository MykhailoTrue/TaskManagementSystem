using Dapper;
using Microsoft.Data.SqlClient;
using TMS.Dapper.DAL.Entities;
using TMS.Dapper.DAL.Repositories.Interfaces;

namespace TMS.Dapper.DAL.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(SqlConnection connection, SqlTransaction transaction) 
            : base(connection, transaction, "Projects")
        {
        }

        public async Task<IEnumerable<Project>> GetAllWithCategoryAsync()
        {
            var query = "SELECT * FROM dbo.Projects p " +
                "LEFT JOIN dbo.ProjectCategories pc ON p.ProjectCategoryId = pc.Id;";

            var projects = await _connection.QueryAsync<Project, ProjectCategory, Project>(
                query,
                map: (p, pc) =>
                {
                    p.ProjectCategory = pc;
                    return p;
                },
                transaction: _transaction);
            return projects;
        }

        public async Task<Project?> GetByIdWithCategoryAsync(int id)
        {
            var query = "SELECT * FROM dbo.Projects p " +
                "LEFT JOIN dbo.ProjectCategories pc ON p.ProjectCategoryId = pc.Id " +
                "WHERE p.Id = @Id;";

            var project = await _connection.QueryAsync<Project, ProjectCategory, Project>(
                query,
                map: (p, pc) =>
                {
                    p.ProjectCategory = pc;
                    return p;
                },
                param: new {@Id = id},
                transaction: _transaction);

            return project.SingleOrDefault();
        }

        public async Task<Project?> GetProjectWithMembersAsync(int id)
        {
            var query = @"SELECT 
                        p.Id, 
                        p.Name, 
                        p.ProjectCategoryId,

                        pc.Id, 
                        pc.Name, 
                        pc.Description,

                        u.Id, 
                        u.FirstName, 
                        u.LastName, 
                        u.Email, 
                        u.BirthDate 
                    FROM 
                        dbo.[Projects] p
                    LEFT JOIN 
                        ProjectCategories pc ON p.ProjectCategoryId = pc.Id
                    LEFT JOIN 
                        ProjectMembers pm ON p.Id = pm.ProjectId
                    LEFT JOIN 
                        Users u ON pm.MemberId = u.Id
                    WHERE 
                        p.Id = @Id;";

            Project? project = null;
            var projects = await _connection.QueryAsync<Project, ProjectCategory, User, Project>(
                query, (p, pc, u) =>
                {
                    if (project is null)
                    {
                        project = p;
                        project.ProjectCategory = pc;
                    }

                    if (u is not null)
                    {
                        project.Members.Add(u);
                    }

                    return project;
                },
                param: new { @Id = id },
                transaction: _transaction);

            return project;
        }
    }
}
