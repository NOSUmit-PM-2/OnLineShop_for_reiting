using System;
using System.IO;

namespace WebApplicationShopOnline.Helpers
{
    public static class Logger
    {
        private static readonly string LogFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", "app.log");

        public static void LogAction(string action, string details)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(LogFilePath));
                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {action}: {details}";
                File.AppendAllText(LogFilePath, logMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                // If logging fails, we don't want to break the application
                Console.WriteLine($"Logging failed: {ex.Message}");
            }
        }
    }
} 