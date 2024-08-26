using Apps.Twitter.Constants;
using Apps.Twitter.Invocables;
using Apps.Twitter.Models.Identifiers;
using Apps.Twitter.Models.Requests;
using Apps.Twitter.RestSharp;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Twitter.Actions;

[ActionList]
public class TweetActions(InvocationContext invocationContext)
    : TwitterInvocable(invocationContext)
{
    [Action("Create tweet", Description = "Create tweet on your twitter page")]
    public Task CreateTweet([ActionParameter] CreateTweetRequest request)
    {
        var apiRequest = new TwitterRestRequest(ApiEndpoints.TweetsEndpoint, Method.Post, Creds)
            .AddJsonBody(new { text = request.Text });
        return Client.SendTwitterRequest(apiRequest);
    }
    
    [Action("Remove tweet", Description = "Remove specified tweet from the page")]
    public Task RemoveTweet([ActionParameter] TweetIdentifier identifier)
    {
        var endpoint = $"{ApiEndpoints.TweetsEndpoint}/{identifier.TweetId}";
        var request = new TwitterRestRequest(endpoint, Method.Delete, Creds);
        return Client.SendTwitterRequest(request);
    }

    [Action("[DEBUG] Action", Description = "Action for debugging purposes")]
    public List<AuthenticationCredentialsProvider> DebugAction()
    {
        return Creds.ToList();
    }
}