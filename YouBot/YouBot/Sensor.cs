
using System;
namespace YouBot
{
    class Sensor
    {
        int clientID;
        int handler;

        public char detectionState;
        public float[] detectionPoint = { -1.0f, -1.0f, -1.0f };
        public int objectHandle;
        public float[] normalVector = { -1.0f, -1.0f, -1.0f };

        public Sensor(int id, int handler)
        {
            this.clientID = id;
            this.handler  = handler;
        }

        public float Distance()
        {
            double v = Math.Sqrt(detectionPoint[0] * detectionPoint[0] + detectionPoint[1] * detectionPoint[1] + detectionPoint[2] * detectionPoint[2]);
            //Console.WriteLine(v);
            return (float)v;
        }

        public void ClearMeasurements()
        {
            detectionState = (char)0;
            detectionPoint[0] = -1.0f;
            detectionPoint[1] = -1.0f;
            detectionPoint[2] = -1.0f;
            objectHandle = -1;
            normalVector[0] = -1.0f;
            normalVector[1] = -1.0f;
            normalVector[2] = -1.0f;
        }



        public void Scan()
        {            
            vrepLib.simxReadProximitySensor(clientID, handler, ref detectionState, detectionPoint, ref objectHandle, normalVector, simx_opmode.oneshot);            
        }
    }
}
