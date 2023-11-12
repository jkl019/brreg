using System.Net;

namespace Logging;

public class Error {

    public Error(string orgNumber, HttpStatusCode code, string message) => LogError(orgNumber, code, message);

    public static void LogError(string orgNumber, HttpStatusCode code, string message)
    {
        string logFilePath = "error_log.txt";

        try
        {
            using (StreamWriter writer = File.AppendText(logFilePath))
            {
                writer.WriteLine($"{orgNumber} - Code: {code} - Message: {message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to log error: {ex.Message}");
        }
    }

}