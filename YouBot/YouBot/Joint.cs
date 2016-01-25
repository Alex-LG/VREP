using System;

namespace YouBot
{
    class Joint
    {
        int clientID;
        int handler;
        float position;

        public Joint(int id, int handler)
        {
            this.clientID = id;
            this.handler  = handler;
        }

        public void GetPosition()
        {
            vrepLib.simxGetJointPosition(clientID, handler, ref position, simx_opmode.oneshot_wait);        
        }

        public void Move(float value)
        {
            position += value;
            vrepLib.simxSetJointPosition(clientID, handler, position, simx_opmode.oneshot_wait);        
        }

        public void Backward()
        {
            Move(-(float)Math.PI / 90.0f);
        }

        public void Forward()
        {
            Move((float)Math.PI / 90.0f);
        }


    }
}
