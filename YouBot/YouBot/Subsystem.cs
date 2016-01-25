
using System;

namespace YouBot
{
    abstract class Subsystem
    {
        public void GetHandle(int id, string objectName, out int handler)
        {
            Console.Write("Get handler for " + objectName + " : \t");
            vrepLib.simxGetObjectHandle(id, objectName, out handler, simx_opmode.oneshot_wait);
            if (-1 == handler)
                Console.WriteLine("Fail");
            else
                Console.WriteLine("OK");
        }
    }
}
