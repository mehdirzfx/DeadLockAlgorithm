using System;
using System.Threading;

namespace DeadLock
{
    public class DeadLock
    {
        static readonly object firstLock = new object();
        static readonly object secondLock = new object();
        static void ThreadJob()
        {
            Console.WriteLine("\t\t\t\tLocking firstLock");
            lock (firstLock)
            {
                Console.WriteLine("\t\t\t\tLocked firstLock");
                // صبر میکند تا اولین ترد یا رشته وضعیتش مشخص شود
                //  .را گرفته است secondLock
                Thread.Sleep(1000);

                Console.WriteLine("\t\t\t\tLocking secondLock");
                lock (secondLock)
                {
                    Console.WriteLine("\t\t\t\tLocked secondLock");
                }
                Console.WriteLine("\t\t\t\tReleased secondLock");
            }
            Console.WriteLine("\t\t\t\tReleased firstLock");
        }
        static void Main()
        {
            new Thread(new ThreadStart(ThreadJob)).Start();
            // صبر میکند تا ترد های دیگر وضعیتشان مشخص شود.
            // .را گرفته است firstLock
            Thread.Sleep(500);

            Console.WriteLine("Locking secondLock");
            lock (secondLock)
            {
                Console.WriteLine("Locked secondLock");
                Console.WriteLine("Locking firstLock");
                lock (firstLock)
                {
                    Console.WriteLine("Locked firstLock");
                }
                Console.WriteLine("Released firstLock");
            }
            Console.WriteLine("Released secondLock");
            Console.Read();
        }
    }
}