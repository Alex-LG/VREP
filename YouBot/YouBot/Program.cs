using System;
using System.Threading;

namespace YouBot
{
    class Program
    {
        static int port;
        static int clientID;
        static int[] wheelJoints    = { -1, -1, -1, -1 };
        static int[] armJoints      = { -1, -1, -1, -1, -1 };
        static int youBot = -1;

        static int[] gripperJoints = { -1, -1 };

        static bool gripperOpen = false;


        static float[] armPos = { -1, -1, -1, -1, -1 };

        static void Main(string[] args)
        {
            Init();

            // Test move
            vrepLib.simxSetJointTargetVelocity(clientID, gripperJoints[0], 0.04f, simx_opmode.oneshot_wait);
            vrepLib.simxSetJointTargetVelocity(clientID, gripperJoints[1], 0.04f, simx_opmode.oneshot_wait);
            GetJointPosition();
            while (true)
            {
                Move();
                Thread.Sleep(20);
            }

           
        }

        private static void Move()
        {
            GetJointPosition();

            KeyboardControl();
           
            
        }

        private static void Forward()
        {
            for (int i = 0; i < 4; ++i)
                vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[i], (float)Math.PI, simx_opmode.oneshot_wait);        
        }

        private static void Stop()
        {
            for(int i = 0; i<4;++i)
                vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[i], 0, simx_opmode.oneshot_wait);                                                 
        }

        private static void Backward()
        {
            for (int i = 0; i < 4; ++i)
                vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[i], -(float)Math.PI, simx_opmode.oneshot_wait);          
        }

        private static void RotateCounterclockwise()
        {
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[0], (float)Math.PI, simx_opmode.oneshot_wait);
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[1], (float)Math.PI, simx_opmode.oneshot_wait);
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[2], -(float)Math.PI, simx_opmode.oneshot_wait);
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[3], -(float)Math.PI, simx_opmode.oneshot_wait);           
        }

        private static void RotateClockwise()
        {
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[0], -(float)Math.PI, simx_opmode.oneshot_wait);
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[1], -(float)Math.PI, simx_opmode.oneshot_wait);
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[2], (float)Math.PI, simx_opmode.oneshot_wait);
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[3], (float)Math.PI, simx_opmode.oneshot_wait);
        }

        private static void Left()
        {
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[0], -(float)Math.PI, simx_opmode.oneshot_wait);
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[1], (float)Math.PI, simx_opmode.oneshot_wait);
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[2], -(float)Math.PI, simx_opmode.oneshot_wait);
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[3], (float)Math.PI, simx_opmode.oneshot_wait);
        }

        private static void Right()
        {
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[0], (float)Math.PI, simx_opmode.oneshot_wait);
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[1], -(float)Math.PI, simx_opmode.oneshot_wait);
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[2], (float)Math.PI, simx_opmode.oneshot_wait);
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[3], -(float)Math.PI, simx_opmode.oneshot_wait);
        }

        private static void ArmJoint0Forward()
        {
            armPos[0] += (float)Math.PI / 90.0f;
            vrepLib.simxSetJointPosition(clientID, armJoints[0], armPos[0], simx_opmode.oneshot_wait);        
        }

        private static void ArmJoint0Backward()
        {
            armPos[0] -= (float)Math.PI / 90.0f;
            vrepLib.simxSetJointPosition(clientID, armJoints[0], armPos[0], simx_opmode.oneshot_wait);        
        }

        private static void KeyboardControl()
        {
            ConsoleKeyInfo c = Console.ReadKey();
            switch (c.KeyChar)
            {
                case ('w'):
                    {
                        Forward();
                    }
                    break;
                case ('s'):
                    {
                        Stop();              
                    }
                    break;
                case ('x'):
                    {
                        Backward();
                    }
                    break;
                case ('q'):
                    {
                        RotateCounterclockwise();
                    }
                    break;
                case ('e'):
                    {
                        RotateClockwise();
                    }
                    break;
                case ('d'):
                    {
                        Right();
                    }
                    break;
                case ('a'):
                    {
                        Left();
                    }
                    break;

                case ('r'):
                    {
                        ArmJoint0Backward();
                    }
                    break;

                case ('f'):
                    {
                        ArmJoint0Forward();                        
                    }
                    break;
                case ('t'):
                    {
                        ArmJoint1Backward();
                    }
                    break;

                case ('g'):
                    {
                        ArmJoint1Forward(); 
                    }
                    break;
                case ('y'):
                    {
                        ArmJoint2Backward();
                    }
                    break;

                case ('h'):
                    {
                        ArmJoint2Forward(); 
                    }
                    break;

                case ('u'):
                    {
                        ArmJoint3Backward();
                    }
                    break;

                case ('j'):
                    {
                        ArmJoint3Forward();
                    }
                    break;

                case ('i'):
                    {
                        ArmJoint4Backward(); 
                    }
                    break;

                case ('k'):
                    {
                       ArmJoint4Forward(); 
                    }
                    break;

                case ('p'):
                    {
                        GripperOpen();
                    }
                    break;

            }

        }

        private static void GripperOpen()
        {
            if (gripperOpen)
            {

                vrepLib.simxSetJointTargetVelocity(clientID, gripperJoints[1], 0.04f, simx_opmode.oneshot_wait);

                gripperOpen = false;
            }
            else
            {

                vrepLib.simxSetJointTargetVelocity(clientID, gripperJoints[1], -0.04f, simx_opmode.oneshot_wait);

                gripperOpen = true;
            }
            float gripperPos0 = 0;
            vrepLib.simxSetJointTargetPosition(clientID, gripperJoints[0], (float)vrepLib.simxGetJointPosition(clientID, gripperJoints[1], ref gripperPos0, simx_opmode.oneshot_wait) - 0.5f, simx_opmode.oneshot_wait);
        }

        private static void ArmJoint4Backward()
        {
            armPos[4] -= (float)Math.PI / 90.0f;
            vrepLib.simxSetJointPosition(clientID, armJoints[4], armPos[4], simx_opmode.oneshot_wait);
        }

        private static void ArmJoint4Forward()
        {
            armPos[4] += (float)Math.PI / 90.0f;
            vrepLib.simxSetJointPosition(clientID, armJoints[4], armPos[4], simx_opmode.oneshot_wait);
        }

        private static void ArmJoint3Forward()
        {
            armPos[3] += (float)Math.PI / 90.0f;
            vrepLib.simxSetJointPosition(clientID, armJoints[3], armPos[3], simx_opmode.oneshot_wait);
        }

        private static void ArmJoint3Backward()
        {
            armPos[3] -= (float)Math.PI / 90.0f;
            vrepLib.simxSetJointPosition(clientID, armJoints[3], armPos[3], simx_opmode.oneshot_wait);
        }

        private static void ArmJoint2Forward()
        {
            armPos[2] += (float)Math.PI / 90.0f;
            vrepLib.simxSetJointPosition(clientID, armJoints[2], armPos[2], simx_opmode.oneshot_wait);
        }

        private static void ArmJoint2Backward()
        {
            armPos[2] -= (float)Math.PI / 90.0f;
            vrepLib.simxSetJointPosition(clientID, armJoints[2], armPos[2], simx_opmode.oneshot_wait);
        }

        private static void ArmJoint1Forward()
        {
            armPos[1] += (float)Math.PI / 90.0f;
            vrepLib.simxSetJointPosition(clientID, armJoints[1], armPos[1], simx_opmode.oneshot_wait);
        }

        private static void ArmJoint1Backward()
        {
            armPos[1] -= (float)Math.PI / 90.0f;
            vrepLib.simxSetJointPosition(clientID, armJoints[1], armPos[1], simx_opmode.oneshot_wait);
        }

        private static void GetJointPosition()
        {

            for (int i = 0; i < 5; ++i)
            {
                vrepLib.simxGetJointPosition(clientID, armJoints[i],    ref armPos[i],      simx_opmode.oneshot_wait);
            }

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

                GetHandle(clientID, "youBotGripperJoint1", out gripperJoints[0]);
                GetHandle(clientID, "youBotGripperJoint2", out gripperJoints[1]);
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