using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using TMS.Dapper.Common.DTOs.Users.CRUD;

namespace TMS.Dapper.Web.Formatters
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
        {
            return typeof(UserReadDto).IsAssignableFrom(type)
                || typeof(IEnumerable<UserReadDto>).IsAssignableFrom(type); 
        }
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<UserReadDto>)
            {
                foreach (var user in (IEnumerable<UserReadDto>)context.Object)
                {
                    FormatCsv(buffer, user);
                }
            }
            else
            {
                FormatCsv(buffer, (UserReadDto)context.Object); 
            }

            await response.WriteAsync(buffer.ToString(), selectedEncoding);
        }

        private static void FormatCsv(StringBuilder buffer, UserReadDto user)
        {
            buffer.AppendLine($"\"{user.Id}\",\"{user.FirstName}\",\"{user.LastName}\",\"{user.Email}\",\"{user.BirthDate}\"");
        }
    }
}
