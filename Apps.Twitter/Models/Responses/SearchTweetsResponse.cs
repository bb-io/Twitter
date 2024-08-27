using Blackbird.Applications.Sdk.Common;

namespace Apps.Twitter.Models.Responses;

public class SearchTweetsResponse : BasePaginationResponse<TweetResponse>
{
    [Display("Tweets")]
    public override List<TweetResponse> Data { get; set; } = new();
}