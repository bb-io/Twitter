namespace Apps.Twitter.Dto;

public class ErrorDto
{
    public string Title { get; set; } = string.Empty;
    
    public string Type { get; set; } = string.Empty;
    
    public int Status { get; set; }
    
    public string Detail { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"Title: {Title}, Type: {Type}, Status: {Status}, Detail: {Detail}";
    }
}