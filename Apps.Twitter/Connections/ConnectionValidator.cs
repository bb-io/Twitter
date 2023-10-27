using Apps.Twitter.RestSharp;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using RestSharp;

namespace Apps.Twitter.Connections;

public class ConnectionValidator : IConnectionValidator
{
    public async ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> authProviders, CancellationToken cancellationToken)
    {
        var client = new TwitterRestClient();
        var request = new TwitterRestRequest("/me", Method.Get, authProviders);
        
        try
        {
            await client.SendTwitterRequest(request, cancellationToken);
            return new ConnectionValidationResponse
            {
                IsValid = true
            };
        }
        catch (Exception)
        {
            return new ConnectionValidationResponse
            {
                IsValid = false,
                Message = "Ping failed"
            };
        }
    }
}