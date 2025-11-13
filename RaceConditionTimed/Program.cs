using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    const int PerThreadIterations = 5_000_000;

    static void Main()
    {
        RunInMain();
        RunWithLock();
        RunWithInterlocked();
    }

    static void RunInMain()
    {
        int counter = 0;
        var sw = Stopwatch.StartNew();
        for (int i = 0; i < PerThreadIterations * 2; i++) counter++;
        sw.Stop();
        Console.WriteLine($"Main:       expected={PerThreadIterations * 2}, actual={counter}, time={sw.ElapsedMilliseconds} ms");
    }

    static void RunWithLock()
    {
        int counter = 0;
        object gate = new object();
        var t1 = new Thread(() => { for (int i = 0; i < PerThreadIterations; i++) lock (gate) counter++; });
        var t2 = new Thread(() => { for (int i = 0; i < PerThreadIterations; i++) lock (gate) counter++; });

        var sw = Stopwatch.StartNew();
        t1.Start(); t2.Start();
        t1.Join(); t2.Join();
        sw.Stop();

        Console.WriteLine($"lock:       expected={PerThreadIterations * 2}, actual={counter}, time={sw.ElapsedMilliseconds} ms");
    }

    static void RunWithInterlocked()
    {
        int counter = 0;
        var t1 = new Thread(() => { for (int i = 0; i < PerThreadIterations; i++) Interlocked.Increment(ref counter); });
        var t2 = new Thread(() => { for (int i = 0; i < PerThreadIterations; i++) Interlocked.Increment(ref counter); });

        var sw = Stopwatch.StartNew();
        t1.Start(); t2.Start();
        t1.Join(); t2.Join();
        sw.Stop();

        Console.WriteLine($"Interlocked: expected={PerThreadIterations * 2}, actual={counter}, time={sw.ElapsedMilliseconds} ms");
    }
}
