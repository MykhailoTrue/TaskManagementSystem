using System.Net;

namespace TMS.EF.NTier.Common.Exceptions
{
    public class BadRequestException : CustomException
    {
        public BadRequestException(string message)
            : base(message, HttpStatusCode.BadRequest)
        {
        }
    }
}
