﻿using Dapper;
using TMS.Dapper.DAL.Context;
using TMS.Dapper.DAL.Entities;
using TMS.Dapper.DAL.Repositories.Interfaces;

namespace TMS.Dapper.DAL.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(DapperContext context) : base(context, "Projects")
        {
        }

        public async Task<Project?> GetProjectWithMembersAsync(int id)
        {
            var query = @"SELECT 
                        p.Id, 
                        p.Name, 
                        p.ProjectCategoryId,

                        pc.Id, 
                        pc.Name AS ProjectCategoryName, 
                        pc.Description AS ProjectCategoryDescription,

                        u.Id, 
                        u.FirstName, 
                        u.LastName, 
                        u.Email, 
                        u.BirthDate 
                    FROM 
                        dbo.[Projects] p
                    LEFT JOIN 
                        ProjectCategories pc ON p.ProjectCategoryId = pc.Id
                    INNER JOIN 
                        ProjectMembers pm ON p.Id = pm.ProjectId
                    INNER JOIN 
                        Users u ON pm.MemberId = u.Id
                    WHERE 
                        p.Id = 1;";

            using (var connection = _context.CreateConnection())
            {
                Project? project = null;
                var projects = await connection.QueryAsync<Project, ProjectCategory, User, Project>(
                    query, (p, pc, u) =>
                    {
                        if (project is null)
                        {
                            project = p;
                            project.ProjectCategory = pc;
                        }
                        project.Members.Add(u);

                        return project;
                    },
                    param: new {@Id = id});

                return project;
            }
        }
    }
}
