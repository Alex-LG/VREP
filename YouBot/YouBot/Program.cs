using System;


namespace YouBot
{
    class Program
    {
        static void Main(string[] args)
        {
            

            //System.Reflection.Assembly.LoadFrom("$(ProjectDir)\\lib");

            int port = 20100;   // VREP connection port

            int clientID = vrepLib.simxStart("127.0.0.1", port, true, true, 5000, 5);

            int[] wheelJoints = { -1, -1, -1, -1 };
            int[] armJoints = { -1, -1, -1, -1, -1 };
            int youBot = -1;

            if (clientID != -1)
            {
                Console.WriteLine(" - Success on simx.start");

                GetHandle(clientID, "youBot", out youBot);

                GetHandle(clientID, "rollingJoint_fl", out wheelJoints[0]);
                GetHandle(clientID, "rollingJoint_rl", out wheelJoints[1]);
                GetHandle(clientID, "rollingJoint_rr", out wheelJoints[2]);
                GetHandle(clientID, "rollingJoint_fr", out wheelJoints[3]);

                for (int i = 0; i < 4; ++i)
                {
                    GetHandle(clientID, "youBotArmJoint" + i.ToString(), out armJoints[i + 1]);
                }
            }
            else
            {
                Console.WriteLine(" - Error on simx.start");
                vrepLib.simxFinish(clientID);
            }

            Console.ReadLine();

            double[] desiredJ = { 0, 0, 0, 0, 0 };
            for (int i = 0; i < 5; ++i)
            {
                vrepLib.simxSetJointPosition(clientID, armJoints[i], (float)desiredJ[i], simx_opmode.oneshot_wait);
            }

            Console.ReadLine();
        }

        static void GetHandle(int id, string objectName, out int handler)
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
