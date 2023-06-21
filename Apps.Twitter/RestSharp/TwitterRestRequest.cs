using Apps.Twitter.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.Twitter.RestSharp;

public class TwitterRestRequest : RestRequest
{
    public TwitterRestRequest(string endpoint, Method method, IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders) :
        base(UrlConstants.TwitterApiUrl + endpoint, method)
    {
        var token = authenticationCredentialsProviders.First(p => p.KeyName == "access_token").Value;
        this.AddHeader("Authorization", $"Bearer {token}");
    }
}