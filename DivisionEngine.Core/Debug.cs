﻿namespace DivisionEngine
{
    /// <summary>
    /// Represents the severity level of a log entry.
    /// </summary>
    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error,
    }

    /// <summary>
    /// Debugging and logging utility for the Division Engine.
    /// </summary>
    public class Debug
    {
        /// <summary>
        /// Represents a single log entry in the debug log.
        /// </summary>
        /// <param name="message">?Message of the log entry</param>
        /// <param name="level">Log level</param>
        public class LogEntry(string message, LogLevel level)
        {
            public DateTime Timestamp { get; } = DateTime.Now;
            public string Message { get; } = message;
            public LogLevel Level { get; } = level;

            /// <summary>
            /// Converts log entry to a string representation.
            /// </summary>
            /// <returns>Log entry string</returns>
            public override string ToString()
            {
                return $"[{Level}] {Timestamp}: {Message}";
            }
        }

        private static readonly Debug instance = new Debug();
        private readonly List<LogEntry> debugLog = [];

        public static event Action<LogEntry>? OnLogUpdate;
        public static IReadOnlyList<LogEntry> Logs => instance.debugLog;

        /// <summary>
        /// Creates a new Debug instance and initializes the debug log.
        /// </summary>
        public Debug()
        {
            debugLog.Add(new LogEntry("Debug system initialized.", LogLevel.Info));
        }

        /// <summary>
        /// Creates an info log entry.
        /// </summary>
        /// <param name="message">Message of the info log</param>
        public static void Info(string message) => Log(message, LogLevel.Info);

        /// <summary>
        /// Creates an error log entry.
        /// </summary>
        /// <param name="message">Message of the error log</param>
        public static void Error(string message) => Log(message, LogLevel.Error);

        /// <summary>
        /// Creates a warning log entry.
        /// </summary>
        /// <param name="message">Message of the warning log</param>
        public static void Warning(string message) => Log(message, LogLevel.Warning);

        /// <summary>
        /// Creates a debug log entry.
        /// </summary>
        /// <param name="message">Message of the log entry</param>
        /// <param name="level">Level of the log entry</param>
        public static void Log(string message, LogLevel level)
        {
            string prefix = level switch
            {
                LogLevel.Debug => "[DEBUG]",
                LogLevel.Info => "[INFO]",
                LogLevel.Warning => "[WARNING]",
                LogLevel.Error => "[ERROR]",
                _ => "[LOG]"
            };
            
            LogEntry entry = new LogEntry(message, level);
            Console.WriteLine(entry.ToString());
            instance.debugLog.Add(entry);
            OnLogUpdate?.Invoke(entry);
        }
    }
}
