using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    const int N = 10_000_000;

    static void Main()
    {
        int[] data = new int[N];
        for (int i = 0; i < N; i++) data[i] = i;

        SingleThreaded(data);
        MultiThreaded(data);
    }

    static void SingleThreaded(int[] data)
    {
        var sw = Stopwatch.StartNew();

        long sum = 0;
        for (int i = 0; i < data.Length; i++)
            sum += SlowFunc(data[i]);

        sw.Stop();
        Console.WriteLine($"Single-threaded: {sw.ElapsedMilliseconds} ms, sum={sum}");
    }

    static void MultiThreaded(int[] data)
    {
        var sw = Stopwatch.StartNew();

        long sum1 = 0, sum2 = 0;

        var t1 = new Thread(() =>
        {
            for (int i = 0; i < data.Length / 2; i++)
                sum1 += SlowFunc(data[i]);
        });

        var t2 = new Thread(() =>
        {
            for (int i = data.Length / 2; i < data.Length; i++)
                sum2 += SlowFunc(data[i]);
        });

        t1.Start();
        t2.Start();
        t1.Join();
        t2.Join();

        long total = sum1 + sum2;

        sw.Stop();
        Console.WriteLine($"Two-thread:     {sw.ElapsedMilliseconds} ms, sum={total}");
    }

    static int SlowFunc(int x)
    {
        // Fake CPU work
        for (int i = 0; i < 100; i++)
            x = x * 1664525 + 1013904223;
        return x;
    }
}
