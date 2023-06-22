using System.Net;
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

    public async Task<RestResponse> SendTwitterRequest(RestRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await ExecuteAsync(request, cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            var message = ex.StatusCode switch
            {
                HttpStatusCode.TooManyRequests =>
                    "You've exceeded your requests limit. Please wait approximately 15 min to continue",
                _ => ex.Message
            };

            throw new(message);
        }
    }
}