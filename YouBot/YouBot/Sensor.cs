
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

        public void ClearMeasurements()
        {
            detectionState = '0';
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
            ClearMeasurements();

            simx_error e = vrepLib.simxReadProximitySensor(clientID, handler, ref detectionState, detectionPoint, ref objectHandle, normalVector, simx_opmode.oneshot_wait);

            if (e != simx_error.noerror) Console.WriteLine("Error");
        }
    }
}
