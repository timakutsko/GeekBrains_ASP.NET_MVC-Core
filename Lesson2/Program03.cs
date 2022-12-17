using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lesson2
{
    internal class Program03
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
            
            AutoResetEvent waitHandlerFarmer = new AutoResetEvent(true);
            AutoResetEvent waitHandlerPrinter = new AutoResetEvent(true);

            FieldPrinter(waitHandlerPrinter);
            
            Thread threadX = new Thread(() =>
            {
                waitHandlerFarmer.WaitOne();
                FarmerX(waitHandlerFarmer, waitHandlerPrinter);
            });

            Thread threadY = new Thread(() =>
            {
                waitHandlerFarmer.WaitOne();
                FarmerY(waitHandlerFarmer, waitHandlerPrinter);
            });

            threadX.Start();
            threadY.Start();
        }

        static void FieldPrinter(AutoResetEvent waitHandler)
        {
            waitHandler.WaitOne();
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
            waitHandler.Set();
        }

        static void FarmerX(AutoResetEvent waitHandler, AutoResetEvent waitHandlerPrinter)
        {
            for (int i = 0; i < _field.GetLength(0); i++)
            {
                for (int j = 0; j < _field.GetLength(1); j++)
                {
                    if (_field[i, j] == '.')
                    {
                        _field[i, j] = 'X';
                        Thread.Sleep(100);
                        waitHandler.Set();
                        FieldPrinter(waitHandlerPrinter);
                    }
                }
            }
        }

        static void FarmerY(AutoResetEvent waitHandler, AutoResetEvent waitHandlerPrinter)
        {
            for (int i = _field.GetLength(1) - 1; i > 0; i--)
            {
                for (int j = _field.GetLength(0) - 1; j > 0; j--)
                {
                    if (_field[j, i] == '.')
                    {
                        _field[j, i] = 'Y';
                        Thread.Sleep(200);
                        waitHandler.Set();
                        FieldPrinter(waitHandlerPrinter);
                    }
                }
            }
        }
    }
}
