using Lesson5.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5.Data
{
    internal class FileMonitor_JSON : AbsScanner
    {
        public FileMonitor_JSON()
        {
            Description = "Вас приветсвует монитор файлов .json!";
        }
        
        public override string Description { get; }

        public override MonitorData Scan(FileInfo fInfo)
        {
            if (fInfo.Extension.Equals(".json"))
            {
                string fName = fInfo.Name;
                Random rnd = new Random();

                return new MonitorData(Description, rnd.Next(50, 150), rnd.Next(5, 10));
            }
            else
                return base.Scan(fInfo);
        }
    }
}
