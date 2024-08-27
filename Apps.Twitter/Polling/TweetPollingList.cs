using Apps.Twitter.Constants;
using Apps.Twitter.Invocables;
using Apps.Twitter.Models.Requests;
using Apps.Twitter.Models.Responses;
using Apps.Twitter.Polling.Models;
using Apps.Twitter.Polling.Models.Requests;
using Apps.Twitter.RestSharp;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;
using RestSharp;

namespace Apps.Twitter.Polling;

[PollingEventList]
public class TweetPollingList(InvocationContext invocationContext) : TwitterInvocable(invocationContext)
{
    [PollingEvent("On tweets created", "Triggered after specified time interval to check for new tweets")]
    public async Task<PollingEventResponse<DateMemory, SearchTweetsResponse>> OnTweetsCreated(
        PollingEventRequest<DateMemory> request,
        [PollingEventParameter] SearchTweetsPollingRequest searchTweetsRequest)
    {
        if (request.Memory is null)
        {
            return new()
            {
                FlyBird = false,
                Memory = new()
                {
                    LastInteractionDate = DateTime.UtcNow
                }
            };
        }
        
        var tweets = await SearchTweets(new SearchTweetsRequest(searchTweetsRequest, request.Memory.LastInteractionDate));
        return new()
        {
            FlyBird = tweets.Data.Any(),
            Result = tweets,
            Memory = new()
            {
                LastInteractionDate = DateTime.UtcNow
            }
        };
    }

    private async Task<SearchTweetsResponse> SearchTweets(SearchTweetsRequest request)
    {
        var apiRequest = new TwitterRestRequest(ApiEndpoints.SearchTweetsEndpoint, Method.Get, Creds)
            .AddQueryParameter("max_results", request.MaxResults.HasValue ? request.MaxResults.ToString() : "10")
            .AddQueryParameter("tweet.fields", "article,attachments,author_id,card_uri,context_annotations,conversation_id,created_at,edit_history_tweet_ids");

        if (request.Keywords != null && request.Keywords.Any())
        {
            var query = request.ExactPhraseMatch.HasValue && request.ExactPhraseMatch.Value 
                ? "\"" + string.Join(" ", request.Keywords) + "\"" 
                : string.Join(" OR ", request.Keywords);
            apiRequest.AddQueryParameter("query", query);
        }

        if (!string.IsNullOrEmpty(request.From))
            apiRequest.AddQueryParameter("query", $"from:{request.From}");

        if (!string.IsNullOrEmpty(request.To))
            apiRequest.AddQueryParameter("query", $"to:{request.To}");

        if (!string.IsNullOrEmpty(request.Context))
            apiRequest.AddQueryParameter("query", $"context:{request.Context}");

        if (!string.IsNullOrEmpty(request.Language))
            apiRequest.AddQueryParameter("query", $"lang:{request.Language}");

        if (request.StartTime.HasValue)
            apiRequest.AddQueryParameter("start_time", request.StartTime.Value.ToString("yyyy-MM-ddTHH:mm:ssZ"));

        if (request.EndTime.HasValue)
            apiRequest.AddQueryParameter("end_time", request.EndTime.Value.ToString("yyyy-MM-ddTHH:mm:ssZ"));

        return await Client.ExecuteWithErrorHandling<SearchTweetsResponse>(apiRequest);
    }
}