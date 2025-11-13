using System;
using System.Threading;

class Program
{
    static void Main()
    {
        int counter = 0;
        object gate = new object();

        var t1 = new Thread(() =>
        {
            for (int i = 0; i < 500_000; i++)
                lock (gate)
                {
                    counter++;
                }
        });

        var t2 = new Thread(() =>
        {
            for (int i = 0; i < 500_000; i++)
                lock (gate)
                {
                    counter++;
                }
        });

        t1.Start();
        t2.Start();
        t1.Join();
        t2.Join();

        Console.WriteLine($"Expected: 1000000, Actual: {counter}");
        
    }
}