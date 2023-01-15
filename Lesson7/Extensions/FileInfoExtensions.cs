using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson7.Extensions
{
    public static class FileInfoExtensions
    {
        public static Process? Execute(this FileInfo file)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo(file.FullName)
            {
                UseShellExecute = true,
            };

            return Process.Start(processStartInfo);
        }
    }
}
