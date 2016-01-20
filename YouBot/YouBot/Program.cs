using System;

namespace YouBot
{
    class Program
    {
        static int port;
        static int clientID;
        static int[] wheelJoints    = { -1, -1, -1, -1 };
        static int[] armJoints      = { -1, -1, -1, -1, -1 };
        static int youBot = -1;

        static float[] wheelPos = { -1, -1, -1, -1 };
        static float[] armPos   = { -1, -1, -1, -1, -1 };

        static float[] desiredWheelPos  = { -1, -1, -1, -1 };
        static float[] desiredArmPos    = { -1, -1, -1, -1, -1 };

        static void Main(string[] args)
        {
            Init();

            Console.ReadLine();
            
            // Test move

            GetJointPosition();
            
            while(true) Move();

            Console.ReadLine();
        }

        private static void Move()
        {
            GetJointPosition();

            CalcMove();

            SetJointPosition();
        }

        private static void CalcMove()
        {
            ConsoleKeyInfo c = Console.ReadKey();
            switch (c.KeyChar)
            {
                case ('w'):
                    {
                        armPos[0] += (float)Math.PI / 180;
                    }
                    break;
                case ('s'):
                    {
                        armPos[1] += (float)Math.PI / 180;
                    }
                    break;
                case ('d'):
                    {
                        armPos[2] += (float)Math.PI / 180;
                    }
                    break;
                case ('e'):
                    {
                        armPos[3] += (float)Math.PI / 180;
                    }
                    break;
                case ('r'):
                    {
                        armPos[3] += (float)Math.PI / 180;
                    }
                    break;
            }

        }

        private static void GetJointPosition()
        {

            for (int i = 0; i < 4; ++i)
            {
                vrepLib.simxGetJointPosition(clientID, armJoints[i],    ref armPos[i],      simx_opmode.oneshot_wait);
                vrepLib.simxGetJointPosition(clientID, wheelJoints[i],  ref wheelPos[i],    simx_opmode.oneshot_wait);
            }
            
            vrepLib.simxGetJointPosition(clientID, armJoints[4], ref armPos[4], simx_opmode.oneshot_wait);  
        }

        static void SetJointPosition()
        {
            for (int i = 0; i < 4; ++i)
            {
                vrepLib.simxSetJointPosition(clientID, wheelJoints[i], wheelPos[i], simx_opmode.oneshot_wait);
                vrepLib.simxSetJointPosition(clientID, armJoints[i], armPos[i], simx_opmode.oneshot_wait);
            }

            vrepLib.simxSetJointPosition(clientID, armJoints[4], armPos[4], simx_opmode.oneshot_wait);

        }

        static void Init()
        {
            port = 20100;   // VREP connection port

            clientID = vrepLib.simxStart("127.0.0.1", port, true, true, 5000, 5);

            if (clientID != -1)
            {
                Console.WriteLine(" - Success on simx.start");

                GetHandle(clientID, "youBot", out youBot);

                GetHandle(clientID, "rollingJoint_fl", out wheelJoints[0]);
                GetHandle(clientID, "rollingJoint_rl", out wheelJoints[1]);
                GetHandle(clientID, "rollingJoint_rr", out wheelJoints[2]);
                GetHandle(clientID, "rollingJoint_fr", out wheelJoints[3]);

                for (int i = 0; i < 5; ++i)
                {
                    GetHandle(clientID, "youBotArmJoint" + i.ToString(), out armJoints[i]);
                    
                }

            }
            else
            {
                Console.WriteLine(" - Error on simx.start");
                vrepLib.simxFinish(clientID);
            }
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
