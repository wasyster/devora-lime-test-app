namespace Solution.Core.Models.Exceptions;

public class HttpStatusCodeException : Exception
{
    /// <summary>
    /// HTTP Status Code
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// HTTP Content Type of the Error Response
    /// </summary>
    public string ContentType { get; private set; } = @"application/json";

    /// <summary>
    /// Translation key
    /// </summary>
    public string Key { get; set; }

    public HttpStatusCodeException()
    {
    }

    public HttpStatusCodeException(HttpStatusCode statusCode)
    {
        StatusCode = (int)statusCode;
    }

    public HttpStatusCodeException(int statusCode, string message, string key) : base(message)
    {
        StatusCode = statusCode;
        Key = key;
    }

    public HttpStatusCodeException(HttpStatusCode statusCode, string message, string key) : base(message)
    {
        StatusCode = (int)statusCode;
        Key = key;
    }

    public HttpStatusCodeException(HttpStatusCode statusCode, Exception inner, string key) : base(inner.ToString())
    {
        Key = key;
        StatusCode = (int)statusCode;
    }

    public HttpStatusCodeException(HttpStatusCode statusCode, GlobalErrorResponse errorObject, string key) : base(errorObject.Exception)
    {
        Key = key;
        StatusCode = (int)statusCode;
    }
}
