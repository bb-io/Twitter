using Apps.Twitter.Dto;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Twitter.Models.Responses;

public class BasePaginationResponse<T>
{
    public virtual List<T> Data { get; set; } = new();
    
    [DefinitionIgnore]
    public MetaDto Meta { get; set; } = new();
}