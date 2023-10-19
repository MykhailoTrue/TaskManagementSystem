using System.Net;

namespace TMS.Dapper.Common.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message) 
            : base(message, HttpStatusCode.NotFound)
        {
        }
    }
}
