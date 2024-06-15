namespace Solution.Core.Models.Response;

public class GlobalErrorResponse
{
    public string Exception { get; set; }

    public GlobalErrorResponse()
    {}
    public GlobalErrorResponse(string exceptionMessage)
    {
        Exception = exceptionMessage;
    }

    public GlobalErrorResponse(Exception? ex)
    {
        Exception = ex?.Message ?? string.Empty;
    }
}
