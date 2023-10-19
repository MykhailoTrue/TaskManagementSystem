using TMS.Dapper.Web.Middleware;

namespace TMS.Dapper.Web.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void UseExceptionHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
