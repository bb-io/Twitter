using Blackbird.Applications.Sdk.Common;

namespace Apps.Twitter.Models.Responses;

public class UserResponse
{
    [Display("User ID")]
    public string Id { get; set; } = string.Empty;
    
    public string Username { get; set; } = string.Empty;
    
    public bool Protected { get; set; }
    
    [Display("Created at")]
    public DateTime CreatedAt { get; set; }
    
    [Display("Receives your DM")]
    public bool ReceivesYourDm { get; set; }
    
    public bool Verified { get; set; }
    
    [Display("Subscription type")]
    public string SubscriptionType { get; set; } = string.Empty;
    
    [Display("Verified type")]
    public string VerifiedType { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;
    
    [Display("Most recent tweet ID")]
    public string MostRecentTweetId { get; set; } = string.Empty;
    
    [Display("Public metrics")]
    public PublicMetrics PublicMetrics { get; set; } = new();
    
    public string Description { get; set; } = string.Empty;
    
    [Display("Profile image URL")]
    public string ProfileImageUrl { get; set; } = string.Empty;
}

public class PublicMetrics
{
    [Display("Followers count")]
    public int FollowersCount { get; set; }
    
    [Display("Following count")]
    public int FollowingCount { get; set; }
    
    [Display("Tweet count")]
    public int TweetCount { get; set; }
    
    [Display("Listed count")]
    public int ListedCount { get; set; }
    
    [Display("Like count")]
    public int LikeCount { get; set; }
}