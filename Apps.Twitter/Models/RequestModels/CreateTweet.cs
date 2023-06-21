namespace Apps.Twitter.Models.RequestModels;

public record CreateTweet
{
    public string Text { get; init; }
}