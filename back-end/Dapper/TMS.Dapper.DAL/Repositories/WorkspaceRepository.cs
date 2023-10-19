using Microsoft.Data.SqlClient;
using System.Data;
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

        public async Task<Workspace?> GetWorkspaceWithProjectsAsync(int workspaceId)
        {
            var query = @"SELECT 
	                        w.Id as w_id, 
	                        w.Title as w_title, 
	                        w.Description as w_description, 
	                        w.CreatedAt as w_createdAt,
	                        w.UpdatedAt as w_updatedAt,
	                        w.AuthorId as w_authorId,

	                        p.Id as p_id,
	                        p.Name as p_name,
	                        p.WorkspaceId as p_workspaceId,
	                        p.ProjectCategoryId as p_projectCategoryId,

							pc.Id as pc_id,
							pc.Name as pc_name,
							pc.Description as pc_description,
							pc.WorkspaceId as pc_workspaceId
                        FROM dbo.Workspaces as w
                        LEFT JOIN dbo.Projects as p 
							ON w.Id = p.WorkspaceId
						LEFT JOIN dbo.ProjectCategories as pc
							ON pc.Id = p.ProjectCategoryId
                        WHERE w.Id = @Id;";

            var command = new SqlCommand(query, _connection, _transaction);

            command.Parameters.Add(new SqlParameter("Id", workspaceId));
            Workspace? workspace = null;
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    workspace ??= new Workspace
                    {
                        Id = reader.GetInt32("w_id"),
                        Title = reader.GetString("w_title"),
                        Description = (await reader.IsDBNullAsync("w_description")) ? null : reader.GetString("w_description"),
                        CreatedAt = reader.GetDateTime("w_createdAt"),
                        UpdatedAt = reader.GetDateTime("w_updatedAt"),
                        AuthorId = reader.GetInt32("w_authorId")
                    };

                    if (await reader.IsDBNullAsync("p_id"))
                    {
                        continue;
                    }

                    var project = new Project
                    {
                        Id = reader.GetInt32("p_id"),
                        Name = reader.GetString("p_name"),
                        WorkspaceId = reader.GetInt32("p_workspaceId"),
                    };

                    if (await reader.IsDBNullAsync("p_projectCategoryId")) 
                    {
                        workspace.Projects.Add(project);
                        continue;
                    } 

                    var projectCategory = new ProjectCategory
                    {
                        Name = reader.GetString("pc_name"),
                    };
                    project.ProjectCategory = projectCategory;
                    workspace.Projects.Add(project);
                }
            }
            return workspace;
        }
    }
}
