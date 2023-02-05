using Lesson5.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5.Data
{
    internal class LogService
    {
        private readonly string _path;
        private readonly DirectoryInfo _directory;

        public LogService(string path)
        {
            _path = path;
            _directory = new DirectoryInfo(_path);

            LogFilePath = Path.Combine(_directory.FullName, "MyLog.log");
        }

        public string LogFilePath { get; }
        
        public void PrepareLogFile()
        {
            FileInfo myLogFileInfo = new FileInfo(LogFilePath);
            if (!myLogFileInfo.Exists)
            {
                var logFile = File.Create($"{LogFilePath}");
                logFile.Close();
            }
        }

        public LogEntry PrepareLogEntry(LogLevel logLevel, string message)
        {
            return new LogEntry(DateTime.Now, logLevel, message);
        }
    }
}
