namespace wf.Domain.Common;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}
