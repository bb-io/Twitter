namespace Apps.Twitter.Dto;

public class MetaDto
{
    public string NewestId { get; set; } = string.Empty;
    
    public string OldestId { get; set; } = string.Empty;
    
    public int ResultCount { get; set; }
    
    public string NextToken { get; set; } = string.Empty;
}