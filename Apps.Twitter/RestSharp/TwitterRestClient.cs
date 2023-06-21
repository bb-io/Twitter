using Apps.Twitter.Constants;
using RestSharp;

namespace Apps.Twitter.RestSharp;

public class TwitterRestClient : RestClient
{
    public TwitterRestClient() : base(new RestClientOptions
    {
        ThrowOnAnyError = true,
        BaseUrl = new(UrlConstants.TwitterApiUrl)
    })
    {
    }
}