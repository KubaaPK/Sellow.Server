using System.Net;

namespace Sellow.Modules.Shared.Abstractions.Exceptions;

public class SellowException : Exception
{
    public HttpStatusCode StatusCode { get; init; }
    public string ErrorCode { get; init; }

    public SellowException(string message) : base(message)
    {
        StatusCode = HttpStatusCode.InternalServerError;
        ErrorCode = "server_error";
    }
}