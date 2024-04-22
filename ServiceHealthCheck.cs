using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

public class ServiceHealthCheck
{
    // Primary service URL
    private static string primaryServiceUrl = "http://primary-service.com";

    // Fallback service URL
    private static string fallbackServiceUrl = "http://fallback-service.com";

    public static async Task<bool> CheckServiceHealth(string serviceUrl)
    {
        try
        {
            Ping ping = new Ping();
            PingReply reply = await ping.SendPingAsync(new Uri(serviceUrl).Host);
            return reply.Status == IPStatus.Success;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error checking service health for {serviceUrl}: {ex.Message}");
            return false;
        }
    }

    public static async Task<string> GetServiceData()
    {
        // Check primary service health
        bool isPrimaryServiceHealthy = await CheckServiceHealth(primaryServiceUrl);

        if (isPrimaryServiceHealthy)
        {
            Console.WriteLine("Primary service is healthy. Fetching data...");
            // Call primary service API and return data
            return await CallServiceApi(primaryServiceUrl);
        }
        else
        {
            Console.WriteLine("Primary service is down. Attempting fallback...");
            // Check fallback service health
            bool isFallbackServiceHealthy = await CheckServiceHealth(fallbackServiceUrl);
            if (isFallbackServiceHealthy)
            {
                Console.WriteLine("Fallback service is healthy. Fetching data...");
                // Call fallback service API and return data
                return await CallServiceApi(fallbackServiceUrl);
            }
            else
            {
                Console.WriteLine("Fallback service is also down. Unable to fetch data.");
                return null;
            }
        }
    }

    public static async Task<string> CallServiceApi(string serviceUrl)
    {
        // Call service API and return data
        // For demonstration, returning a placeholder string
        return "Data from service API";
    }

    public static async Task Main(string[] args)
    {
        string serviceData = await GetServiceData();
        if (serviceData != null)
        {
            Console.WriteLine($"Service data: {serviceData}");
        }
        else
        {
            Console.WriteLine("Failed to retrieve service data.");
        }
    }
}
