using System;
using System.Linq;
using System.Threading;

namespace Lesson2
{

    // X X X . . .     X X X X X .     X X X X X X     X X X X X X     
    // . . . . . .     . . . . . .     X. . . . .      X X X. . .
    // . . . . . .     . . . . . .     . . . . . .     . . . . . .
    // . . . . . .     . . . . . .     . . . . . .     . . . . . .

    // . . . . . .     . . . . . 0     . . . . . 0     . . . . . 0     
    // . . . . . 0     . . . . . 0     . . . . . 0     . . . . 0 0     
    // . . . . . 0     . . . . . 0     . . . . 0 0     . . . . 0 0     
    // . . . . . 0     . . . . . 0     . . . . 0 0     . . . . 0 0

    internal class Program
    {
        private static char[,] _field;
        private static int _x;
        private static int _y;

        static void Main(string[] args)
        {
            Console.Write("Укажите размер поля по оси X: ");
            _x = Convert.ToInt32(Console.ReadLine());
            
            Console.Write("Укажите размер поля по оси Y: ");
            _y = Convert.ToInt32(Console.ReadLine());

            _field = new char[_y, _x];
            for (int i = 0; i < _field.GetLength(0); i++)
            {
                if (i != 0)
                    Console.WriteLine();

                for (int j = 0; j < _field.GetLength(1); j++)
                {
                    _field[i, j] = '.';
                }
            }
            FieldPrinter();

            AutoResetEvent[] waitHandles = new AutoResetEvent[2]
            {
                new AutoResetEvent(false),
                new AutoResetEvent(false),
            };
            ThreadPool.QueueUserWorkItem(new WaitCallback(TaskFarmerX), new ThreadControl(_field, waitHandles[0]));
            ThreadPool.QueueUserWorkItem(new WaitCallback(TaskFarmerY), new ThreadControl(_field, waitHandles[1]));

            WaitHandle.WaitAll(waitHandles);
            Console.WriteLine("\nПоле засеяли");
            Console.ReadLine();
        }

        static void FieldPrinter()
        {
            Console.Clear();

            for (int i = 0; i < _field.GetLength(0); i++)
            {
                if (i != 0)
                    Console.WriteLine();

                for (int j = 0; j < _field.GetLength(1); j++)
                {
                    Console.Write(_field[i, j]);
                }
            }
        }

        static void TaskFarmerX(object obj)
        {
            if (obj != null && obj is ThreadControl)
            {
                var threadControl = (ThreadControl)obj;

                for (int i = 0; i < threadControl.Field.GetLength(0); i++)
                {
                    for (int j = 0; j < threadControl.Field.GetLength(1); j++)
                    {
                        if (_field[i, j] == '.')
                        {
                            lock (threadControl.Field)
                            {
                                _field[i, j] = 'X';
                                FieldPrinter();
                            }
                            Thread.Sleep(100);
                        }
                    }
                }

                threadControl.WaitHandle.Set();
            }
        }

        static void TaskFarmerY(object obj)
        {
            if (obj != null && obj is ThreadControl)
            {
                var threadControl = (ThreadControl)obj;

                for (int i = threadControl.Field.GetLength(1) - 1; i > 0; i--)
                {
                    for (int j = threadControl.Field.GetLength(0) - 1; j > 0; j--)
                    {
                        if (_field[j, i] == '.')
                        {
                            lock (threadControl.Field)
                            {
                                _field[j, i] = 'Y';
                                FieldPrinter();
                            }
                            Thread.Sleep(200);
                        }
                    }
                }

                threadControl.WaitHandle.Set();
            }
        }
    }
}
