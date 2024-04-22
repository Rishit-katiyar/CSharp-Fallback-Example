using System;
using System.Data.SqlClient;

public class DatabaseAccessFallback
{
    // Primary connection string
    private static string primaryConnectionString = "Data Source=primaryServer;Initial Catalog=primaryDatabase;Integrated Security=True";

    // Fallback connection string
    private static string fallbackConnectionString = "Data Source=fallbackServer;Initial Catalog=fallbackDatabase;Integrated Security=True";

    public static void AccessDatabase()
    {
        try
        {
            // Attempt to connect to the primary database
            using (SqlConnection connection = new SqlConnection(primaryConnectionString))
            {
                connection.Open();
                Console.WriteLine("Connected to primary database.");
                // Perform database operations here
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Error connecting to primary database: {ex.Message}");

            // If connection to the primary database fails, try connecting to the fallback database
            Console.WriteLine("Attempting fallback...");
            try
            {
                using (SqlConnection fallbackConnection = new SqlConnection(fallbackConnectionString))
                {
                    fallbackConnection.Open();
                    Console.WriteLine("Connected to fallback database.");
                    // Perform fallback database operations here
                }
            }
            catch (SqlException fallbackEx)
            {
                // If both primary and fallback databases are inaccessible, log the error
                Console.WriteLine($"Error connecting to fallback database: {fallbackEx.Message}");
            }
        }
    }

    public static void Main(string[] args)
    {
        AccessDatabase();
    }
}
