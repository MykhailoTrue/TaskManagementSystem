using Microsoft.EntityFrameworkCore;
using TMS.EF.NTier.DAL.Context;

namespace TMS.EF.NTier.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var contextAssembly = typeof(TaskManagementSystemDbContext).Assembly.GetName().Name;
            services.AddDbContext<TaskManagementSystemDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TaskManagementSystem"), opts => opts.MigrationsAssembly(contextAssembly)));
        }
    }
}
