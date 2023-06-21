using System.Text;
using Apps.Twitter.Actions;
using Apps.Twitter.Connections.OAuth2;
using Blackbird.Applications.Sdk.Common.Authentication;

// var url = new OAuth2AuthorizationSerivce().GetAuthorizationUrl(new()
// {
//     {"client_id", "SERzdEhlSGFoRG4xaGNkUWVXYXU6MTpjaQ"},
//     {"state", "state"},
//     {"code_challenge", "chanllenge"},
// });

var code = "SEFIR0FJd1U4MnNnZHYzd0FmM3labDVBbFhULV90TGlNbHRuYWE4R05yWmhlOjE2ODczNjU5OTEwMjY6MTowOmFjOjE";

var actions = new TweetActions();
var service = new OAuth2TokenService();

var token = await service.RequestToken("state", code, new()
{
    {"client_id", "SERzdEhlSGFoRG4xaGNkUWVXYXU6MTpjaQ"},
    {"secret", "g8kp_XBHd_oj27GPiHv7WPYtbTwP1HGQDJDrb8fiVWEETOZ2-h"},
    {"code_challenge", "dsfjhkjdsfhskjdhrejk213jsdfjsdljfk123"}
}, CancellationToken.None);

    await actions.CreateTweet(new List<AuthenticationCredentialsProvider>()
{
    new (AuthenticationCredentialsRequestLocation.QueryString, "access_token", token["access_token"])
}, "Test tweet"u8.ToArray());


Console.ReadKey();