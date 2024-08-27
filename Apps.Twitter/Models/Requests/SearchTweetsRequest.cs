using Apps.Twitter.DataSources.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Twitter.Models.Requests;

public class SearchTweetsRequest
{
    [Display("Max results", Description = "Maximum number of results to return. Default is 10")]
    public int? MaxResults { get; set; }

    [Display("Keywords", Description = "Matches a keyword within the body of a Post. This is a tokenized match, meaning that your keyword string will be matched against the tokenized text of the Post body. Tokenization splits words based on punctuation, symbols, and Unicode basic plane separator characters.\n")]
    public IEnumerable<string>? Keywords { get; set; }

    [Display("Exact phrase match", Description = "Matches the exact phrase within the body of a Post.")]
    public bool? ExactPhraseMatch { get; set; }

    [Display("From", Description = "\tMatches any Post from a specific user. The value can be either the username (excluding the @ character) or the user’s numeric user ID.")]
    public string? From { get; set; }
    
    [Display("To", Description = "Matches any Post that is in reply to a particular user. The value can be either the username (excluding the @ character) or the user’s numeric user ID.")]
    public string? To { get; set; }

    [Display("Context", Description = "Matches Posts with a specific domain id/enitity id pair. To learn more about this operator, please visit page on annotations https://developer.x.com/en/docs/x-api/annotations/overview.")]
    public string? Context { get; set; }

    [StaticDataSource(typeof(LanguageDataSource))]
    public string? Language { get; set; }

    [Display("Start time")]
    public DateTime? StartTime { get; set; }
    
    [Display("End time")]
    public DateTime? EndTime { get; set; }
}