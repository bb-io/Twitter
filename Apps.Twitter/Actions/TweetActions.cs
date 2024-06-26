﻿using Apps.Twitter.Constants;
using Apps.Twitter.Models.RequestModels;
using Apps.Twitter.RestSharp;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.Twitter.Actions;

[ActionList]
public class TweetActions
{
    private readonly TwitterRestClient _twitterRestClient = new();

    [Action("Create tweet", Description = "Create tweet on your twitter page")]
    public Task CreateTweet(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter, Display("Tweet text")] string tweetText)
    {
        var request = new TwitterRestRequest(ApiEndpoints.TweetsEndpoint, Method.Post, authenticationCredentialsProviders);
        request.AddJsonBody(new CreateTweet { Text = tweetText });

        return _twitterRestClient.SendTwitterRequest(request);
    }
    
    [Action("Remove tweet", Description = "Remove specified tweet from the page")]
    public Task RemoveTweet(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter, Display("Tweet ID")] string tweetId)
    {
        var endpoint = $"{ApiEndpoints.TweetsEndpoint}/{tweetId}";
        var request = new TwitterRestRequest(endpoint, Method.Delete, authenticationCredentialsProviders);

        return _twitterRestClient.SendTwitterRequest(request);
    }
}