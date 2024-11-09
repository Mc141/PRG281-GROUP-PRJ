using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG282_PRJ
{
    internal class Logger
    {
        private readonly string logFilePath = @"log.txt"; // FilePath of Log file.

        // Method to log messages
        public void LogTransaction(string action, string details, string result)
        {
                // Open or create the log file and write the message with a timestamp
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    // Log the transaction to the Log file.
                    writer.WriteLine("======================================================================");
                    writer.WriteLine($"Date and Time: {DateTime.Now}");
                    writer.WriteLine($"Action: {action}");
                    writer.WriteLine($"Details: {details}");
                    writer.WriteLine($"Result: {result}");
                }
        }
    }
}
