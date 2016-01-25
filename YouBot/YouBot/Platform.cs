using System;


namespace YouBot
{
    class Platform : Subsystem
    {
        private int[] wheelJoints = {-1, -1, -1, -1};

        public Platform(int id)
        {
            clientID = id;

            GetHandle(clientID, "rollingJoint_fl", out wheelJoints[0]);
            GetHandle(clientID, "rollingJoint_rl", out wheelJoints[1]);
            GetHandle(clientID, "rollingJoint_rr", out wheelJoints[2]);
            GetHandle(clientID, "rollingJoint_fr", out wheelJoints[3]);
        }

        private int clientID;
        

        public void Forward()
        {
            for (int i = 0; i < 4; ++i)
                vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[i], (float)Math.PI, simx_opmode.oneshot_wait);
        }

        public void Stop()
        {
            for (int i = 0; i < 4; ++i)
                vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[i], 0, simx_opmode.oneshot_wait);
        }

        public void Backward()
        {
            for (int i = 0; i < 4; ++i)
                vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[i], -(float)Math.PI, simx_opmode.oneshot_wait);
        }

        public void RotateCounterclockwise()
        {
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[0], (float)Math.PI, simx_opmode.oneshot_wait);
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[1], (float)Math.PI, simx_opmode.oneshot_wait);
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[2], -(float)Math.PI, simx_opmode.oneshot_wait);
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[3], -(float)Math.PI, simx_opmode.oneshot_wait);
        }

        public void RotateClockwise()
        {
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[0], -(float)Math.PI, simx_opmode.oneshot_wait);
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[1], -(float)Math.PI, simx_opmode.oneshot_wait);
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[2], (float)Math.PI, simx_opmode.oneshot_wait);
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[3], (float)Math.PI, simx_opmode.oneshot_wait);
        }

        public void Left()
        {
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[0], -(float)Math.PI, simx_opmode.oneshot_wait);
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[1], (float)Math.PI, simx_opmode.oneshot_wait);
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[2], -(float)Math.PI, simx_opmode.oneshot_wait);
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[3], (float)Math.PI, simx_opmode.oneshot_wait);
        }

        public void Right()
        {
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[0], (float)Math.PI, simx_opmode.oneshot_wait);
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[1], -(float)Math.PI, simx_opmode.oneshot_wait);
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[2], (float)Math.PI, simx_opmode.oneshot_wait);
            vrepLib.simxSetJointTargetVelocity(clientID, wheelJoints[3], -(float)Math.PI, simx_opmode.oneshot_wait);
        }

    }
}
