using Microsoft.Data.SqlClient;
using System.Data;

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

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped((provider) =>
            {
                async Task<SqlConnection> GetDbConnectionAsync()
                {
                    var connection = new SqlConnection(_configuration.GetConnectionString("TaskManagementSystem"));
                    await connection.OpenAsync();
                    return connection;
                }

                return GetDbConnectionAsync().Result;
            });

            services.AddScoped<IDbTransaction>(s =>
            {
                SqlConnection conn = s.GetRequiredService<SqlConnection>();
                return conn.BeginTransaction();
            }); 

            services.AddScoped<SqlCommand>(s => new SqlCommand("", s.GetRequiredService<SqlConnection>()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
