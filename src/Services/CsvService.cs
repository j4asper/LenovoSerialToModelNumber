using System.IO;
using System.Reflection;
using LenovoSerialToModelNumber.Models;
using LenovoSerialToModelNumber.Options;
using Microsoft.Extensions.Options;

namespace LenovoSerialToModelNumber.Services;

public class CsvService : ICsvService
{
    private readonly CsvOutputOptions options;

    public CsvService(IOptions<CsvOutputOptions> options)
    {
        this.options = options.Value;
    }
    
    public async Task CreateCsvFileAsync(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                await using var writer = new StreamWriter(filePath);
                
                await writer.WriteLineAsync(options.Header);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating CSV file: {ex.Message}");
        }
    }

    public async Task WriteToCsvAsync(string filePath, Product product)
    {
        try
        {
            await using var writer = new StreamWriter(filePath, append: true);
            
            await writer.WriteLineAsync(ReplacePlaceholders(product));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to CSV file: {ex.Message}");
        }
    }
    
    private string ReplacePlaceholders(Product product)
    {
        var result = options.Lines;
        
        var properties = product.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        
        foreach (var property in properties)
        {
            var placeholder = $"{{{property.Name}}}";

            if (!result.Contains(placeholder))
                continue;
            
            var value = property.GetValue(product)?.ToString();
                
            result = result.Replace(placeholder, value ?? string.Empty);
        }

        return result;
    }
}