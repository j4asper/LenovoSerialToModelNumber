using LenovoSerialToModelNumber.Models;

namespace LenovoSerialToModelNumber.Services;

public interface ILenovoApiService
{
    Task<Product?> GetProductBySerialNumberAsync(string serialNumber);
}