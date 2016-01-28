using System;

namespace YouBot
{
    class Platform : Subsystem
    {
        private int[] wheelJoints = {-1, -1, -1, -1};
        private int clientID;


        public Platform(int id, string suffix)
        {
            clientID = id;

            GetHandle(clientID, "rollingJoint_fl" + suffix, out wheelJoints[0]);
            GetHandle(clientID, "rollingJoint_rl" + suffix, out wheelJoints[1]);
            GetHandle(clientID, "rollingJoint_rr" + suffix, out wheelJoints[2]);
            GetHandle(clientID, "rollingJoint_fr" + suffix, out wheelJoints[3]);
        }

        private float velocity = (float)Math.PI;

        private void BasicMove(int handle, float velocity)
        {
            vrepLib.simxSetJointTargetVelocity(clientID, handle, velocity, simx_opmode.oneshot);
        }

        public void Forward()
        {
            for (int i = 0; i < 4; ++i)
                BasicMove(wheelJoints[i], velocity);                
        }

        public void Stop()
        {
            for (int i = 0; i < 4; ++i)
                BasicMove(wheelJoints[i], 0);
        }

        public void Backward()
        {
            for (int i = 0; i < 4; ++i)
                BasicMove(wheelJoints[i], -velocity);
        }

        public void RotateCounterclockwise()
        {
            BasicMove(wheelJoints[0],  velocity);
            BasicMove(wheelJoints[1],  velocity);
            BasicMove(wheelJoints[2], -velocity);
            BasicMove(wheelJoints[3], -velocity);            
        }

        public void RotateClockwise()
        {
            BasicMove(wheelJoints[0], -velocity);
            BasicMove(wheelJoints[1], -velocity);
            BasicMove(wheelJoints[2],  velocity);
            BasicMove(wheelJoints[3],  velocity);  
        }

        public void Left()
        {
            BasicMove(wheelJoints[0], -velocity);
            BasicMove(wheelJoints[1],  velocity);
            BasicMove(wheelJoints[2], -velocity);
            BasicMove(wheelJoints[3],  velocity);  
        }

        public void Right()
        {
            BasicMove(wheelJoints[0],  velocity);
            BasicMove(wheelJoints[1], -velocity);
            BasicMove(wheelJoints[2],  velocity);
            BasicMove(wheelJoints[3], -velocity);  
        }
    }
}
