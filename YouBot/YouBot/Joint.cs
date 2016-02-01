using System;

namespace YouBot
{
    class Joint
    {
        int clientID;
        int handler;
        public float position;

        private float step = 0.01f;

        public Joint(int id, int handler)
        {
            this.clientID = id;
            this.handler  = handler;

            GetPosition();
        }

        public void GetPosition()
        {
            float pos = 0;

            simx_error e = simx_error.novalue_flag;
            
            while(e != simx_error.noerror)
                e = vrepLib.simxGetJointPosition(clientID, handler, ref pos, simx_opmode.oneshot_wait);            
        }

        public void Move(float value)
        {
            GetPosition();
            float newPos = position + value;
            Console.WriteLine(position + "\t" + newPos);
            vrepLib.simxSetJointPosition(clientID, handler, newPos, simx_opmode.oneshot);

            position = newPos;
        }        

        public void Backward()
        {
            Move(-step);
        }

        public void Forward()
        {
            Move(step);
        }
    }
}
