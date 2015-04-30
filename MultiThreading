using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MultiThreading
{
    class Program
    {
        //showiung multithreading - there are three threads -the thread starting in main, then thread obj one and 2
        //if threads are set to background they end as soon as the main thread closes.
        static void Main(string[] args)
        {
            Thread threadObj1 = new Thread(Function1);
            Thread threadObj2 = new Thread(Function2);

            //threadObj1.IsBackground = true; //as background threads when the main thread ends these threads are cut off
            //threadObj2.IsBackground = true;
            //invoking the threads
            threadObj1.Start(); //this thread starts first
            threadObj2.Start();

            Console.WriteLine("exiting");
           
        }

        private static void Function1(object obj)
        {
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine(i.ToString());
                Thread.Sleep(2500);
            }
        }

        private static void Function2(object obj)
        {
            for (int i = 20; i < 40; i++)
            {
                Console.WriteLine(i.ToString());
                Thread.Sleep(2500); //pauses this thread while other thread starts
            }
        }
    }
}
