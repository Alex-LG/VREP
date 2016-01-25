using System;
using System.Threading;

namespace YouBot
{
    class Program
    {

        const int PORT = 20100;

        static int port;

        static void Main(string[] args)
        {
            Bot bot = new Bot(PORT);
            
            if( bot.clientID != -1 )
                while (true)
                {
                    bot.Move();
                }
          

        }


    }
}