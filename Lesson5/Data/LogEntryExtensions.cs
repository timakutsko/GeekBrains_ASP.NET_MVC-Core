using Lesson5.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5.Data
{
    internal static class LogEntryExtensions
    {
        public static LogEntry WriteLog(this LogEntry logEntry, string path)
        {
            var strBuilder = new StringBuilder();
            strBuilder
                .AppendFormat("[{0}] ", logEntry.EntryDateTime)
                .AppendFormat("[{0}] ", logEntry.EntryLevel)
                .AppendLine(logEntry.Message);
            
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(strBuilder.ToString());
            }

            return logEntry;
        }
    }
}
