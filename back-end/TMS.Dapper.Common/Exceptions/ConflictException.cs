using System.Net;

namespace TMS.Dapper.Common.Exceptions
{
    public class ConflictException : CustomException
    {
        public ConflictException(string message) 
            : base(message, HttpStatusCode.Conflict)
        {
        }
    }
}
