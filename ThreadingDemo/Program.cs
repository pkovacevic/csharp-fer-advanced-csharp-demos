using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingDemo
{
    class Program
    {

        static void Main(string[] args)
        {
            Example1();

            Example2();
        }


        private static void Example1()
        {
            // Creates new threads that will do SomeOperation
            Task t1 = Task.Run(() => SomeOperation("A"));
            Task t2 = Task.Run(() => SomeOperation("B"));
            Task t3 = Task.Run(() => SomeOperation("C"));
            Task t4 = Task.Run(() => SomeOperation("D"));
            Task t5 = Task.Run(() => SomeOperation("E"));

            //All Task.Run methods return immediately.. 
            //They just start a new thread that will do the actual work

            //We can wait until Task finish using Task.WaitAll / Task.WaitAny
            //Task.WaitAll(t1, t2, t3, t4, t5);
            SomeOperation("Main");

            Console.Read();
        }


        private static void Example2()
        {
            // Task<T> -> promise that action will finish and return T result
            Task<int> t = Task.Run(() => SomeOperationThatReturnsInt(1));
            Task<int> t2 = Task.Run(() => SomeOperationThatReturnsInt(2));

            // Using wait before was not necessary, if we try to access the "Result" of a Task,
            // Main thread waits until results are ready (t and t2 finish execution)
            Console.WriteLine(t.Result + t2.Result);
            Console.Read();
        }


        static void SomeOperation(string taskName)
        {
            Console.WriteLine("Started {0} - from thread: {1}", taskName, Thread.CurrentThread.ManagedThreadId);
            //Simulate hard work
            Thread.Sleep(2000);
            Console.WriteLine("End {0} - from thread: {1}", taskName, Thread.CurrentThread.ManagedThreadId);
        }

        static int SomeOperationThatReturnsInt(int i)
        {
            //Simulate hard work
            Thread.Sleep(200);
            return i;
        }







    }
}
