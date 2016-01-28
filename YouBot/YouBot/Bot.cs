
using System;
using System.Threading;

namespace YouBot
{
    public enum CONTROL_TYPE
    {
        MANUAL,
        AUTOMATIC
    };

    class Bot
    {       
        public int clientID;
        //private string suffix;
        
        private Platform platform;
        private Arm arm;
        private Gripper gripper;
        private SensSys sensSys;

        public Bot(int port, string suffix)
        {
            Init(port);

            if(clientID != -1)
            {
                platform    = new Platform(clientID, suffix);
                arm = new Arm(clientID, suffix);
                gripper = new Gripper(clientID, suffix);
                sensSys = new SensSys(clientID, suffix);
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

        public void MoveAutomatic()
        {
            Thread tSens  = new Thread(sensSys.loop);
            Thread tPlatf = new Thread(platform.loop);

            tSens.Start();
            tPlatf.Start();
        }



        public void MoveManual()
        {
            Thread tSens = new Thread(sensSys.loop);
            Thread tPlatf = new Thread(platform.loop);
            Thread tControl = new Thread(KeyboardControl);

            tSens.Start();
            tPlatf.Start();  
            tControl.Start();
                        
        }

        private void platformAlgorithm()
        {
            

            bool findTrigger = sensSys.find[0] || sensSys.find[1] || sensSys.find[2] || sensSys.find[3];
            if (findTrigger)
            {
                platform.Stop();
                return;
            }
            else platform.Right();
        }

        private void armAlgorithm()
        {


        }

        
        private void KeyboardControl()
        {
            while(true)
            {
                ConsoleKeyInfo c = Console.ReadKey();
                switch (c.KeyChar)
                {
                    case ('w'):
                        {
                            platform.ChangeState(PLATFORM_STATE.FORWARD);
                        }
                        break;
                    case ('s'):
                        {
                            platform.ChangeState(PLATFORM_STATE.STOP);
                        }
                        break;
                    case ('x'):
                        {
                            platform.ChangeState(PLATFORM_STATE.BACKWARD);
                        }
                        break;
                    case ('q'):
                        {
                            platform.ChangeState(PLATFORM_STATE.ROTATE_COUNTERCLOCKWISE);
                        }
                        break;
                    case ('e'):
                        {
                            platform.ChangeState(PLATFORM_STATE.ROTATE_CLOCKWISE);
                        }
                        break;
                    case ('d'):
                        {
                            platform.ChangeState(PLATFORM_STATE.RIGHT);
                        }
                        break;
                    case ('a'):
                        {
                            platform.ChangeState(PLATFORM_STATE.LEFT);
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
}
