using System.Net;

namespace Catalog.Domain.Errors
{
    public sealed class AppError : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public AppError(string detail, HttpStatusCode statusCode) : base(detail)
        {
            StatusCode = statusCode;
        }
    }
}