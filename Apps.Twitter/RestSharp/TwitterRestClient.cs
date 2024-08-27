using System.Net;
using Apps.Twitter.Constants;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.Twitter.RestSharp;

public class TwitterRestClient() : BlackBirdRestClient(new RestClientOptions { ThrowOnAnyError = true, BaseUrl = new(UrlConstants.TwitterApiUrl) })
{
    protected override JsonSerializerSettings JsonSettings => JsonConfig.JsonSettings;

    protected override Exception ConfigureErrorException(RestResponse response)
    {
        if(response.StatusCode == HttpStatusCode.TooManyRequests)
            return new("You've exceeded your requests limit. Please wait approximately 15 min to continue");

        return new Exception($"Status code: {response.StatusCode}, Content: {response.Content}");
    }
}