using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeadLock1Broken
{
    internal class Account
    {
        private int balance;
        public Account(int initialBalance) { balance = initialBalance; }

        public void Debit(int amount) { balance -= amount; }
        public void Credit(int amount) { balance += amount; }

        public object LockObject { get; } = new object();
    }

}
