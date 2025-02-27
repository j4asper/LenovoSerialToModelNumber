using LenovoSerialToModelNumber.Models;

namespace LenovoSerialToModelNumber.Services;

public interface ICsvService
{
    Task CreateCsvFileAsync(string filePath);
    
    Task WriteToCsvAsync(string filePath, Product product);
}