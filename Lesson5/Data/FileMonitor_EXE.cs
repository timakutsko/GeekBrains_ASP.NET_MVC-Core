using Lesson5.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5.Data
{
    internal class FileMonitor_EXE : AbsScanner
    {
        public FileMonitor_EXE()
        {
            Description = "Вас приветсвует монитор файлов .exe!";
        }
        
        public override string Description { get; }

        public override MonitorData Scan(FileInfo fInfo)
        {
            if (fInfo.Extension.Equals(".exe"))
            {
                string fName = fInfo.Name;
                Random rnd = new Random();

                return new MonitorData(Description, rnd.Next(50, 2000), rnd.Next(10, 5000));
            }
            else
                return base.Scan(fInfo);
        }
    }
}
