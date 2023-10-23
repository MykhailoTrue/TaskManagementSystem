using System.Net;

namespace TMS.EF.NTier.Common.Exceptions
{
    public class ConflictException : CustomException
    {
        public ConflictException(string message)
            : base(message, HttpStatusCode.Conflict)
        {
        }
    }
}
