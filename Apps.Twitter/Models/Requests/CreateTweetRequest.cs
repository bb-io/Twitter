using Blackbird.Applications.Sdk.Common;

namespace Apps.Twitter.Models.Requests;

public class CreateTweetRequest
{
    [Display("Tweet text")]
    public string Text { get; set; } = string.Empty;
}