using System;

public class FallbackExample
{
    // Fallback method 1
    public static void FallbackMethod1()
    {
        Console.WriteLine("Fallback method 1 executed.");
    }

    // Fallback method 2
    public static void FallbackMethod2()
    {
        Console.WriteLine("Fallback method 2 executed.");
    }

    // Main method
    public static void Main(string[] args)
    {
        try
        {
            // Code that may throw an exception
            // For demonstration, let's assume an exception occurs
            throw new Exception("Something went wrong!");
        }
        catch (Exception ex)
        {
            // Handle the exception by calling fallback methods
            Console.WriteLine($"Exception occurred: {ex.Message}");

            // Try FallbackMethod1
            try
            {
                FallbackMethod1();
            }
            catch
            {
                // If FallbackMethod1 fails, try FallbackMethod2
                Console.WriteLine("FallbackMethod1 failed. Trying FallbackMethod2...");
                FallbackMethod2();
            }
        }
    }
}
