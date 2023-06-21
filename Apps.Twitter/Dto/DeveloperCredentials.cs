namespace Apps.Twitter.Dto;

public record DeveloperCredentials
{
    public string ClientId { get; init; }
    public string ClientSecret { get; init; }
}