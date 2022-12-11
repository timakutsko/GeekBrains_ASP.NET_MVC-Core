using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lesson2
{
    public class MyClass
    {
        public string Name { get; set; }

        public bool IsUpdate { get; set; }

        public int UpdateCount { get; set; }

        public MyClass(string name)
        {
            Name = name;
            IsUpdate = false;
            UpdateCount = 0;
        }
    }

    internal class UpdateListItems_Test
    {
        static void Main(string[] args)
        {
            List<MyClass> list = new List<MyClass>();
            for (int i = 0; i < 200; i++)
            {
                list.Add(new MyClass($"Элемент {i}"));
            }

            DateTime start = DateTime.Now;

            AutoResetEvent[] waitHandlers = new AutoResetEvent[5];
            for (int i = 0; i < waitHandlers.Length; i++)
            {
                waitHandlers[i] = new AutoResetEvent(false);
            }

            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread((o) =>
                {
                    MainTask(list, Thread.CurrentThread.ManagedThreadId);
                    ((AutoResetEvent)o).Set();
                });
                threads[i].Start(waitHandlers[i]);
            }

            WaitHandle.WaitAll(waitHandlers);

            foreach (MyClass currentInst in list)
            {
                Console.WriteLine($"{currentInst.Name}");
            }

            DateTime finish = DateTime.Now;
            Console.WriteLine($"End Task1. Work time: {(finish - start).TotalSeconds} s");

            static void MainTask(List<MyClass> list, int numb)
            {
                foreach (MyClass currentInst in list)
                {

                    if (Monitor.TryEnter(currentInst))
                    {
                        if (!currentInst.IsUpdate)
                        {
                            currentInst.Name = $"{currentInst.Name} update by {numb}. Update count: {++currentInst.UpdateCount}";
                            currentInst.IsUpdate = true;
                            Thread.Sleep(100);
                        }

                        Monitor.Exit(currentInst);
                    }
                }
            }
        }
    }
}
