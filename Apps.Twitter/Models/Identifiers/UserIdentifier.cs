using Blackbird.Applications.Sdk.Common;

namespace Apps.Twitter.Models.Identifiers;

public class UserIdentifier
{
    [Display("User ID")]
    public string UserId { get; set; } = string.Empty;
}