﻿using System.Text;
using Apps.Twitter.Constants;
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
    private readonly TwitterRestClient _twitterRestClient;
    
    public TweetActions()
    {
        _twitterRestClient = new();
    }
    
    [Action("Create Tweet", Description = "Create tweet on your twitter page")]
    public Task CreateTweet(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] byte[] input)
    {
        var textInput = Encoding.UTF8.GetString(input);
        
        var request = new TwitterRestRequest(ApiEndpoints.TweetsEndpoint, Method.Post, authenticationCredentialsProviders);
        request.AddJsonBody(new CreateTweet { Text = textInput });

        return _twitterRestClient.ExecuteAsync(request);
    }
    
    [Action("Remove Tweet", Description = "Remove specified tweet from the page")]
    public Task RemoveTweet(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] string tweetId)
    {
        var endpoint = $"{ApiEndpoints.TweetsEndpoint}/{tweetId}";
        var request = new TwitterRestRequest(endpoint, Method.Delete, authenticationCredentialsProviders);

        return _twitterRestClient.ExecuteAsync(request);
    }
}