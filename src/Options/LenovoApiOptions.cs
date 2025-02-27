using System.ComponentModel.DataAnnotations;

namespace LenovoSerialToModelNumber.Options;

public class LenovoApiOptions
{
    public static readonly string LenovoApi = nameof(LenovoApi);
    
    [Required]
    public required string GetProductEndpoint { get; set; }
}