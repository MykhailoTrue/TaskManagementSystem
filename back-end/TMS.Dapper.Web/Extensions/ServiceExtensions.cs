using Microsoft.Data.SqlClient;
using System.Data;
using TMS.Dapper.DAL.Repositories.Interfaces;
using TMS.Dapper.DAL.Repositories;
using TMS.Dapper.BLL.MappingProfiles;
using System.Reflection;
using TMS.Dapper.BLL.Services;
using TMS.Dapper.BLL.Services.Abstract;
using TMS.Dapper.Web.Middleware;

namespace TMS.Dapper.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterUnitOfWork(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddScoped(sp =>
                new SqlConnection(configuration.GetConnectionString("TaskManagementSystem")));

            services.AddScoped(sp =>
            {
                var connection = sp.GetRequiredService<SqlConnection>();
                connection.Open();
                return connection.BeginTransaction();
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
            services.AddScoped<IProjectCategoryRepository, ProjectCategoryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cf =>
            {
                cf.AddProfile<UserProfile>();
                cf.AddProfile<WorkspaceProfile>();
                cf.AddProfile<ProjectCategoryProfile>();
                cf.AddProfile<ProjectProfile>();
            }, 
            Assembly.GetExecutingAssembly());
        }

        public static void RegisterCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IWorkspaceService, WorkspaceService>();
        }

        public static void RegisterMiddlewareFactory(this IServiceCollection services)
        {
            services.AddTransient<ExceptionMiddleware>();
        }
    }
}
