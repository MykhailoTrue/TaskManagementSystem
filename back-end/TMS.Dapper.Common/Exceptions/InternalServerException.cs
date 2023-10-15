using System.Net;

namespace TMS.Dapper.Common.Exceptions
{
    public class InternalServerException : CustomException
    {
        public InternalServerException(string message) 
            : base(message, HttpStatusCode.InternalServerError)
        {
        }
    }
}
