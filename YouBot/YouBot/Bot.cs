
using System;
using System.Threading;

namespace YouBot
{
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

        public void Loop()
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
            while ( sensSys.ObjectDetected() == SENSOR_DIRECTION.NOVALUE )
            {                
                platform.ChangeState(PLATFORM_STATE.BACKWARD);
            }

            while (sensSys.back.Distance() > 0.22f)
            {

                
                platform.ChangeState(PLATFORM_STATE.BACKWARD);
                Thread.Sleep(1);
            }

            platform.ChangeState(PLATFORM_STATE.STOP);            

            armAlgorithm();

            KeyboardControl();
        }

        private void armAlgorithm()
        {
            while (arm.joint1.position > -1.2f)
            {                
                arm.joint1.Backward();
            }

            while (arm.joint2.position > -0.9f)
            {
                arm.joint2.Backward();
            }

            while (arm.joint3.position > -0.95f)
            {
                arm.joint3.Backward();
            }
           
            gripper.Open();
            Thread.Sleep(1000);
            gripper.Open();
            Thread.Sleep(1000);
            
            while (arm.joint2.position < 1f)
            {
                arm.joint2.Forward();
            }
            while (arm.joint3.position < 1f)
            {
                arm.joint3.Forward();
            }
            while (arm.joint1.position < 1f)
            {
                arm.joint1.Forward();
            }

            gripper.Open();
            Thread.Sleep(1000);            
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
