using System;
using System.IO;

public class FileProcessingFallback
{
    // Primary file path
    private static string primaryFilePath = "primary_data.txt";

    // Fallback file path
    private static string fallbackFilePath = "fallback_data.txt";

    public static void ProcessFile()
    {
        try
        {
            // Attempt to read data from the primary file
            string data = File.ReadAllText(primaryFilePath);
            Console.WriteLine("Data from primary file:");
            Console.WriteLine(data);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Primary file not found: {ex.Message}");

            // If the primary file is not found, try reading from the fallback file
            Console.WriteLine("Attempting fallback...");
            try
            {
                string fallbackData = File.ReadAllText(fallbackFilePath);
                Console.WriteLine("Data from fallback file:");
                Console.WriteLine(fallbackData);
            }
            catch (FileNotFoundException fallbackEx)
            {
                // If both primary and fallback files are not found, log the error
                Console.WriteLine($"Fallback file not found: {fallbackEx.Message}");
            }
        }
    }

    public static void Main(string[] args)
    {
        ProcessFile();
    }
}
