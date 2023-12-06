using System.Net;

namespace TMS.EF.NTier.Common.Exceptions
{
    public class InternalServerException : CustomException
    {
        public InternalServerException(string message)
            : base(message, HttpStatusCode.InternalServerError)
        {
        }
    }
}
