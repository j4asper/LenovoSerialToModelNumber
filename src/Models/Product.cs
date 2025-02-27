using System.Text.Json.Serialization;

namespace LenovoSerialToModelNumber.Models;

public class Product
{
    public required string Id { get; set; }
    
    public required string Brand { get; set; }
    
    public required string Name { get; set; }
    
    public required string Serial { get; set; }
    
    public required string Image { get; set; }
    
    [JsonIgnore]
    public string ModelNumber => Id.Split('/')[4];
}