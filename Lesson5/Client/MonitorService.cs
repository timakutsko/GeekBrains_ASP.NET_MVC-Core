using Lesson5.Core;
using Lesson5.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5.Client
{
    internal class MonitorService
    {
        private readonly AbsScanner _fileMonitor_EXE;
        private readonly AbsScanner _fileMonitor_DLL;
        private readonly AbsScanner _fileMonitor_JSON;

        private readonly string _path;
        private readonly DirectoryInfo _directory;

        private readonly LogService _logService;

        public MonitorService(string path, LogService logService)
        {
            _fileMonitor_EXE = new FileMonitor_EXE();
            _fileMonitor_DLL = new FileMonitor_DLL();
            _fileMonitor_JSON = new FileMonitor_JSON();
            _fileMonitor_EXE.SetNextScanDevice(_fileMonitor_DLL).SetNextScanDevice(_fileMonitor_JSON);

            _path = path; 
            _directory = new DirectoryInfo(_path);

            _logService = logService;
        }

        public void RunMonitoring()
        {
            foreach (FileInfo fileInfo in _directory.GetFiles())
            {
                _logService
                    .PrepareLogEntry(LogLevel.Information, $"Приступаю к анализу файла {fileInfo.FullName}...")
                    .WriteLog(_logService.LogFilePath);

                try
                {
                    MonitorData monitorData = _fileMonitor_EXE.Scan(fileInfo);
                    if (monitorData != null)
                    {
                        string logMessege = $"{monitorData.MonitoredBy} " +
                            $"Выделяемые ресурсы на обработку: " +
                            $"CPU: {monitorData.CPU}, RAM: {monitorData.RAM}.";

                        _logService
                            .PrepareLogEntry(LogLevel.Information, logMessege)
                            .WriteLog(_logService.LogFilePath);
                    }
                    else
                        throw new Exception("Что-то пошло не так...");
                }
                catch (NotImplementedException ex)
                {
                    _logService
                        .PrepareLogEntry(LogLevel.Warning, ex.Message)
                        .WriteLog(_logService.LogFilePath);
                }
                catch (Exception ex)
                {
                    _logService
                        .PrepareLogEntry(LogLevel.Error, ex.Message)
                        .WriteLog(_logService.LogFilePath);
                }

                _logService
                    .PrepareLogEntry(LogLevel.Information, $"Анализ файла завершен!")
                    .WriteLog(_logService.LogFilePath);
            }
        }
    }
}
