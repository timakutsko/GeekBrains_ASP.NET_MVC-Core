using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5.Core
{
    internal abstract class AbsScanner : IScannerDevice
    {
        private IScannerDevice _nextScannerDevice;

        public AbsScanner() => _nextScannerDevice = null;

        public abstract string Description { get; }

        public IScannerDevice SetNextScanDevice(IScannerDevice scannerDevice)
        {
            _nextScannerDevice = scannerDevice;
            return _nextScannerDevice;
        }

        public virtual MonitorData Scan(FileInfo fInfo)
        {
            if (_nextScannerDevice != null)
                return _nextScannerDevice.Scan(fInfo);
            
            throw new NotImplementedException("Данный тип файлов не поддерживется.");
        }
    }
}
