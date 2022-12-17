using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lesson2
{
    internal class ThreadControl
    {
        public AutoResetEvent WaitHandle { get; }

        public char[,] Field { get; }

        public ThreadControl(char[,] field, AutoResetEvent waitHandle)
        {
            Field = field;
            WaitHandle = waitHandle;
        }
    }
}
