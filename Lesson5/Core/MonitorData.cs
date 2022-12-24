namespace Lesson5.Core
{
    internal class MonitorData
    {
        public MonitorData(string descr, int cpu, int ram)
        {
            MonitoredBy = descr;
            CPU = cpu; 
            RAM = ram;
        }

        public string MonitoredBy { get; set; }
        
        public int CPU { get; }

        public int RAM { get; }
    }
}
