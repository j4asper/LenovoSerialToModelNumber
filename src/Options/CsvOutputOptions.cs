using System.ComponentModel.DataAnnotations;

namespace LenovoSerialToModelNumber.Options;

public class CsvOutputOptions
{
    public static readonly string CsvOutput = nameof(CsvOutput);
    
    [Required]
    public required string Header { get; set; }
    
    [Required]
    public required string Lines { get; set; }
}