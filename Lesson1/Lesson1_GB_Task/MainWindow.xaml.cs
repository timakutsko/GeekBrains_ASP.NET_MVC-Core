using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace Lesson1_GB_Task
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _fibNumber = 41;

        private int _tempFibNumber = 0;

        private double _tempFibTime = 0;

        private string _nextFibTime = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
        }

        private int Fibonachi(int n)
        {
            if (n == 0 || n == 1) return n;
            return Fibonachi(n - 1) + Fibonachi(n - 2);
        }

        private void DisplayCurrentFib(int n)
        {
            Application currApp = Application.Current;
            if (currApp == null) return;

            _ = currApp.Dispatcher.BeginInvoke(
            new Action(() =>
            {
                tblDescr.Text = $"Число Фибоначчи #{_fibNumber}:";
                tblFib.Text = n.ToString();
            }));
        }

        private void DisplayNextFib(int n)
        {
            Application currApp = Application.Current;
            if (currApp == null) return;

            _ = currApp.Dispatcher.BeginInvoke(
            new Action(() =>
            {
                tblNextDescr.Text = $"Время для расчета числа #{n}:";
                tblTime.Text = _nextFibTime;
                btnNextFib.IsEnabled = true;
            }));
        }

        private void OnNextFibClick(object sender, EventArgs e)
        {
            btnNextFib.IsEnabled = false;

            if (_tempFibNumber == 0)
            {
                Thread thread1 = new Thread((obj) =>
                {
                    DateTime start = DateTime.Now;
                    int fibNumber = Fibonachi((int)obj);
                    DateTime finish = DateTime.Now;
                    _tempFibTime = (finish - start).TotalSeconds;

                    DisplayCurrentFib(fibNumber);
                });
                thread1.Start(_fibNumber);
            }
            else
            {
                DisplayCurrentFib(_tempFibNumber);
            }
            
            _fibNumber++;

            Thread thread2 = new Thread((obj) =>
            {
                DateTime start = DateTime.Now;
                _tempFibNumber = Fibonachi((int)obj);
                DateTime finish = DateTime.Now;
                _tempFibTime = _tempFibTime + (finish - start).TotalSeconds;
                _nextFibTime = $"{Math.Round(_tempFibTime, 5)} с";

                DisplayNextFib(_fibNumber + 1);
            });
            thread2.Start(_fibNumber);
        }
    }
}
