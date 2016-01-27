
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

        public void Scan()
        {
            vrepLib.simxReadProximitySensor(clientID, handler, ref detectionState, detectionPoint, ref objectHandle, normalVector, simx_opmode.oneshot_wait);
        }
    }
}
