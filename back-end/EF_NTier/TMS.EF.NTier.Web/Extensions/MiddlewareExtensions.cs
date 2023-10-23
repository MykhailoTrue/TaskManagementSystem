using TMS.EF.NTier.Web.Middleware;

namespace TMS.EF.NTier.Web.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void UseExceptionHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
