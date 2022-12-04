try
{
    Task1();
    Console.WriteLine("########################");
    Task2();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

static void Task1()
{
    DateTime start = DateTime.Now;
    Console.WriteLine("Start Task1");
    
    float[] arr = new float[100_000_000];
    Array.Fill(arr, 1);

    for(int i = 0; i < arr.Length; i++)
    {
        arr[i] = (float)(arr[i] * Math.Sin(0.2f + i / 5) * Math.Cos(0.2f + i / 5) * Math.Cos(0.4f + i / 2));
    }

    DateTime finish = DateTime.Now;
    Console.WriteLine($"Check items: 498 - {arr[498]}; 500 - {arr[500]}; 502 - {arr[502]}");
    Console.WriteLine($"End Task1. Work time: {(finish - start).TotalSeconds} s");
}

static void Task2()
{
    DateTime start = DateTime.Now;
    Console.WriteLine("Start Task2");
     
    float[] arr = new float[100_000_000];
    Array.Fill(arr, 1);

    float[] arr1 = new float[arr.Length / 2];
    Array.Copy(arr, arr1, arr.Length / 2);
    float[] arr2 = new float[arr.Length / 2];
    Array.Copy(arr, arr.Length / 2, arr2, 0, arr.Length / 2);

    AutoResetEvent autoResetEvent1 = new AutoResetEvent(false);
    Thread thread1 = new Thread((obj) =>
    {
        if (obj != null)
        {
            ChangeValue(obj);
        }
        autoResetEvent1.Set();
    });
    thread1.Start(arr1);

    AutoResetEvent autoResetEvent2 = new AutoResetEvent(false);
    Thread thread2 = new Thread((obj) =>
    {
        if (obj != null)
        {
            ChangeValue(obj);
        }
        autoResetEvent2.Set();
    });
    thread2.Start(arr2);

    WaitHandle.WaitAll(new WaitHandle[] 
    { 
        autoResetEvent1, 
        autoResetEvent2 
    });

    arr1.CopyTo(arr, 0);
    arr2.CopyTo(arr, arr.Length / 2 - 1);

    DateTime finish = DateTime.Now;
    Console.WriteLine($"Check items: 498 - {arr[498]}; 500 - {arr[500]}; 502 - {arr[502]}");
    Console.WriteLine($"End Task2. Work time: {(finish - start).TotalSeconds} s");
}

static float[] ChangeValue(object? obj)
{
    if (obj != null)
    {
        try
        {
            float[] arr = (float[])obj;
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = (float)(arr[i] * Math.Sin(0.2f + i / 5) * Math.Cos(0.2f + i / 5) * Math.Cos(0.4f + i / 2));
            }
            return arr;
        }
        catch (InvalidCastException)
        {
            throw new Exception("Not float array!");
        }
    }
    throw new Exception("Empty object input");
}