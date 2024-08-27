using Blackbird.Applications.Sdk.Common;

namespace Apps.Twitter.Models.Responses;

public class TweetResponse
{
    [Display("Tweet ID")]
    public string Id { get; set; } = string.Empty;

    [Display("Author ID")]
    public string AuthorId { get; set; } = string.Empty;

    [Display("Conversation ID")]
    public string ConversationId { get; set; } = string.Empty;

    [Display("Text")]
    public string Text { get; set; } = string.Empty;

    [Display("Created at")]
    public DateTime CreatedAt { get; set; }

    [Display("Attachments")]
    public TweetAttachments? Attachments { get; set; }

    [Display("Context annotations")]
    public List<ContextAnnotation> ContextAnnotations { get; set; } = new();

    [Display("Edit history tweet IDs")]
    public List<string> EditHistoryTweetIds { get; set; } = new();

    [Display("Card URI")]
    public string? CardUri { get; set; }
}

public class TweetAttachments
{
    [Display("Media keys")]
    public List<string> MediaKeys { get; set; } = new();
}

public class ContextAnnotation
{
    [Display("Domain")]
    public ContextDomain Domain { get; set; } = new();

    [Display("Entity")]
    public ContextEntity Entity { get; set; } = new();
}

public class ContextDomain
{
    [Display("Context domain ID")]
    public string Id { get; set; } = string.Empty;

    [Display("Name")]
    public string Name { get; set; } = string.Empty;

    [Display("Description")]
    public string Description { get; set; } = string.Empty;
}

public class ContextEntity
{
    [Display("Context entity ID")]
    public string Id { get; set; } = string.Empty;

    [Display("Name")]
    public string Name { get; set; } = string.Empty;
}
