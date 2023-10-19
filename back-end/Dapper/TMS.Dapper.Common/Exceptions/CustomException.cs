using System.Net;

namespace TMS.Dapper.Common.Exceptions
{
    public class CustomException : Exception
    {
        public HttpStatusCode StatusCode { get; init; }

        public CustomException(
            string message,
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message)
        {
            StatusCode = statusCode;
        }


    }
}
