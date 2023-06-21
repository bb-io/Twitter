using Apps.Twitter.Constants;
using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;
using Microsoft.AspNetCore.WebUtilities;

namespace Apps.Twitter.Connections.OAuth2;

public class OAuth2AuthorizationSerivce : IOAuth2AuthorizeService
{
    public string GetAuthorizationUrl(Dictionary<string, string> values)
    {
        var parameters = new Dictionary<string, string>
        {
            { "response_type", "code"},
            { "client_id", values["client_id"] },
            { "redirect_uri", ApplicationConstants.RedirectUri},
            { "scope", ApplicationConstants.Scope},
            { "state", values["state"]},
            { "code_challenge", values["code_challenge"]},
            { "code_challenge_method", "plain"},
        };
        return QueryHelpers.AddQueryString(UrlConstants.TwitterOAuthUrl, parameters);
    }
}