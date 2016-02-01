using System;
using System.Threading;

namespace YouBot
{
    enum PLATFORM_STATE
    { 
        FORWARD,
        STOP,
        BACKWARD,
        ROTATE_COUNTERCLOCKWISE,
        ROTATE_CLOCKWISE,
        LEFT,
        RIGHT
    };

    class Platform : Subsystem
    {
        private int[] wheelJoints = {-1, -1, -1, -1};
        private int clientID;

        public PLATFORM_STATE state;
        private bool stateChanged = false;

        public Platform(int id, string suffix)
        {
            clientID = id;

            GetHandle(clientID, "rollingJoint_fl" + suffix, out wheelJoints[0]);
            GetHandle(clientID, "rollingJoint_rl" + suffix, out wheelJoints[1]);
            GetHandle(clientID, "rollingJoint_rr" + suffix, out wheelJoints[2]);
            GetHandle(clientID, "rollingJoint_fr" + suffix, out wheelJoints[3]);
        }

        private float velocity = (float)Math.PI/2;

        public void ChangeState(PLATFORM_STATE s)
        {
            state = s;
            stateChanged = true;
        }

        private void ReadState()
        {
            switch(state)
            {
                case PLATFORM_STATE.BACKWARD :
                Backward();
                break;

                case PLATFORM_STATE.FORWARD:
                Forward();
                break;

                case PLATFORM_STATE.LEFT:
                Left();
                break;

                case PLATFORM_STATE.RIGHT:
                Right();
                break;

                case PLATFORM_STATE.ROTATE_CLOCKWISE:
                RotateClockwise();
                break;

                case PLATFORM_STATE.ROTATE_COUNTERCLOCKWISE:
                RotateCounterclockwise();
                break;

                case PLATFORM_STATE.STOP:
                Stop();
                break;
            }

            stateChanged = false;
        }

        public void loop()
        {
            while (true)
            {
                if (stateChanged)
                {
                    ReadState();
                }

                Thread.Sleep(1);
            }
        }

        private void BasicMove(int handle, float velocity)
        {
            simx_error e = vrepLib.simxSetJointTargetVelocity(clientID, handle, velocity, simx_opmode.oneshot);

            if (simx_error.noerror != e) vrepLib.simxSetJointTargetVelocity(clientID, handle, 0, simx_opmode.oneshot);
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
