using System.Net.Http;
using System.Net.Http.Json;
using LenovoSerialToModelNumber.Models;
using LenovoSerialToModelNumber.Options;
using Microsoft.Extensions.Options;

namespace LenovoSerialToModelNumber.Services;

public class LenovoApiService : ILenovoApiService
{
    private readonly HttpClient httpClient;
    private readonly LenovoApiOptions options;

    public LenovoApiService(HttpClient httpClient, IOptions<LenovoApiOptions> options)
    {
        this.httpClient = httpClient;
        this.options = options.Value;
    }
    
    
    public async Task<Product?> GetProductBySerialNumberAsync(string serialNumber)
    {
        var response = await httpClient.GetAsync(options.GetProductEndpoint + $"?productId={serialNumber}");
        
        response.EnsureSuccessStatusCode();
        
        var product = await response.Content.ReadFromJsonAsync<Product[]>() ?? [];
        
        return product.FirstOrDefault();
    }
}