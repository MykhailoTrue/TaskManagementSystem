using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using TMS.Dapper.DAL.Repositories.Interfaces;
using TMS.Dapper.DAL.Repositories;

namespace TMS.Dapper.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterUnitOfWork(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddScoped<IDbConnection>(sp =>
                new SqlConnection(configuration.GetConnectionString("TaskManagementSystem")));

            services.AddScoped(sp =>
            {
                var connection = sp.GetRequiredService<IDbConnection>();
                connection.Open();
                return connection.BeginTransaction();
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
            services.AddScoped<IProjectCategoryRepository, ProjectCategoryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
