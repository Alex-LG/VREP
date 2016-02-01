
using System;
using System.Threading;


namespace YouBot
{
    // public delegate void ConsoleSignal(ConsoleKeyInfo c);

    class Program
    {
        // public static event ConsoleSignal Input;

        const int PORT0 = 20100;
        const int PORT1 = 20200;

        static void Main(string[] args)
        {
            Bot bot0 = new Bot(PORT0, "");
//          Bot bot1 = new Bot(PORT1, "#0");


            Thread thread0 = new Thread(bot0.Loop);
//          Thread thread1 = new Thread(bot1.MoveManual);

            thread0.Start();
        }
    }
}