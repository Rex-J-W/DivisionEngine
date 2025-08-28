using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivisionEngine
{
    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error,
    }

    public class Debug
    {
        private static readonly Debug instance = new Debug();

        public static void Log(string message)
        {
            Console.WriteLine($"[DEBUG] {DateTime.Now}: {message}");
        }
    }
}
