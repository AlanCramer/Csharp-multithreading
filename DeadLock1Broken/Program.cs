using DeadLock1Broken;
using System;
using System.Threading;

class Program
{
    static void Transfer(Account from, Account to, int amount)
    {
        lock (from.LockObject)
        {
            Thread.Sleep(100); // simulate work
            lock (to.LockObject)
            {
                from.Debit(amount);
                to.Credit(amount);
            }
        }
    }



    static void Main()
    {
        Console.WriteLine("DeadlockBroken: transfer between two accounts with inconsistent lock ordering.");

        var ameliaAcct = new Account(1000);
        var kevinAcct = new Account(1000);

        var t1 = Task.Run(() => { Transfer(ameliaAcct, kevinAcct, 75); });
        var t2 = Task.Run(() => { Transfer(kevinAcct, ameliaAcct, 50); });

        Task.WaitAll(t1, t2);

        Console.WriteLine("Finished Transfers!");
    }
}
