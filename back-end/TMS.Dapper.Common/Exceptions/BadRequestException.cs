using System.Net;

namespace TMS.Dapper.Common.Exceptions
{
    public class BadRequestException : CustomException
    {
        public BadRequestException(string message) 
            : base(message, HttpStatusCode.BadRequest)
        {
        }
    }
}
