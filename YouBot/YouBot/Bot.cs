
using System;

namespace YouBot
{
    class Bot
    {
        public int clientID;                        

        private Platform platform;
        private Arm arm;
        private Gripper gripper;
        private SensSys sensSys;

        public Bot(int port)
        {
            Init(port);

            if(clientID != -1)
            {
                platform    = new Platform(clientID);
                arm         = new Arm(clientID);
                gripper     = new Gripper(clientID);
                sensSys      = new SensSys(clientID);
            }            
        }        

        public void Init(int port)
        {

            clientID = vrepLib.simxStart("127.0.0.1", port, true, true, 5000, 5);

            if (clientID != -1)
            {
                Console.WriteLine(" - Success on simx.start");
            }
            else
            {
                Console.WriteLine(" - Error on simx.start");
                vrepLib.simxFinish(clientID);
            }
        }

        public void Move()
        {
            sensSys.Scan();
            bool findTrigger = sensSys.find[0]||sensSys.find[1]||sensSys.find[2]||sensSys.find[3];
            if (findTrigger)
            {
                platform.Stop();
                return;
            }
            else platform.Forward();

            //KeyboardControl();
        }

        private void KeyboardControl()
        {
            ConsoleKeyInfo c = Console.ReadKey();
            switch (c.KeyChar)
            {
                case ('w'):
                    {
                       platform.Forward();
                    }
                    break;
                case ('s'):
                    {
                        platform.Stop();
                    }
                    break;
                case ('x'):
                    {
                        platform.Backward();
                    }
                    break;
                case ('q'):
                    {
                        platform.RotateCounterclockwise();
                    }
                    break;
                case ('e'):
                    {
                        platform.RotateClockwise();
                    }
                    break;
                case ('d'):
                    {
                        platform.Right();
                    }
                    break;
                case ('a'):
                    {
                        platform.Left();
                    }
                    break;

                case ('r'):
                    {
                        arm.joint0.Backward();
                    }
                    break;

                case ('f'):
                    {
                        arm.joint0.Forward();
                    }
                    break;
                case ('t'):
                    {
                        arm.joint1.Backward();
                    }
                    break;

                case ('g'):
                    {
                        arm.joint1.Forward();
                    }
                    break;
                case ('y'):
                    {
                        arm.joint2.Backward();
                    }
                    break;

                case ('h'):
                    {
                        arm.joint2.Forward();
                    }
                    break;

                case ('u'):
                    {
                        arm.joint3.Backward();
                    }
                    break;

                case ('j'):
                    {
                        arm.joint3.Forward();
                    }
                    break;

                case ('i'):
                    {
                        arm.joint4.Backward();
                    }
                    break;

                case ('k'):
                    {
                        arm.joint4.Forward();
                    }
                    break;

                case ('p'):
                    {
                        gripper.Open();
                    }
                    break;

            }

        }

    }
}
