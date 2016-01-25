using System;
using System.Collections.Generic;

namespace YouBot
{
    class Arm : Subsystem
    {
        int clientID;

        int[] armJoints = { -1, -1, -1, -1, -1 };

        public Joint joint0;
        public Joint joint1;
        public Joint joint2;
        public Joint joint3;
        public Joint joint4;

        public Arm(int id)
        {
            for (int i = 0; i < 5; ++i)
            {
                GetHandle(clientID, "youBotArmJoint" + i.ToString(), out armJoints[i]);
            }

            joint0 = new Joint(id, armJoints[0]);
            joint1 = new Joint(id, armJoints[1]);
            joint2 = new Joint(id, armJoints[2]);
            joint3 = new Joint(id, armJoints[3]);
            joint4 = new Joint(id, armJoints[4]);
        }
    
    }
}
