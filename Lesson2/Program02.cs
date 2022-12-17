using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lesson2
{
    internal class Program02
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

            Thread threadX = new Thread(TaskFarmerX);
            Thread threadY = new Thread(TaskFarmerY);
            
            threadX.Start(_field);
            threadY.Start(_field);
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
            if (obj != null && obj is Array)
            {
                var array = (Array)obj;

                for (int i = 0; i < array.GetLength(0); i++)
                {
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        if (_field[i, j] == '.')
                        {
                            lock (array)
                            {
                                _field[i, j] = 'X';
                                FieldPrinter();
                            }
                            Thread.Sleep(100);
                        }
                    }
                }
            }
        }

        static void TaskFarmerY(object obj)
        {
            if (obj != null && obj is Array)
            {
                var array = (Array)obj;

                for (int i = array.GetLength(1) - 1; i > 0; i--)
                {
                    for (int j = array.GetLength(0) - 1; j > 0; j--)
                    {
                        if (_field[j, i] == '.')
                        {
                            lock (array)
                            {
                                _field[j, i] = 'Y';
                                FieldPrinter();
                            }
                            Thread.Sleep(200);
                        }
                    }
                }
            }
        }
    }
}
