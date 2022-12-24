using Lesson5.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5.Data
{
    internal class FileMonitor_DLL : AbsScanner
    {
        public FileMonitor_DLL()
        {
            Description = "Вас приветсвует монитор файлов .dll!";
        }
        
        public override string Description { get; }

        public override MonitorData Scan(FileInfo fInfo)
        {
            if (fInfo.Extension.Equals(".dll"))
            {
                string fName = fInfo.Name;
                Random rnd = new Random();

                return new MonitorData(Description, rnd.Next(100, 400), rnd.Next(10, 50));
            }
            else
                return base.Scan(fInfo);
        }
    }
}
