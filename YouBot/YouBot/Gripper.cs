
namespace YouBot
{
    class Gripper : Subsystem
    {
        int clientID;
        bool open;

        int[] gripperJoints = { -1, -1 };

        public Gripper(int id, string suffix)
        {
            clientID = id;

            GetHandle(clientID, "youBotGripperJoint1" + suffix, out gripperJoints[0]);
            GetHandle(clientID, "youBotGripperJoint2" + suffix, out gripperJoints[1]);
        }

        public void Open()
        {
            if (open)
            {

                vrepLib.simxSetJointTargetVelocity(clientID, gripperJoints[1], 0.04f, simx_opmode.oneshot_wait);

                open = false;
            }
            else
            {

                vrepLib.simxSetJointTargetVelocity(clientID, gripperJoints[1], -0.04f, simx_opmode.oneshot_wait);

                open = true;
            }
            
            float gripperPos0 = 0;
           
            vrepLib.simxSetJointTargetPosition(clientID, gripperJoints[0], (float)vrepLib.simxGetJointPosition(clientID, gripperJoints[1], ref gripperPos0, simx_opmode.oneshot_wait) - 0.5f, simx_opmode.oneshot_wait);
        }
    }
}
