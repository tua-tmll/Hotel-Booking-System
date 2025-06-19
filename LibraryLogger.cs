using System;

namespace LibrarySystem
{
    public class LibraryLogger
    {
        // Generic delegate for logging events
        public delegate void LogEventHandler<T>(T message);
        
        public delegate void LogHandler(string message);
        
        public static event LogHandler OnLog;
        
        public static void Log(string message)
        {
            OnLog?.Invoke($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
        }
        
        public static void LogError(string message)
        {
            OnLog?.Invoke($"[ERROR] [{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
        }
    }
} 