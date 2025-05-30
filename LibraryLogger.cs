using System;

namespace LibrarySystem
{
    public class LibraryLogger
    {
        // Delegate for logging
        public delegate void LogHandler(string message);
        
        // Event for logging
        public static event LogHandler OnLog;
        
        // Static method to log messages
        public static void Log(string message)
        {
            OnLog?.Invoke($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
        }
        
        // Static method to log errors
        public static void LogError(string message)
        {
            OnLog?.Invoke($"[ERROR] [{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
        }
    }
} 