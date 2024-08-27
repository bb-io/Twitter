namespace Apps.Twitter.Models.Responses;

public class BaseSingleResponse<T>
{
    public T Data { get; set; } = default!;
}