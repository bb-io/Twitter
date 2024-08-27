using Apps.Twitter.Api;
using Apps.Twitter.Constants;
using Apps.Twitter.Invocables;
using Apps.Twitter.Models.Identifiers;
using Apps.Twitter.Models.Requests;
using Apps.Twitter.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Twitter.Actions;

[ActionList]
public class TweetActions(InvocationContext invocationContext)
    : TwitterInvocable(invocationContext)
{
    [Action("Search tweets", Description = "Search tweets by specified parameters")]
    public Task<SearchTweetsResponse> SearchTweets([ActionParameter] SearchTweetsRequest request)
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

        return Client.ExecuteWithErrorHandling<SearchTweetsResponse>(apiRequest);
    }
    
    [Action("Get tweet", Description = "Get tweet by specified ID")]
    public async Task<TweetResponse> GetTweet([ActionParameter] TweetIdentifier identifier)
    {
        var endpoint = $"{ApiEndpoints.TweetsEndpoint}/{identifier.TweetId}";
        var request = new TwitterRestRequest(endpoint, Method.Get, Creds)
            .AddQueryParameter("tweet.fields", "article,attachments,author_id,card_uri,context_annotations,conversation_id,created_at,edit_history_tweet_ids");
        var response = await Client.ExecuteWithErrorHandling<BaseSingleResponse<TweetResponse>>(request);
        return response.Data;
    }
    
    [Action("Create tweet", Description = "Create tweet on your twitter page")]
    public async Task<TweetResponse> CreateTweet([ActionParameter] CreateTweetRequest request)
    {
        var apiRequest = new TwitterRestRequest(ApiEndpoints.TweetsEndpoint, Method.Post, Creds)
            .AddJsonBody(new { text = request.Text });
        var response = await Client.ExecuteWithErrorHandling<BaseSingleResponse<TweetResponse>>(apiRequest);
        
        // Response doesn't contain all tweet fields so we need to get it again
        return await GetTweet(new TweetIdentifier { TweetId = response.Data.Id });
    }
    
    [Action("Delete tweet", Description = "Delete specified tweet by ID")]
    public Task RemoveTweet([ActionParameter] TweetIdentifier identifier)
    {
        var endpoint = $"{ApiEndpoints.TweetsEndpoint}/{identifier.TweetId}";
        var request = new TwitterRestRequest(endpoint, Method.Delete, Creds);
        return Client.ExecuteWithErrorHandling(request);
    }
    
    [Action("Retweet", Description = "Retweet specified tweet by id and user")]
    public Task Retweet([ActionParameter] UserIdentifier userIdentifier, 
        [ActionParameter] TweetIdentifier tweetIdentifier)
    {
        var endpoint = $"{ApiEndpoints.UsersEndpoint}/{userIdentifier.UserId}/retweets";
        var apiRequest = new TwitterRestRequest(endpoint, Method.Post, Creds)
            .AddJsonBody(new { tweet_id = tweetIdentifier.TweetId });
        return Client.ExecuteWithErrorHandling(apiRequest);
    }
    
    [Action("Unretweet", Description = "Unretweet specified tweet by id and user")]
    public Task Unretweet([ActionParameter] UserIdentifier userIdentifier, 
        [ActionParameter] TweetIdentifier tweetIdentifier)
    {
        var endpoint = $"{ApiEndpoints.UsersEndpoint}/{userIdentifier.UserId}/retweets/{tweetIdentifier.TweetId}";
        var request = new TwitterRestRequest(endpoint, Method.Delete, Creds);
        return Client.ExecuteWithErrorHandling(request);
    }
}