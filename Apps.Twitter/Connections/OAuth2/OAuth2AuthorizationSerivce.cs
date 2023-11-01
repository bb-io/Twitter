using Apps.Twitter.Constants;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;
using Blackbird.Applications.Sdk.Common.Invocation;
using Microsoft.AspNetCore.WebUtilities;

namespace Apps.Twitter.Connections.OAuth2;

public class OAuth2AuthorizationSerivce : BaseInvocable, IOAuth2AuthorizeService
{
    public OAuth2AuthorizationSerivce(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public string GetAuthorizationUrl(Dictionary<string, string> values)
    {
        string bridgeOauthUrl = $"{InvocationContext.UriInfo.BridgeServiceUrl.ToString().TrimEnd('/')}/oauth";
        var parameters = new Dictionary<string, string>
        {
            { "response_type", "code"},
            { "client_id", values["client_id"] },
            { "redirect_uri", $"{InvocationContext.UriInfo.BridgeServiceUrl.ToString().TrimEnd('/')}/AuthorizationCode"},
            { "scope", ApplicationConstants.Scope},
            { "state", values["state"]},
            { "code_challenge", values["state"]},
            { "code_challenge_method", "plain"},
            { "authorization_url", UrlConstants.TwitterOAuthUrl},
            { "actual_redirect_uri", InvocationContext.UriInfo.AuthorizationCodeRedirectUri.ToString() },
        };
        return QueryHelpers.AddQueryString(bridgeOauthUrl, parameters);
    }
}