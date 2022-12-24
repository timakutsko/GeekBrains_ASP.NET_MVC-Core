using Lesson5.Client;
using Lesson5.Data;
using System;
using System.IO;

namespace Lesson5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();

            LogService logService = new LogService(path);
            logService.PrepareLogFile();
            
            MonitorService monitorService = new MonitorService(path, logService);
            monitorService.RunMonitoring();
        }
    }
}
