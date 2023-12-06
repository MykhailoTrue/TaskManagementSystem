using TMS.EF.NTier.DAL.Context;
using TMS.EF.NTier.Web.Extensions;
using TMS.EF.NTier.Web.Middleware;

namespace TMS.EF.NTier.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                //.AddXmlSerializerFormatters()
                .AddNewtonsoftJson();

            // Add services to the container.
            services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.RegisterUnitOfWork(Configuration);
            services.RegisterCustomServices();
            services.RegisterAutomapper();

            services.AddTransient<ExceptionMiddleware>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandlingMiddleware();

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();
            
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoint => endpoint.MapControllers());

            InitializeDb(app);
        }

        private static void InitializeDb(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using var context = scope.ServiceProvider.GetRequiredService<TaskManagementSystemDbContext>();
                //context.Database.Migrate();
            }
        }
    }
}
