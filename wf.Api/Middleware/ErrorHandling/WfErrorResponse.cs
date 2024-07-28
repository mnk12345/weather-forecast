namespace wf.Api.Middleware.ErrorHandling;

public class WfErrorResponse
{
    public string ErrorCode { get; set; } = default!;
    public string? ErrorMessage { get; set; }
    public string? ErrorStackTrace { get; set; }
}
