using System.Collections.Generic;
using System.IO;

namespace Lesson5.Core
{
    internal interface IScannerDevice
    {
        public IScannerDevice SetNextScanDevice(IScannerDevice scannerDevice);
        
        public MonitorData Scan(FileInfo fInfo);
    }
}
