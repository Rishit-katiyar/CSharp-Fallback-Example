using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

public class NetworkRequestFallback
{
    private static HttpClient httpClient = new HttpClient();

    // Primary endpoint URL
    private static string primaryEndpoint = "https://api.example.com/primary";

    // Fallback endpoint URL
    private static string fallbackEndpoint = "https://api.example.com/fallback";

    public static async Task<string> MakeRequest()
    {
        try
        {
            // Attempt to make a request to the primary endpoint
            HttpResponseMessage response = await httpClient.GetAsync(primaryEndpoint);
            response.EnsureSuccessStatusCode(); // Throw if not successful
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error making request to primary endpoint: {ex.Message}");

            // If an error occurs, fallback to the secondary endpoint
            Console.WriteLine("Attempting fallback...");
            try
            {
                HttpResponseMessage fallbackResponse = await httpClient.GetAsync(fallbackEndpoint);
                fallbackResponse.EnsureSuccessStatusCode(); // Throw if not successful
                return await fallbackResponse.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException fallbackEx)
            {
                // If both primary and fallback endpoints fail, log the error and rethrow
                Console.WriteLine($"Error making request to fallback endpoint: {fallbackEx.Message}");
                throw;
            }
        }
    }

    public static async Task Main(string[] args)
    {
        try
        {
            string response = await MakeRequest();
            Console.WriteLine($"Response: {response}");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
