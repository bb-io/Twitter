using Apps.Twitter.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Twitter.Invocables;

public class TwitterInvocable(InvocationContext invocationContext) : BaseInvocable(invocationContext)
{
    protected AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    protected TwitterRestClient Client { get; } = new();
}