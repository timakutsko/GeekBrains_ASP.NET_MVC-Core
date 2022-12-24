using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5.Core
{
    internal enum LogLevel
    {
        Information,
        Warning,
        Error
    }

    internal sealed class LogEntry
    {
        public LogEntry(DateTime dateTime, LogLevel level, string message)
        {
            EntryDateTime = dateTime;
            EntryLevel = level;
            Message = message;
        }
        
        public DateTime EntryDateTime { get; internal set; }

        public LogLevel EntryLevel { get; internal set; }

        public string Message { get; internal set; }
    }
}
