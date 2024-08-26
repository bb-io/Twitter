using Blackbird.Applications.Sdk.Common;

namespace Apps.Twitter.Models.Identifiers;

public class TweetIdentifier
{
    [Display("Tweet ID")]
    public string TweetId { get; set; } = string.Empty;
}