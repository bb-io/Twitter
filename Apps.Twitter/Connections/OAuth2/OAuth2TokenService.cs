using System.Text;
using System.Text.Json;
using Apps.Twitter.Constants;
using Apps.Twitter.Dto;
using Apps.Twitter.RestSharp;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Twitter.Connections.OAuth2;

public class OAuth2TokenService : BaseInvocable, IOAuth2TokenService
{
    private readonly TwitterRestClient _twitterClient;

    public OAuth2TokenService(InvocationContext invocationContext) : base(invocationContext)
    {
        _twitterClient = new();
    }

    public Task<Dictionary<string, string>> RequestToken(
        string state,
        string code,
        Dictionary<string, string> values,
        CancellationToken cancellationToken)
    {
        var requestUrl = UrlConstants.TwitterApiUrl + ApiEndpoints.TokenEndpoint;
        var developerCredentials = new DeveloperCredentials()
        {
            ClientId = values["client_id"],
            ClientSecret = values["secret"],
        };
        
        var bodyParameters = new Dictionary<string, string>
        {
            { "grant_type", "authorization_code" },
            { "client_id", values["client_id"] },
            { "redirect_uri", $"{InvocationContext.UriInfo.BridgeServiceUrl.ToString().TrimEnd('/')}/AuthorizationCode" },
            { "code_verifier", state },
            { "code", code }
        };

        return GetDictionaryResponse(requestUrl, developerCredentials, bodyParameters, cancellationToken);
    }

    public Task<Dictionary<string, string>> RefreshToken(Dictionary<string, string> values,
        CancellationToken cancellationToken)
    {
        var requestUrl = UrlConstants.TwitterApiUrl + ApiEndpoints.TokenEndpoint;
        var developerCredentials = new DeveloperCredentials()
        {
            ClientId = values["client_id"],
            ClientSecret = values["secret"],
        };
        
        var bodyParameters = new Dictionary<string, string>
        {
            { "grant_type", "refresh_token" },
            { "refresh_token", values["refresh_token"] },
            { "client_id", values["client_id"] },
        };
        
        return GetDictionaryResponse(requestUrl, developerCredentials, bodyParameters, cancellationToken);
    }

    public async Task RevokeToken(Dictionary<string, string> values)
    {
        var requestUrl = UrlConstants.TwitterApiUrl + ApiEndpoints.RevokeTokenEndpoint;
        var developerCredentials = new DeveloperCredentials()
        {
            ClientId = values["client_id"],
            ClientSecret = values["secret"],
        };
        
        var bodyParameters = new Dictionary<string, string>
        {
            { "token", values["access_token"] },
            { "client_id", values["client_id"] },
            { "token_type_hint", "access_token" },
        };

        await ExecuteRequest(requestUrl, developerCredentials, bodyParameters, CancellationToken.None);
    }

    public bool IsRefreshToken(Dictionary<string, string> values) => true;

    private async Task<RestResponse> ExecuteRequest(
        string url,
        DeveloperCredentials developerCredentials,
        Dictionary<string, string> bodyParameters,
        CancellationToken cancellationToken)
    {
        var appCredentials = Encoding.ASCII.GetBytes(
            $"{developerCredentials.ClientId}:{developerCredentials.ClientSecret}");
        var authHeader = "Basic " + Convert.ToBase64String(appCredentials);
        
        var request = new RestRequest(url, Method.Post);
        request.AddHeader("Authorization", authHeader);
        bodyParameters.ToList().ForEach(x => request.AddParameter(x.Key, x.Value));

        return await _twitterClient.SendTwitterRequest(request, cancellationToken);
    }

    private async Task<Dictionary<string, string>> GetDictionaryResponse(string requestUrl,
        DeveloperCredentials developerCredentials,
        Dictionary<string, string> bodyParameters,
        CancellationToken cancellationToken)
    {
        var response = await ExecuteRequest(requestUrl, developerCredentials, bodyParameters, cancellationToken);
        var content = response.Content;

        var deserializedData = JsonSerializer.Deserialize<Dictionary<string, object>>(content);
        
        return deserializedData
            .ToDictionary(r => r.Key, r => r.Value.ToString());
    }
}