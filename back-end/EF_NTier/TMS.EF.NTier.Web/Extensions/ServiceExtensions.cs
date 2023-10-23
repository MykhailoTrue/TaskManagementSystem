using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TMS.EF.NTier.BLL.MappingProfiles;
using TMS.EF.NTier.BLL.Services;
using TMS.EF.NTier.BLL.Services.Interfaces;
using TMS.EF.NTier.DAL.Context;
using TMS.EF.NTier.DAL.Repositories;
using TMS.EF.NTier.DAL.Repositories.Interfaces;

namespace TMS.EF.NTier.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterUnitOfWork(this IServiceCollection services, IConfiguration configuration)
        {
            // db context
            var contextAssembly = typeof(TaskManagementSystemDbContext).Assembly.GetName().Name;
            services.AddDbContext<TaskManagementSystemDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TaskManagementSystem"), opts => opts.MigrationsAssembly(contextAssembly)));
            
            // repositories
            services.AddScoped<IProjectColumnRepository, ProjectColumnRepository>();
            
            // unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void RegisterCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IProjectColumnService, ProjectColumnService>();
        }

        public static void RegisterAutomapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<IssueProfile>();
                cfg.AddProfile<ProjectColumnProfile>();
            }, 
            Assembly.GetExecutingAssembly());
        }
    }
}
