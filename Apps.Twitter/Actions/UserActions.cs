using Apps.Twitter.Api;
using Apps.Twitter.Constants;
using Apps.Twitter.Invocables;
using Apps.Twitter.Models.Requests;
using Apps.Twitter.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Twitter.Actions;

[ActionList]
public class UserActions(InvocationContext invocationContext)
    : TwitterInvocable(invocationContext)
{
    [Action("Get user by username", Description = "Get user by specified username")]
    public async Task<UserResponse> GetUser([ActionParameter] GetUserByUsernameRequest request)
    {
        var endpoint = $"{ApiEndpoints.UsersEndpoint}/by/username/{request.Username}";
        var apiRequest = new TwitterRestRequest(endpoint, Method.Get, Creds)
            .AddQueryParameter("user.fields", "affiliation,connection_status,created_at,description,entities,id,location,most_recent_tweet_id,name,pinned_tweet_id,profile_banner_url,profile_image_url,protected,public_metrics,receives_your_dm,subscription_type,url,username,verified,verified_type,withheld");
        var response = await Client.ExecuteWithErrorHandling<BaseSingleResponse<UserResponse>>(apiRequest);
        return response.Data;
    }
}