using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class GoldPriceService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GoldPriceService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<decimal> GetGoldPriceAsync(DateTime date)
    {
        var client = _httpClientFactory.CreateClient();
        var apiKey = "goldapi-36hcqslx7v868z-io";
        var formattedDate = date.ToString("yyyy-MM-dd");
        var url = $"https://www.goldapi.io/api/XAU/USD/{formattedDate}";

        client.DefaultRequestHeaders.Add("x-access-token", apiKey);

        try
        {
            var response = await client.GetFromJsonAsync<GoldApiResponse>(url);
            return response?.Price ?? 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }
}

public class GoldApiResponse
{
    public decimal Price { get; set; }
}
