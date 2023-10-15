using TMS.Dapper.BLL.MappingProfiles;
using TMS.Dapper.Web.Extensions;
using TMS.Dapper.Web.Formatters;
using TMS.Dapper.Web.Middleware;

namespace TaskManagementSystem.Ado.Web
{
    public class Startup
    {
        private readonly IConfigurationRoot _configuration;

        public Startup(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.

            services
                .AddControllers(opts =>
                {
                    // set up content negotation
                    opts.RespectBrowserAcceptHeader = true;
                })
                .AddNewtonsoftJson()
                .AddXmlSerializerFormatters()
                .AddMvcOptions(opts =>
                {
                    opts.OutputFormatters.Add(new CsvOutputFormatter());
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.RegisterUnitOfWork(_configuration);
            services.RegisterAutoMapper();
            services.RegisterCustomServices();
            services.RegisterMiddlewareFactory();
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

            app.UseEndpoints(app => app.MapControllers());

        }
    }
}
